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
    public class PostController : BaseController
    {
        private PostDao _postDao = new PostDao();

        [HasCredential(RoleID = "VIEW_POST")]
        public ActionResult Index()
        {
            var result = _postDao.ListAll();
            return View(result);
        }

        [HasCredential(RoleID = "ADD_POST")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HasCredential(RoleID = "ADD_POST")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(PostViewModel model)
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
                var post = new Post();
                post.UpdatePost(model);
                var result = _postDao.Insert(post);
                if (result > 0)
                {
                    SetAlert("Thêm danh thành công", "success");
                    return RedirectToAction("Index", "Post");
                }
                else
                    ModelState.AddModelError("", "Thêm thất bại");
            }
          
            return View(model);
        }

        [HasCredential(RoleID = "EDIT_POST")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = _postDao;
            var result = Mapper.Map<Post, PostViewModel>(dao.GetById(id)); ;         
            return View(result);
        }

        [HasCredential(RoleID = "EDIT_POST")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var session = (AdminLogin)Session[CommonConstants.ADMIN_SESSION];
                    var entity = new UserDao().GetByID(session.UserName);
                    model.UpdatedBy = entity.Name;
                    var post = new Post();
                    post.UpdatePost(model);
                    var result = _postDao.Update(post);
                    if (result)
                    {
                        SetAlert("Cập nhật thành công", "success");
                        return RedirectToAction("Index", "Post");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật thất bại");
                    }
                }
            }
         
            return View(model);
        }       

        [HasCredential(RoleID = "EDIT_POST")]
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = _postDao.ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        [HasCredential(RoleID = "DELETE_POST")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            _postDao.Delete(id);
            return RedirectToAction("Index");
        }
    }
}