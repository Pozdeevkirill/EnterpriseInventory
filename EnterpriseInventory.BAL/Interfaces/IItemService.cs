using EnterpriseInventory.BAL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseInventory.BAL.Interfaces
{
    public interface IItemService
    {
        public Task AddItemAsync(ItemDTO item);
        public Task RemoveItemAsync(int id);
        public Task UpdateItem(ItemDTO item);
        public ItemDTO GetItemById(int id);
        public List<ItemDTO> GetItemByName(string name);
        public List<ItemDTO> GetAll();
    }
}
