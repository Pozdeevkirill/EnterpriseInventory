using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseInventory.DAL.Models
{
    public class Cabinet
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Owner { get; set; }
        public List<Item>? Items { get; set; }
    }
}
