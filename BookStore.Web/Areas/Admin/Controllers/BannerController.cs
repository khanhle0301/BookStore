using AutoMapper;
using BookStore.Model.Dao;
using BookStore.Model.Models;
using BookStore.Web.Common;
using BookStore.Web.Infrastructure.Extensions;
using BookStore.Web.Models;
using System.Web.Mvc;

namespace BookStore.Web.Areas.Admin.Controllers
{
    public class BannerController : BaseController
    {
        private BannerDao _bannerDao = new BannerDao();

        [HasCredential(RoleID = "VIEW_BANNER")]
        public ActionResult Index()
        {
            var result = _bannerDao.GetAll();
            return View(result);
        }

        [HasCredential(RoleID = "EDIT_BANNER")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = Mapper.Map<Banner, BannerViewModel>(_bannerDao.GetById(id));
            return View(model);
        }

        [HasCredential(RoleID = "EDIT_BANNER")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(BannerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var banner = new Banner();
                banner.UpdateBanner(model);
                var res = _bannerDao.Update(banner);
                if (res)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "Banner");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }
            }
            return View(model);
        }
    }
}