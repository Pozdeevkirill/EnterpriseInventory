using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseInventory.BAL.ModelsDTO
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public int Article { get; set; }
        public string Name { get; set; }
        public string CabinetName { get; set; }
    }
}
