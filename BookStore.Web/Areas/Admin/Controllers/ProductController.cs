using AutoMapper;
using BookStore.Common;
using BookStore.Model.Dao;
using BookStore.Model.Models;
using BookStore.Web.Common;
using BookStore.Web.Infrastructure.Extensions;
using BookStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BookStore.Web.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        ProductDao _productDao = new ProductDao();
        // GET: Admin/Product
        [HasCredential(RoleID = "VIEW_PRODUCT")]
        public ActionResult Index()
        {
            var result = _productDao.ListAll();
            return View(result);
        }

        [HasCredential(RoleID = "ADD_PRODUCT")]
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            SetViewBagProvider();
            return View();
        }

        [HasCredential(RoleID = "ADD_PRODUCT")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.PromotionPrice >= model.Price)
                {
                    ModelState.AddModelError("", "Vui lòng kiểm tra lại giá khuyến mãi.");
                }
                else
                {
                    model.Alias = StringHelper.ToUnsignString(model.Name);
                    model.CreatedDate = DateTime.Now;
                    model.UpdatedDate = DateTime.Now;
                    model.MoreImages = "[]";
                    var session = (AdminLogin)Session[CommonConstants.ADMIN_SESSION];
                    var entity = new UserDao().GetByID(session.UserName);
                    model.CreatedBy = entity.Name;
                    model.UpdatedBy = entity.Name;
                    model.ViewCount = 0;
                    var product = new Product();
                    product.UpdateProduct(model);
                    var result = _productDao.Insert(product);
                    if (result > 0)
                    {
                        SetAlert("Thêm danh thành công", "success");
                        return RedirectToAction("Index", "Product");
                    }
                    else
                        ModelState.AddModelError("", "Thêm thất bại");
                }
            }
            SetViewBag();
            SetViewBagProvider();
            return View(model);
        }

        [HasCredential(RoleID = "EDIT_PRODUCT")]
        [HttpGet]
        public ActionResult Edit(int id)
        {          
            var result = Mapper.Map<Product, ProductViewModel>(_productDao.GetById(id));
            SetViewBag(result.CategoryID);
            SetViewBagProvider(result.ProviderID);
            return View(result);
        }

        [HasCredential(RoleID = "EDIT_PRODUCT")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProductViewModel model)
        {         
            if (ModelState.IsValid)
            {
                if (model.PromotionPrice >= model.Price)
                {
                    ModelState.AddModelError("", "Vui lòng kiểm tra lại giá khuyến mãi.");
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var session = (AdminLogin)Session[CommonConstants.ADMIN_SESSION];
                        var entity = new UserDao().GetByID(session.UserName);
                        model.UpdatedBy = entity.Name;
                        model.MoreImages = "[]";
                        var product = new Product();
                        product.UpdateProduct(model);
                        var result = _productDao.Update(product);                      
                        if (result)
                        {
                            SetAlert("Cập nhật thành công", "success");
                            return RedirectToAction("Index", "Product");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Cập nhật thất bại");
                        }
                    }
                }
            }
            SetViewBagProvider(model.ProviderID);
            SetViewBag(model.CategoryID);
            return View(model);
        }

        [HasCredential(RoleID = "DELETE_PRODUCT")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var result = _productDao.GetById(id);
            return View(result);
        }

        [HasCredential(RoleID = "DELETE_PRODUCT")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var dao = _productDao.Delete(id);
            if (dao)
            {
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ViewData["Error"] = "Xóa thất bại!";
                var result = _productDao.GetById(id);
                return View(result);
            }
        }

        public void SetViewBag(int? selectedId = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }

        public void SetViewBagProvider(int? selectedId = null)
        {
            var dao = new ProviderDao();
            ViewBag.ProviderID = new SelectList(dao.GetAll(), "ID", "Name", selectedId);
        }

        [HasCredential(RoleID = "EDIT_PRODUCT")]
        public JsonResult LoadImages(int id)
        {
            ProductDao dao = _productDao;
            var product = dao.GetById(id);
            List<string> listImagesReturn = new JavaScriptSerializer().Deserialize<List<string>>(product.MoreImages);
            return Json(new
            {
                data = listImagesReturn
            }, JsonRequestBehavior.AllowGet);
        }

        [HasCredential(RoleID = "EDIT_PRODUCT")]
        public JsonResult SaveImages(int id, string images)
        {
            var listImages = images.Replace(ConfigHelper.GetByKey("CurrentLink"), "/");

            ProductDao dao = _productDao;
            try
            {
                dao.UpdateImages(id, listImages);
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        [HasCredential(RoleID = "EDIT_PRODUCT")]
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = _productDao.ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}