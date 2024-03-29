﻿using EnterpriseInventory.DAL.Data;
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
    public class CabinetRepository : ICabinetRepository
    {
        private AppDbContext db;
        public CabinetRepository(AppDbContext _db)
        {
            db = _db;
        }

        public void Create(Cabinet model)
        {
            if (model == null)
                return;
            db.Cabinets.Add(model);
        }

        public void Delete(int id)
        {
            if (id < 0)
                return;
            var cabinet = db.Cabinets.Include(c=> c.Items).FirstOrDefault(c => c.Id == id);
            var storage = db.Cabinets.FirstOrDefault(c => c.Id == 1);


            foreach (var item in cabinet.Items)
            {
                item.Cabinet = storage;
            }

            db.Cabinets.Remove(cabinet);
        }

        public IEnumerable<Cabinet> GetAll()
        {
           return db.Cabinets.Include(c => c.Items).ToList();
        }

        public Cabinet GetById(int id)
        {
            if(id < 0)
                return null;
            return db.Cabinets.Include(c => c.Items).FirstOrDefault(c => c.Id == id);
        }

        public Cabinet GetCabinetByName(string name)
        {
            if (name == string.Empty)
                return null;
            return db.Cabinets
                .Include(c => c.Items)
                .FirstOrDefault(c => c.Name == name);
        }

        public IEnumerable<Cabinet> GetCabinetByOwner(string owner)
        {
            if (owner == string.Empty)
                return null;

            return db.Cabinets
                .Include(c => c.Items)
                .Where(c => EF.Functions.Like(c.Owner,$"%{owner}%"));
        }

        public void Update(Cabinet model)
        {
            if (model == null)
                return;

            var cabinet = GetById(model.Id);

            cabinet.Name = model.Name;
            cabinet.Owner = model.Owner;

            db.Cabinets.Update(cabinet);    
        }
    }
}
