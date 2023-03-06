using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseInventory.BAL.ModelsDTO
{
    public class CabinetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public List<ItemDTO> Items { get; set; }
    }
}
