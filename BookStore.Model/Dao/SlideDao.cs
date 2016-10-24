using BookStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Model.Dao
{
    public class SlideDao
    {
        private BookStoreDbContext db = null;

        public SlideDao()
        {
            db = new BookStoreDbContext();
        }

        public bool ChangeStatus(int id)
        {
            var slide = db.Slides.Find(id);
            slide.Status = !slide.Status;
            db.SaveChanges();
            return slide.Status;
        }

        public IEnumerable<Slide> GetAll()
        {
            return db.Slides;
        }

        public IEnumerable<Slide> GetSlides()
        {
            return db.Slides.Where(x => x.Status == true).OrderByDescending(x => x.ID);
        }       

        public Slide GetById(int id)
        {
            return db.Slides.Find(id);
        }

        public int Insert(Slide entity)
        {
            try
            {
                db.Slides.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool Update(Slide entity)
        {
            try
            {
                var Slide = db.Slides.Find(entity.ID);
                Slide.Name = entity.Name;              
                Slide.Image = entity.Image;
                Slide.Url = entity.Url;
                Slide.DisplayOrder = entity.DisplayOrder;
                Slide.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var Slide = db.Slides.Find(id);
                db.Slides.Remove(Slide);
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