using BookStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model.Dao
{
    public class BannerDao
    {
        private BookStoreDbContext db = null;

        public BannerDao()
        {
            db = new BookStoreDbContext();
        }

        public IEnumerable<Banner> GetAll()
        {
            return db.Banners;
        }

        public Banner GetBannerByType(string type)
        {
            return db.Banners.SingleOrDefault(x => x.Type == type);
        }

        public Banner GetById(int id)
        {
            return db.Banners.Find(id);
        }

        public bool Update(Banner entity)
        {
            try
            {
                var Slide = db.Banners.Find(entity.ID);
                Slide.Name = entity.Name;
                Slide.Image = entity.Image;
                Slide.Url = entity.Url;                        
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
