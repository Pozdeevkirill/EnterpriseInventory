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

        public async Task AddCabinetAsync(CabinetDTO cabinet)
        {
            Cabinet cab = new()
            {
                Name = cabinet.Name,
                Owner = cabinet.Owner,
            };

            db.CabinetRepository.Create(cab);
            await db.SaveAsync();
        }

        public async Task DeleteCabinet(int id)
        {
            if (id < 0)
                return;

            db.CabinetRepository.Delete(id);
            await db.SaveAsync();
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
                    Category = item.Category,
                    Count = item.Count,
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
