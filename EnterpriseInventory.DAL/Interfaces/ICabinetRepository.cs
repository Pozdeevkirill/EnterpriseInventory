using EnterpriseInventory.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseInventory.DAL.Interfaces
{
    public interface ICabinetRepository : IRepository<Cabinet>
    {
        public Cabinet GetCabinetByName(string name);
        public Cabinet GetCabinetByOwner(string owner);
    }
}
