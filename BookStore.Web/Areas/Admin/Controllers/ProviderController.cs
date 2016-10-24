using AutoMapper;
using BookStore.Common;
using BookStore.Model.Dao;
using BookStore.Model.Models;
using BookStore.Web.Common;
using BookStore.Web.Infrastructure.Extensions;
using BookStore.Web.Models;
using System;
using System.Web.Mvc;

namespace BookStore.Web.Areas.Admin.Controllers
{
    public class ProviderController : BaseController
    {
        private ProviderDao _providerDao = new ProviderDao();

        [HasCredential(RoleID = "VIEW_PROVIDER")]
        public ActionResult Index()
        {
            var model = _providerDao.GetAll();
            return View(model);
        }

        [HasCredential(RoleID = "EDIT_PROVIDER")]
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = _providerDao.ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        [HasCredential(RoleID = "DELETE_PROVIDER")]
        public ActionResult Delete(int id)
        {
            var result = _providerDao.GetById(id);
            return View(result);
        }

        [HasCredential(RoleID = "DELETE_PROVIDER")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var dao = _providerDao.Delete(id);
            if (dao)
            {
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index", "Provider");
            }
            else
            {
                ViewData["Error"] = "Xóa thất bại!";
                var result = _providerDao.GetById(id);
                return View(result);
            }
        }


        [HasCredential(RoleID = "ADD_PROVIDER")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HasCredential(RoleID = "ADD_PROVIDER")]
        [HttpPost]
        public ActionResult Create(ProviderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.CreatedDate = DateTime.Now;
                    model.UpdatedDate = DateTime.Now;
                    var session = (AdminLogin)Session[CommonConstants.ADMIN_SESSION];
                    var entity = new UserDao().GetByID(session.UserName);
                    model.CreatedBy = entity.Name;
                    model.UpdatedBy = entity.Name;
                    var provider = new Provider();
                    provider.UpdateProvider(model);
                    int result = _providerDao.Insert(provider);
                    if (result == 0)
                    {
                        ModelState.AddModelError("", "Tên đã tồn tại");
                    }
                    else if (result > 0)
                    {
                        SetAlert("Thêm danh thành công", "success");
                        return RedirectToAction("Index", "Provider");
                    }
                    else
                        ModelState.AddModelError("", "Thêm thất bại");
                }
                return View(model);
            }
            catch
            { return View(); }
        }

        [HasCredential(RoleID = "EDIT_PROVIDER")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _providerDao.GetById(id);
            var result = Mapper.Map<Provider, ProviderViewModel>(model);
            return View(result);
        }

        [HasCredential(RoleID = "EDIT_PROVIDER")]
        [HttpPost]
        public ActionResult Edit(ProviderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var session = (AdminLogin)Session[CommonConstants.ADMIN_SESSION];
                    var entity = new UserDao().GetByID(session.UserName);
                    model.UpdatedBy = entity.Name;
                    var provider = new Provider();
                    provider.UpdateProvider(model);
                    var result = _providerDao.Update(provider);
                    if (result == 0)
                    {
                        ModelState.AddModelError("", "Tên đã tồn tại");
                    }
                    else if (result > 0)
                    {
                        SetAlert("Cập nhật thành công", "success");
                        return RedirectToAction("Index", "Provider");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật thất bại");
                    }
                }
                return View(model);
            }
            catch
            { return View(); }
        }
    }
}