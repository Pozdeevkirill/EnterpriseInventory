using EnterpriseInventory.BAL.Interfaces;
using EnterpriseInventory.BAL.ModelsDTO;
using EnterpriseInventory.DAL.Interfaces;
using EnterpriseInventory.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseInventory.BAL.Services
{
    public class CabinetService : ICabinetService
    {
        IUnitOfWork db;
        public CabinetService(IUnitOfWork _db)
        {
            db = _db;
        }

        public void AddCabinet(CabinetDTO cabinet)
        {
            if (cabinet == null)
                return;

            Cabinet cab = new()
            {
                Name = cabinet.Name,
                Owner = cabinet.Owner,
            };

            db.CabinetRepository.Create(cab);
            db.SaveAsync();
        }

        public async Task DeleteCabinet(int id)
        {
            if (id < 0)
                return;

            db.CabinetRepository.Delete(id);
            await db.SaveAsync();
        }

        public IEnumerable<CabinetDTO> GetAllCabinets()
        {
            var cabinets = db.CabinetRepository.GetAll();
            List<CabinetDTO> resultList = new();
            foreach (var cab in cabinets)
            {
                CabinetDTO _cab = new()
                {
                    Id = cab.Id,
                    Name = cab.Name,
                    Owner = cab.Owner,
                };
                resultList.Add(_cab);
            }
            return resultList;
        }

        public CabinetDTO GetCabinetById(int id)
        {
            if (id < 0)
                return null;

            var cab = db.CabinetRepository.GetById(id);
            CabinetDTO _item = new()
            {
                Id = cab.Id,
                Name = cab.Name,
                Owner = cab.Owner
            };
            List <ItemDTO> list = new();

            foreach (var item in cab.Items)
            {
                var itemDTO = new ItemDTO()
                {
                    Id=item.Id,
                    Article = item.Article,
                    CabinetName = _item.Name,
                    Name = item.Name,
                };
                list.Add(itemDTO);
            }
            _item.Items = list;

            return _item;
        }

        public CabinetDTO GetCabinetByItem(ItemDTO item)
        {
            throw new NotImplementedException();
        }

        public CabinetDTO GetCabinetByOwner(string owner)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCavinet(CabinetDTO cabinet)
        {
            throw new NotImplementedException();
        }
    }
}
