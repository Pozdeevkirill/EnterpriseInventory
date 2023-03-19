using EnterpriseInventory.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseInventory.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Cabinet> Cabinets { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Cabinet cabinet = new Cabinet() { Id = 1, Name = "Склад", Owner = "-", };
            modelBuilder.Entity<Cabinet>().HasData(new Cabinet[] {cabinet});
                
            base.OnModelCreating(modelBuilder);
        }
    }
}
