using EnterpriseInventory.DAL.Data;
using EnterpriseInventory.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseInventory.DAL.Repositoryes
{
    public class UnitOfWork : IUnitOfWork
    {
        CabinetRepository cabinetRepository;
        ItemRepository itemRepository;


        AppDbContext db;
        public UnitOfWork(AppDbContext _db)
        {
            db = _db;
        }

        public IItemRepository ItemRepository
        {
            get
            {
                if (itemRepository == null)
                    itemRepository = new(db);
                return itemRepository;
            }
        }

        public ICabinetRepository CabinetRepository
        {
            get
            {
                if(cabinetRepository == null)
                    cabinetRepository = new(db);
                return cabinetRepository;
            }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
