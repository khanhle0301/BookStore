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
    public class ProductCategoryController : BaseController
    {
        private ProductCategoryDao _productCategoryDao =new ProductCategoryDao();

        // GET: Admin/ProductCategory
        [HasCredential(RoleID = "VIEW_PRODUCTCATEGORY")]
        public ActionResult Index()
        {
            var result = _productCategoryDao.ListAll();
            return View(result);
        }

        [HasCredential(RoleID = "ADD_PRODUCTCATEGORY")]
        [HttpGet]
        public ActionResult Create()
        {        
            return View();
        }

        [HasCredential(RoleID = "ADD_PRODUCTCATEGORY")]
        [HttpPost]
        public ActionResult Create(ProductCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Alias = StringHelper.ToUnsignString(model.Name);
                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;
                var session = (AdminLogin)Session[CommonConstants.ADMIN_SESSION];
                var entity = new UserDao().GetByID(session.UserName);
                model.CreatedBy = entity.Name;
                model.UpdatedBy = entity.Name;
                var productCategory = new ProductCategory();
                productCategory.UpdateProductCategory(model);
                var result = _productCategoryDao.Insert(productCategory);
                if(result==0)
                {
                    ModelState.AddModelError("", "Tên danh mục đã tồn tại");
                }
                else if (result > 0)
                {
                    SetAlert("Thêm danh thành công", "success");
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                    ModelState.AddModelError("", "Thêm thất bại");
            }       
            return View(model);
        }

        [HasCredential(RoleID = "EDIT_PRODUCTCATEGORY")]
        [HttpGet]
        public ActionResult Edit(int id)
        {        
            var result = Mapper.Map<ProductCategory, ProductCategoryViewModel>(_productCategoryDao.GetById(id));          
            return View(result);
        }

        [HasCredential(RoleID = "EDIT_PRODUCTCATEGORY")]
        [HttpPost]
        public ActionResult Edit(ProductCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {              
                var session = (AdminLogin)Session[CommonConstants.ADMIN_SESSION];
                var entity = new UserDao().GetByID(session.UserName);
                model.UpdatedBy = entity.Name;
                var productCategory = new ProductCategory();
                productCategory.UpdateProductCategory(model);
                var result = _productCategoryDao.Update(productCategory);
                if (result == 0)
                {
                    ModelState.AddModelError("", "Tên danh mục đã tồn tại");
                }
                else if (result > 0)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thất bại");
                }
            }          
            return View(model);
        }

        [HasCredential(RoleID = "DELETE_PRODUCTCATEGORY")]
        public ActionResult Delete(int id)
        {
            var result = _productCategoryDao.GetById(id);
            return View(result);
        }

        [HasCredential(RoleID = "DELETE_PRODUCTCATEGORY")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var dao = _productCategoryDao.Delete(id);
            if (dao)
            {
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index", "ProductCategory");
            }
            else
            {
                ViewData["Error"] = "Xóa thất bại!";
                var result = _productCategoryDao.GetById(id);
                return View(result);
            }
        }

        [HasCredential(RoleID = "EDIT_PRODUCTCATEGORY")]
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = _productCategoryDao.ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}