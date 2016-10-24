using BookStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Model.Dao
{
    public class FeedbackDao
    {
        private BookStoreDbContext db = null;

        public FeedbackDao()
        {
            db = new BookStoreDbContext();
        }

        public bool ChangeStatus(int id)
        {
            var user = db.Feedbacks.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }

        public bool Delete(int id)
        {
            try
            {
                var feedback = db.Feedbacks.Find(id);
                db.Feedbacks.Remove(feedback);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Feedback GetById(int id)
        {
            return db.Feedbacks.Find(id);
        }

        public IEnumerable<Feedback> ListAll()
        {
            return db.Feedbacks.OrderByDescending(x => x.CreatedDate);
        }

        public int Insert(Feedback feedback)
        {
            try
            {
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                return feedback.ID;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}