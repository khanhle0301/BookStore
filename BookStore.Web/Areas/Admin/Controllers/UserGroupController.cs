using AutoMapper;
using BookStore.Model.Dao;
using BookStore.Model.Models;
using BookStore.Web.Common;
using BookStore.Web.Infrastructure.Extensions;
using BookStore.Web.Models;
using System.Web.Mvc;

namespace BookStore.Web.Areas.Admin.Controllers
{
    public class UserGroupController : BaseController
    {
        private UserGroupDao _userGrDao = new UserGroupDao();

        // GET: Admin/UserGroup
        [HasCredential(RoleID = "VIEW_USERGROUP")]
        public ActionResult Index()
        {
            var result = _userGrDao.GetAll();
            return View(result);
        }

        [HasCredential(RoleID = "ADD_USERGROUP")]
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Role = _userGrDao.ListAllRole();
            return View();
        }

        [HasCredential(RoleID = "ADD_USERGROUP")]
        [HttpPost]
        public ActionResult Create(UserGroupViewModel model)
        {
            ViewBag.Role = _userGrDao.ListAllRole();
            try
            {
                if (ModelState.IsValid)
                {
                    var userGr = new UserGroup();
                    userGr.UpdateUserGroup(model);
                    int id = _userGrDao.Insert(userGr);
                    new CredentialDao().Create(userGr, model.Role);
                    if (id == 0)
                    {
                        ModelState.AddModelError("", "Tên đã tồn tại");
                    }
                    else if (id > 0)
                    {
                        SetAlert("Thêm thành công", "success");
                        return RedirectToAction("Index", "UserGroup");
                    }
                    else
                        ModelState.AddModelError("", "Thêm thất bại");
                }
                return View(model);
            }
            catch
            { return View(); }
        }

        [HasCredential(RoleID = "EDIT_USERGROUP")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Role = _userGrDao.ListAllRole();
            string _credential = null;
            foreach (var credential in _userGrDao.GetCredentialByUserGroupID(id))
            {
                _credential += credential.RoleID + ",";
            }
            ViewBag.Credentail = _credential;
            var user = _userGrDao.ViewDetail(id);
            var res = Mapper.Map<UserGroup, UserGroupViewModel>(user);
            return View(res);
        }

        [HasCredential(RoleID = "EDIT_USERGROUP")]
        [HttpPost]
        public ActionResult Edit(UserGroupViewModel model)
        {
            ViewBag.Role = _userGrDao.ListAllRole();
            if (ModelState.IsValid)
            {
                var userGr = new UserGroup();
                userGr.UpdateUserGroup(model);
                var result = _userGrDao.Update(userGr);
                new CredentialDao().Update(userGr, model.Role);
                if (result == 0)
                {
                    ModelState.AddModelError("", "Tên đã tồn tại");
                }
                else if (result > 0)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index", "UserGroup");
                }
                else
                    ModelState.AddModelError("", "Cập nhật thất bại");
            }
            return View(model);
        }

        [HasCredential(RoleID = "DELETE_USERGROUP")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var result = _userGrDao.ViewDetail(id);
            return View(result);
        }

        [HasCredential(RoleID = "DELETE_USERGROUP")]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var result = _userGrDao.Delete(id);
            if (result)
            {
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index", "UserGroup");
            }
            else
            {
                ViewData["Error"] = "Xóa thất bại!";
                var model = _userGrDao.ViewDetail(id);
                return View(model);
            }
        }
    }
}