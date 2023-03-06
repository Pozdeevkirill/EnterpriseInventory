using EnterpriseInventory.BAL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseInventory.BAL.Interfaces
{
    public interface ICabinetService
    {
        public Task AddCabinetAsync(CabinetDTO cabinet);
        public CabinetDTO GetCabinetById(int id);
        public CabinetDTO GetCabinetByOwner(string owner);
        public CabinetDTO GetCabinetByItem(ItemDTO item);
        public Task UpdateCavinet(CabinetDTO cabinet);
        public Task DeleteCabinet(int id);
    }
}
