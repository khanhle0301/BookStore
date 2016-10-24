using AutoMapper;
using BookStore.Model.Dao;
using BookStore.Model.Models;
using BookStore.Web.Common;
using BookStore.Web.Infrastructure.Extensions;
using BookStore.Web.Models;
using System.Web.Mvc;

namespace BookStore.Web.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        // GET: Admin/Contact
        [HasCredential(RoleID = "VIEW_CONTACT")]
        public ActionResult Index()
        {
            var model = new ContactDao().GetContact();
            return View(model);
        }

        [HasCredential(RoleID = "EDIT_CONTACT")]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var model = new ContactDao().GetById(id);
            var res = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            return View(res);
        }

        [HasCredential(RoleID = "EDIT_CONTACT")]
        [HttpPost]
        public ActionResult Edit(ContactDetailViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var contact = new ContactDetail();
                    contact.UpdateContactDetail(model);
                    var result = new ContactDao().Update(contact);
                    if (result)
                    {
                        SetAlert("Cập nhật thành công", "success");
                        return RedirectToAction("Index", "Contact");
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