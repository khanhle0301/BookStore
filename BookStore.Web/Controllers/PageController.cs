using AutoMapper;
using BookStore.Common;
using BookStore.Model.Dao;
using BookStore.Model.Models;
using BookStore.Web.Infrastructure.Extensions;
using BookStore.Web.Models;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BookStore.Web.Controllers
{
    public class PageController : Controller
    {
        private PageDao _pageDao = new PageDao();

        // GET: Page
        public ActionResult Index(string alias)
        {
            var model = _pageDao.GetByAlias(alias);
            return View(model);
        }

        public ActionResult About()
        {
            var model = _pageDao.GetByAlias("Gioi-thieu");
            return View(model);
        }

        public ActionResult Contact()
        {
            FeedbackViewModel viewModel = new FeedbackViewModel();
            viewModel.ContactDetail = GetDetail();
            return View(viewModel);
        }


        [HttpPost]
        public JsonResult SendFeedback(string feebackViewModel)
        {
            try
            {
                var feebackVm = new JavaScriptSerializer().Deserialize<FeedbackViewModel>(feebackViewModel);
                Feedback newFeedback = new Feedback();
                newFeedback.UpdateFeedback(feebackVm);
                new FeedbackDao().Insert(newFeedback);
                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/feedback.html"));
                content = content.Replace("{{Name}}", feebackVm.Name);
                content = content.Replace("{{Email}}", feebackVm.Email);
                content = content.Replace("{{Message}}", feebackVm.Message);
                var adminEmail = ConfigHelper.GetByKey("AdminEmail");
                MailHelper.SendMail(adminEmail, "Thông tin liên hệ từ BookStore.", content);
                return Json(new
                {
                    status = true
                });
            }
            catch
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        private ContactDetailViewModel GetDetail()
        {
            var model = new ContactDao().GetContact();
            var contactViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            return contactViewModel;
        }
    }
}