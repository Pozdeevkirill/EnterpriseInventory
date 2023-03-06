using EnterpriseInventory.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseInventory.DAL.Interfaces
{
    public interface IItemRepository : IRepository<Item>
    {
        public IEnumerable<Item> GetByCabinet(string cabinet);
        public IEnumerable<Item> GetByCategory(string category);
    }
}
