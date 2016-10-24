using BookStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Model.Dao
{
    public class ProviderDao
    {
        private BookStoreDbContext db = null;

        public ProviderDao()
        {
            db = new BookStoreDbContext();
        }

        public bool ChangeStatus(int id)
        {
            var slide = db.Providers.Find(id);
            slide.Status = !slide.Status;
            db.SaveChanges();
            return slide.Status;
        }

        public IEnumerable<Provider> GetAll()
        {
            return db.Providers;
        }

        public IEnumerable<Provider> GetProviders()
        {
            return db.Providers.Where(x => x.Status == true).OrderByDescending(x => x.ID);
        }

        public Provider GetById(int id)
        {
            return db.Providers.Find(id);
        }

        public int Insert(Provider entity)
        {
            if (db.Providers.Any(x => x.Name == entity.Name))
                return 0;
            db.Providers.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public int Update(Provider entity)
        {
            if (db.Providers.Any(x => x.Name == entity.Name && x.ID != entity.ID))
                return 0;
            var model = db.Providers.Find(entity.ID);
            model.Name = entity.Name;
            model.MetaKeyword = entity.MetaKeyword;
            model.MetaDescription = entity.MetaDescription;
            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = entity.UpdatedBy;
            model.Status = entity.Status;
            db.SaveChanges();
            return entity.ID;
        }

        public bool Delete(int id)
        {
            try
            {
                var Provider = db.Providers.Find(id);
                db.Providers.Remove(Provider);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}