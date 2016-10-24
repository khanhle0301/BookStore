using BookStore.Common;
using BookStore.Model.Dao;
using BookStore.Model.Models;
using BookStore.Web.Infrastructure.Core;
using System;
using System.Web.Mvc;

namespace BookStore.Web.Controllers
{
    public class PostController : Controller
    {
        private PostDao _postDao = new PostDao();

        public ActionResult Index(int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSizeBlog"));
            int totalRow = 0;
            var productModel = _postDao.GetAllPaging(page, pageSize, out totalRow);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var paginationSet = new PaginationSet<Post>()
            {
                Items = productModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            return View(paginationSet);
        }

        public ActionResult Detail(int id)
        {
            var model = _postDao.GetById(id);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult News()
        {
            var mode = _postDao.GetNew(4);
            return PartialView(mode);
        }
    }
}