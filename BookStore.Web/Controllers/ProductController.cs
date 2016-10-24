using BookStore.Common;
using BookStore.Model.Dao;
using BookStore.Model.Models;
using BookStore.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BookStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private BannerDao _bannerDao = new BannerDao();
        private ProviderDao _providerDao = new ProviderDao();
        private ProductDao _productDao = new ProductDao();
        private ProductCategoryDao _productCategory = new ProductCategoryDao();
       
        public ActionResult All(int page = 1, string sort = "created-descending")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productDao.GetAllProductPaging(page, pageSize, sort, out totalRow);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<Product>()
            {
                Items = productModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            ViewBag.Sort = sort;
            return View(paginationSet);
        }

        public ActionResult Banchay(int page = 1, string sort = "created-descending")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productDao.GetSaleFlagProductPaging(page, pageSize, sort, out totalRow);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<Product>()
            {
                Items = productModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            ViewBag.Sort = sort;
            return View(paginationSet);
        }

        public ActionResult News(int page = 1, string sort = "created-descending")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productDao.GetNewsProductPaging(page, pageSize, sort, out totalRow);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<Product>()
            {
                Items = productModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            ViewBag.Sort = sort;
            return View(paginationSet);
        }

        public ActionResult ProductCategory(string alias, int page = 1, string sort = "created-descending")
        {
            var category = _productCategory.GetByAlias(alias);
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productDao.GetListProductByCategoryIdPaging(category.ID, page, pageSize, sort, out totalRow);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<Product>()
            {
                Items = productModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            ViewBag.Sort = sort;
            ViewBag.Category = category;
            return View(paginationSet);
        }

        public ActionResult Search(string keyword, int page = 1, string sort = "created-descending")
        {           
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productDao.GetAllBySearch(keyword, page, pageSize, sort, out totalRow);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<Product>()
            {
                Items = productModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            ViewBag.Sort = sort;
            ViewBag.Keyword = keyword;
            return View(paginationSet);
        }

        public ActionResult Detail(int id)
        {
            var model = _productDao.GetById(id);
            var relatedProduct = _productDao.GetReatedProducts(id, 6);
            ViewBag.RelatedProducts = relatedProduct;
            List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(model.MoreImages);
            ViewBag.MoreImages = listImages;
            ViewBag.Provider = _providerDao.GetById(model.ProviderID);
            ViewBag.Category = _productCategory.GetById(model.CategoryID);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult Category()
        {
            var category = _productCategory.GetCategory();
            return PartialView(category);
        }

        [ChildActionOnly]
        public ActionResult ProductHot()
        {
            var model = _productDao.GetHomeHotProduct(3);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult Quangcao()
        {
            var model = _bannerDao.GetBannerByType("quang-cao-category");
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult Banner()
        {
            var model = _bannerDao.GetBannerByType("banner-category");
            return PartialView(model);
        }
    }
}