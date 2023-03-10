using EnterpriseInventory.DAL.Data;
using EnterpriseInventory.DAL.Interfaces;
using EnterpriseInventory.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseInventory.DAL.Repositoryes
{
    public class ItemRepository : IItemRepository
    {
        private AppDbContext db;
        public ItemRepository(AppDbContext _db)
        {
            db = _db;
        }

        public void Create(Item model)
        {
            if (model == null)
                return;
            db.Items.Add(model);    
        }

        public void Delete(int id)
        {
            if (id < 0)
                return;
            db.Items.Remove(db.Items.FirstOrDefault(i => i.Id == id));
        }

        public IEnumerable<Item> GetAll()
        {
            return db.Items.Include(i => i.Cabinet).ToList();
        }

        public IEnumerable<Item> GetByCabinet(string cabinet)
        {
            if (cabinet == string.Empty)
                return null;
            return db.Items.Include(i => i.Cabinet).Where(i => i.Cabinet.Name == cabinet);
        }

        public Item GetById(int id)
        {
            if (id < 0)
                return null;
            return db.Items.Include(i=>i.Cabinet).FirstOrDefault(i => i.Id == id);
        }

        public void Update(Item model)
        {
            if (model == null)
                return;
            db.Items.Update(model);
        }
    }
}
