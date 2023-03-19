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
            db.SaveAsync();
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

                _cab.Items = new();
                foreach(var item in cab.Items)
                {
                    ItemDTO _item = new()
                    {
                        Name = item.Name,
                        Article = item.Article,
                        CabinetName = item.Cabinet.Name,
                        Id = item.Id
                    };
                    _cab.Items.Add(_item);  
                }

                resultList.Add(_cab);
            }
            return resultList;
        }

        public CabinetDTO GetCabinetById(int id)
        {
            if (id < 0)
                return null;

            var cab = db.CabinetRepository.GetById(id);

            if (cab == null)
                return null;

            CabinetDTO _cab = new()
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
                    CabinetName = _cab.Name,
                    Name = item.Name,
                };
                list.Add(itemDTO);
            }
            _cab.Items = list;

            return _cab;
        }

        public CabinetDTO GetCabinetByItem(ItemDTO item)
        {
            if (item == null) return null;

            var cab = db.CabinetRepository.GetCabinetByName(item.CabinetName);

            CabinetDTO _cab = new()
            {
                Id = cab.Id,
                Name = cab.Name,
                Owner = cab.Owner
            };
            List<ItemDTO> list = new();

            foreach (var _item in cab.Items)
            {
                var itemDTO = new ItemDTO()
                {
                    Id = _item.Id,
                    Article = _item.Article,
                    CabinetName = _cab.Name,
                    Name = _item.Name,
                };
                list.Add(itemDTO);
            }
            _cab.Items = list;

            return _cab;

        }

        public IEnumerable<CabinetDTO> GetCabinetByOwner(string owner)
        {
            if (owner == string.Empty) return null;

            var cabinets = db.CabinetRepository.GetCabinetByOwner(owner).ToList();

            List<CabinetDTO> resultList = new();
            foreach (var cab in cabinets)
            {
                CabinetDTO _cab = new()
                {
                    Id = cab.Id,
                    Name = cab.Name,
                    Owner = cab.Owner,
                };

                _cab.Items = new();
                foreach (var item in cab.Items)
                {
                    ItemDTO _item = new()
                    {
                        Name = item.Name,
                        Article = item.Article,
                        CabinetName = item.Cabinet.Name,
                        Id = item.Id
                    };
                    _cab.Items.Add(_item);
                }

                resultList.Add(_cab);
            }

            return resultList;
        }

        public async Task UpdateCabinet(CabinetDTO cabinet)
        {
            if (cabinet == null)
                return;

            Cabinet cab = new()
            {
                Id = cabinet.Id,
                Name = cabinet.Name,
                Owner = cabinet.Owner,
            };

            var _cab = db.CabinetRepository.GetById(cab.Id);
            cab.Items = _cab.Items;

            db.CabinetRepository.Update(cab);
            db.SaveAsync();
        }
    }
}
