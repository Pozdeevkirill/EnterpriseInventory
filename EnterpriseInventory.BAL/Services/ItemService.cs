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
    public class ItemService : IItemService
    {
        IUnitOfWork db;
        public ItemService(IUnitOfWork _db)
        {
            db = _db;
        }

        public async Task AddItemAsync(ItemDTO item)
        {
            if (item == null)
                return;
            Item _item = new()
            {
                Name = item.Name,
                Cabinet = db.CabinetRepository.GetCabinetByName(item.CabinetName),
                Article = item.Article,
            };
            db.ItemRepository.Create(_item);
            db.SaveAsync();
        }

        public List<ItemDTO> GetAll()
        {
            var temp = db.ItemRepository.GetAll();
            List<ItemDTO> resultList = new();

            foreach (var item in temp)
            {
                ItemDTO _item = new()
                {
                    Id = item.Id,
                    Article = item.Article,
                    Name = item.Name,
                    CabinetName = item.Cabinet.Name
                };
                resultList.Add(_item);
            }

            return resultList;
        }

        public ItemDTO GetItemById(int id)
        {
            if (id < 0)
                return null;

            var item = db.ItemRepository.GetById(id);
            if (item == null)
                return null;
            return new()
            {
                Id = item.Id,
                Article = item.Article,
                Name = item.Name,
                CabinetName = item.Cabinet.Name,
            };
        }

        public List<ItemDTO> GetItemByName(string name)
        {
            if (name == string.Empty)
                return null;

            var items = db.ItemRepository.GetAll().Where(i => i.Name == name);
            List<ItemDTO> _items = new();
            foreach (var item in items)
            {
                _items.Add(new ItemDTO()
                {
                    Id=item.Id,
                    Name = item.Name,
                    Article = item.Article,
                    CabinetName = item.Cabinet.Name,
                });
            }

            return _items;
            
        }

        public async Task RemoveItemAsync(int id)
        {
            if (id < 0)
                return;

            db.ItemRepository.Delete(id);
             db.SaveAsync();
        }

        public async Task UpdateItem(ItemDTO item)
        {
            if (item == null)
                return;

            Item _item = new()
            {
                Id = item.Id,
                Name = item.Name,
                Article = item.Article,
                Cabinet = db.CabinetRepository.GetCabinetByName(item.CabinetName),
            };

            db.ItemRepository.Update(_item);
            db.SaveAsync();
        }
    }
}
