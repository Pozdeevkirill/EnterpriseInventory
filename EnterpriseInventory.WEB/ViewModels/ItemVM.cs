using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseInventory.WEB.ViewModels
{
    public class ItemVM
    {
        public string Name { get; set; }
        public int Article { get; set; }
        public string? CabinetName { get; set; }
    }
}
