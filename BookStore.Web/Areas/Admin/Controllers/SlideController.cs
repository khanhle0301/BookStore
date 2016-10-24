using AutoMapper;
using BookStore.Model.Dao;
using BookStore.Model.Models;
using BookStore.Web.Common;
using BookStore.Web.Infrastructure.Extensions;
using BookStore.Web.Models;
using System.Web.Mvc;

namespace BookStore.Web.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        private SlideDao _slideDao = new SlideDao();

        [HasCredential(RoleID = "VIEW_SLIDE")]
        public ActionResult Index()
        {
            var model = _slideDao.GetAll();
            return View(model);
        }

        [HasCredential(RoleID = "EDIT_SLIDE")]
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = _slideDao.ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        [HasCredential(RoleID = "DELETE_SLIDE")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            _slideDao.Delete(id);
            return RedirectToAction("Index");
        }

        [HasCredential(RoleID = "ADD_SLIDE")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HasCredential(RoleID = "ADD_SLIDE")]
        [HttpPost]
        public ActionResult Create(SlideViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var slide = new Slide();
                    slide.UpdateSlide(model);
                    int id = _slideDao.Insert(slide);
                    if (id > 0)
                    {
                        SetAlert("Thêm thành công", "success");
                        return RedirectToAction("Index", "Slide");
                    }
                    else
                        ModelState.AddModelError("", "Thêm thất bại");
                }
                return View(model);
            }
            catch
            { return View(); }
        }

        [HasCredential(RoleID = "EDIT_SLIDE")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _slideDao.GetById(id);
            var result = Mapper.Map<Slide, SlideViewModel>(model);
            return View(result);
        }

        [HasCredential(RoleID = "EDIT_SLIDE")]
        [HttpPost]
        public ActionResult Edit(SlideViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var slide = new Slide();
                    slide.UpdateSlide(model);
                    var result = _slideDao.Update(slide);
                    if (result)
                    {
                        SetAlert("Cập nhật thành công", "success");
                        return RedirectToAction("Index", "Slide");
                    }
                    else
                        ModelState.AddModelError("", "Cập nhật thất bại");
                }
                return View(model);
            }
            catch
            { return View(); }
        }
    }
}