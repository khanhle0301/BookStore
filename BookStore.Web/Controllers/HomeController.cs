using BookStore.Common;
using BookStore.Model.Dao;
using BookStore.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BookStore.Web.Controllers
{
    public class HomeController : Controller
    {
        SlideDao _slideDao = new SlideDao();
        BannerDao _bannerDao = new BannerDao();
        ProductDao _productDao = new ProductDao();
        ProductCategoryDao _productCategory = new ProductCategoryDao();
        public ActionResult Index()
        {
            ViewBag.Title = ConfigHelper.GetByKey("HomeTitle");
            ViewBag.Keywords = ConfigHelper.GetByKey("HomeKeyword");
            ViewBag.Descriptions = ConfigHelper.GetByKey("HomeDescription");
            var homeVm = new HomeViewModel();
            homeVm.Slide = _slideDao.GetSlides();
            homeVm.Category = _productCategory.GetCategory();
            homeVm.ProductHot = _productDao.GetHomeHotProduct(5);
            homeVm.ProductNew = _productDao.GetHomeLastest(5);
            homeVm.ProductOnsale = _productDao.GetHomeSale(5);
            homeVm.GetAllHome = _productDao.GetAllHome(12);
            homeVm.SlideBanner = _bannerDao.GetBannerByType("banner-slide");
            homeVm.QuangCao1 = _bannerDao.GetBannerByType("quang-cao-1");
            homeVm.QuangCao2 = _bannerDao.GetBannerByType("quang-cao-2");
            return View(homeVm);
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            var category = _productCategory.GetCategory();
            return PartialView(category);
        }

        [ChildActionOnly]
        public ActionResult HeaderCart()
        {
            var cart = Session[CommonConstants.SessionCart];
            var list = new List<ShoppingCartViewModel>();
            if (cart != null)
            {
                list = (List<ShoppingCartViewModel>)cart;
            }
            return PartialView(list);
        }

        [ChildActionOnly]
        public ActionResult Account()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            ViewBag.Facebook = ConfigHelper.GetByKey("Facebook");
            ViewBag.Twitter = ConfigHelper.GetByKey("Twitter");
            ViewBag.Vimeo = ConfigHelper.GetByKey("Vimeo");
            ViewBag.Pinterest = ConfigHelper.GetByKey("Pinterest");
            FooterHomeViewModel footerVm = new FooterHomeViewModel();
            footerVm.ContactDetail = new ContactDao().GetContact();
            footerVm.News = new PostDao().GetNew(3);
            footerVm.Page = new PageDao().GetPages();
            return PartialView(footerVm);
        }

        [ChildActionOnly]
        public ActionResult Mocua()
        {
            ViewBag.Mocua = ConfigHelper.GetByKey("Mocua");
            return PartialView(new ContactDao().GetContact());
        }

    }
}