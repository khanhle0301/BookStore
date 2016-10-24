using BookStore.Common;
using BookStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BookStore.Model.Dao
{
    public class ProductDao
    {
        private BookStoreDbContext db = null;

        public ProductDao()
        {
            db = new BookStoreDbContext();
        }

        public bool ChangeStatus(int id)
        {
            var cate = db.Products.Find(id);
            cate.Status = !cate.Status;
            db.SaveChanges();
            return cate.Status;
        }

        public bool Delete(int id)
        {
            try
            {
                var cate = db.Products.Find(id);
                db.Products.Remove(cate);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Product GetById(int id)
        {
            return db.Products.Find(id);
        }      

        public IEnumerable<Product> ListAll()
        {
            return db.Products.OrderByDescending(x => x.CreatedDate);
        }

        public IEnumerable<string> GetListProductByName(string name)
        {
            return db.Products.Include("ProductCategory").Where(x => x.Status && x.Name.Contains(name)).Select(y => y.Name);
        }

        public IEnumerable<Product> GetReatedProducts(int id, int top)
        {
            var product = db.Products.Find(id);
            return db.Products.Include("ProductCategory").Where(x => x.Status && x.ID != id && x.CategoryID == product.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public void IncreaseView(int id)
        {
            var post = db.Products.Find(id);
            if (post.ViewCount.HasValue)
                post.ViewCount += 1;
            else
                post.ViewCount = 1;
            db.SaveChanges();
        }

        public Post GetByAlias(string alias)
        {
            return db.Posts.SingleOrDefault(x => x.Alias == alias);
        }

        public Product GetAllById(int id)
        {
            return db.Products.Include("ProductCategory").SingleOrDefault(x => x.ID == id);
        }

        public IEnumerable<Product> GetHomeLastest(int top)
        {
            return db.Products.Include("ProductCategory").Where(x => x.Status && x.HomeFlag == true)
                                .OrderByDescending(x => x.UpdatedDate).Take(top);
        }

        public IEnumerable<Product> GetAllHome(int top)
        {
            return db.Products.Include("ProductCategory").Where(x => x.Status && x.HomeFlag == true)
                                .OrderByDescending(x => x.UpdatedDate).Take(top);
        }

        public IEnumerable<Product> GetHomeSale(int top)
        {
            return db.Products.Include("ProductCategory").Where(x => x.Status && x.HomeFlag == true && x.PromotionPrice.HasValue)
                                .OrderByDescending(x => x.UpdatedDate).Take(top);
        }

        public IEnumerable<Product> GetHomeHotProduct(int top)
        {
            return db.Products.Include("ProductCategory").Where(x => x.Status && x.HomeFlag == true && x.HotFlag == true)
                                .OrderByDescending(x => x.UpdatedDate).Take(top);
        }

        public IEnumerable<Product> GetViewCount(int top)
        {
            return db.Products.Include("ProductCategory").Where(x => x.Status).OrderByDescending(x => x.ViewCount).Take(top);
        }

        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }

        public IEnumerable<Product> GetAllProductPaging(int page, int pageSize, string sort, out int totalRow)
        {
            var query = db.Products.Include("ProductCategory").Where(x => x.Status);

            switch (sort)
            {
                case "price-ascending":
                    query = query.OrderBy(x => x.PromotionPrice.HasValue ? x.PromotionPrice : x.Price);
                    break;

                case "price-descending":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue ? x.PromotionPrice : x.Price);
                    break;

                case "title-ascending":
                    query = query.OrderBy(x => x.Name);
                    break;

                case "title-descending":
                    query = query.OrderByDescending(x => x.Name);
                    break;

                case "created-ascending":
                    query = query.OrderBy(x => x.UpdatedDate);
                    break;

                case "created-descending":
                    query = query.OrderByDescending(x => x.UpdatedDate);
                    break;

                default:
                    query = query.OrderByDescending(x => x.UpdatedDate);
                    break;
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetAllBySearch(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = db.Products.Include("ProductCategory").Where(x => x.Status && x.Name.Contains(keyword));
            switch (sort)
            {
                case "price-ascending":
                    query = query.OrderBy(x => x.PromotionPrice.HasValue ? x.PromotionPrice : x.Price);
                    break;

                case "price-descending":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue ? x.PromotionPrice : x.Price);
                    break;

                case "title-ascending":
                    query = query.OrderBy(x => x.Name);
                    break;

                case "title-descending":
                    query = query.OrderByDescending(x => x.Name);
                    break;

                case "created-ascending":
                    query = query.OrderBy(x => x.UpdatedDate);
                    break;

                case "created-descending":
                    query = query.OrderByDescending(x => x.UpdatedDate);
                    break;

                default:
                    query = query.OrderByDescending(x => x.UpdatedDate);
                    break;
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetSaleFlagProductPaging(int page, int pageSize, string sort, out int totalRow)
        {
            var query = db.Products.Include("ProductCategory").Where(x => x.Status && x.SaleFlag == true);

            switch (sort)
            {
                case "price-ascending":
                    query = query.OrderBy(x => x.PromotionPrice.HasValue ? x.PromotionPrice : x.Price);
                    break;

                case "price-descending":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue ? x.PromotionPrice : x.Price);
                    break;

                case "title-ascending":
                    query = query.OrderBy(x => x.Name);
                    break;

                case "title-descending":
                    query = query.OrderByDescending(x => x.Name);
                    break;

                case "created-ascending":
                    query = query.OrderBy(x => x.UpdatedDate);
                    break;

                case "created-descending":
                    query = query.OrderByDescending(x => x.UpdatedDate);
                    break;

                default:
                    query = query.OrderByDescending(x => x.UpdatedDate);
                    break;
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetNewsProductPaging(int page, int pageSize, string sort, out int totalRow)
        {
            var query = db.Products.Include("ProductCategory").Where(x => x.Status).Take(10);

            switch (sort)
            {
                case "price-ascending":
                    query = query.OrderBy(x => x.PromotionPrice.HasValue ? x.PromotionPrice : x.Price);
                    break;

                case "price-descending":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue ? x.PromotionPrice : x.Price);
                    break;

                case "title-ascending":
                    query = query.OrderBy(x => x.Name);
                    break;

                case "title-descending":
                    query = query.OrderByDescending(x => x.Name);
                    break;

                case "created-ascending":
                    query = query.OrderBy(x => x.UpdatedDate);
                    break;

                case "created-descending":
                    query = query.OrderByDescending(x => x.UpdatedDate);
                    break;

                default:
                    query = query.OrderByDescending(x => x.UpdatedDate);
                    break;
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
        public IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow)
        {
            var query = db.Products.Include("ProductCategory").Where(x => x.Status && x.CategoryID == categoryId);

            switch (sort)
            {
                case "price-ascending":
                    query = query.OrderBy(x => x.PromotionPrice.HasValue ? x.PromotionPrice : x.Price);
                    break;

                case "price-descending":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue ? x.PromotionPrice : x.Price);
                    break;

                case "name_asc":
                    query = query.OrderBy(x => x.Name);
                    break;

                case "name_desc":
                    query = query.OrderByDescending(x => x.Name);
                    break;

                case "updated_asc":
                    query = query.OrderBy(x => x.UpdatedDate);
                    break;

                case "updated_desc":
                    query = query.OrderByDescending(x => x.UpdatedDate);
                    break;

                default:
                    query = query.OrderByDescending(x => x.UpdatedDate);
                    break;
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public int Insert(Product product)
        {
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
                return product.ID;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void UpdateImages(int productId, string images)
        {
            var product = db.Products.Find(productId);
            product.MoreImages = images;
            db.SaveChanges();
        }

        public bool Update(Product product)
        {
            try
            {
                var model = db.Products.Find(product.ID);
                model.Name = product.Name;
                model.Alias = StringHelper.ToUnsignString(product.Name);
                model.MetaKeyword = product.MetaKeyword;
                model.MetaDescription = product.MetaDescription;
                model.CategoryID = product.CategoryID;
                model.ProviderID = product.ProviderID;
                model.Image = product.Image;
                model.Price = product.Price;
                model.PromotionPrice = product.PromotionPrice;
                model.Quantity = product.Quantity;
                model.Description = product.Description;
                model.Content = product.Content;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = product.UpdatedBy;
                model.HomeFlag = product.HomeFlag;
                model.HotFlag = product.HotFlag;
                model.SaleFlag = product.SaleFlag;
                model.Status = product.Status;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}