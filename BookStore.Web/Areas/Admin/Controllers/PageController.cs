using AutoMapper;
using BookStore.Common;
using BookStore.Model.Dao;
using BookStore.Model.Models;
using BookStore.Web.Common;
using BookStore.Web.Infrastructure.Extensions;
using BookStore.Web.Models;
using System.Web.Mvc;

namespace BookStore.Web.Areas.Admin.Controllers
{
    public class PageController : BaseController
    {
        // GET: Admin/Page
        [HasCredential(RoleID = "VIEW_PAGE")]
        public ActionResult Index()
        {
            var model = new PageDao().ListAll();
            return View(model);
        }

        [HasCredential(RoleID = "EDIT_PAGE")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new PageDao().GetById(id);
            var result = Mapper.Map<Page, PageViewModel>(model);
            return View(result);
        }

        [HasCredential(RoleID = "EDIT_PAGE")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(PageViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var session = (AdminLogin)Session[CommonConstants.ADMIN_SESSION];
                    var entity = new UserDao().GetByID(session.UserName);
                    model.UpdatedBy = entity.UserName;
                    var page = new Page();
                    page.UpdatePage(model);
                    var result = new PageDao().Update(page);
                    if (result)
                    {
                        SetAlert("Cập nhật thành công", "success");
                        return RedirectToAction("Index", "Page");
                    }
                    else
                        ModelState.AddModelError("", "Cập nhật thất bại");
                }
                return View(model);
            }
            catch
            { return View(); }
        }

        [HasCredential(RoleID = "EDIT_PAGE")]
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new PageDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}