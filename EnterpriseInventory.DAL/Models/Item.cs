using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseInventory.DAL.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int Article { get; set; }
        public string Name { get; set; }
        public Cabinet Cabinet { get; set; }
    }
}
