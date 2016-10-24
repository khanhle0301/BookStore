using BookStore.Common;
using BookStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BookStore.Model.Dao
{
    public class PostDao
    {
        private BookStoreDbContext db = null;

        public PostDao()
        {
            db = new BookStoreDbContext();
        }

        public bool ChangeStatus(int id)
        {
            var cate = db.Posts.Find(id);
            cate.Status = !cate.Status;
            db.SaveChanges();
            return cate.Status;
        }
       
        public int Insert(Post post)
        {
            try
            {
                db.Posts.Add(post);
                db.SaveChanges();               
                return post.ID;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool Update(Post post)
        {
            try
            {
                var model = db.Posts.Find(post.ID);
                model.Name = post.Name;
                model.Alias = StringHelper.ToUnsignString(post.Name);             
                model.MetaKeyword = post.MetaKeyword;
                model.Image = post.Image;
                model.Description = post.Description;
                model.MetaDescription = post.MetaDescription;
                model.Content = post.Content;              
                model.Status = post.Status;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = post.UpdatedBy;
                db.SaveChanges();               
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Post GetById(int id)
        {
            return db.Posts.Find(id);
        }

        public void Delete(int id)
        {
            var cate = db.Posts.Find(id);
            db.Posts.Remove(cate);
            db.SaveChanges();
        }

        public IEnumerable<Post> ListAll()
        {
            return db.Posts.OrderByDescending(x => x.CreatedDate);
        }

        public void IncreaseView(int id)
        {
            var post = db.Posts.Find(id);
            if (post.ViewCount.HasValue)
                post.ViewCount += 1;
            else
                post.ViewCount = 1;
            db.SaveChanges();
        }             

        public IEnumerable<Post> GetNew(int top)
        {
            return db.Posts.Where(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(top);
        }
                
        public IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            var model = db.Posts.Where(x => x.Status).OrderByDescending(x => x.CreatedDate);
            totalRow = model.Count();
            return model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}