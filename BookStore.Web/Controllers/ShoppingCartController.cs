using AutoMapper;
using BookStore.Common;
using BookStore.Model.Dao;
using BookStore.Model.Models;
using BookStore.Web.Common;
using BookStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BookStore.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ProductDao _productDao = new ProductDao();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = Session[CommonConstants.SessionCart];
            var list = new List<ShoppingCartViewModel>();
            if (cart != null)
            {
                list = (List<ShoppingCartViewModel>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult Add(int productId, int quantity)
        {
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            var product = _productDao.GetAllById(productId);
            if (cart == null)
            {
                cart = new List<ShoppingCartViewModel>();
            }
            if (quantity > product.Quantity)
            {
                return Json(new
                {
                    status = false,
                    message = "Số lượng không đủ."
                });
            }

            if (cart.Any(x => x.ProductId == productId))
            {
                foreach (var item in cart)
                {
                    if (item.ProductId == productId)
                    {
                        item.Quantity += quantity;
                    }
                }
            }
            else
            {
                ShoppingCartViewModel newItem = new ShoppingCartViewModel();
                newItem.ProductId = productId;
                newItem.Product = Mapper.Map<Product, ProductViewModel>(product);
                newItem.Quantity = quantity;
                cart.Add(newItem);
            }

            Session[CommonConstants.SessionCart] = cart;
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteItem(int productId)
        {
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            if (cartSession != null)
            {
                cartSession.RemoveAll(x => x.ProductId == productId);
                Session[CommonConstants.SessionCart] = cartSession;
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }

        [HttpPost]
        public JsonResult Update(string cartData)
        {
            var cartViewModel = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(cartData);

            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];

            foreach (var item in cartSession)
            {
                foreach (var jitem in cartViewModel)
                {
                    if (item.ProductId == jitem.ProductId)
                    {
                        if (jitem.Quantity > 0)
                        { item.Quantity = jitem.Quantity; }
                    }
                }
            }

            Session[CommonConstants.SessionCart] = cartSession;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult GetUser()
        {
            var model = (AccountLogin)Session[CommonConstants.AccountSession];
            if (model == null)
            {
                return Json(new
                {
                    status = false
                });
            }
            return Json(new
            {
                data = model,
                status = true
            });
        }

        public ActionResult Checkout()
        {
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            if (cart == null)
                return Redirect("/gio-hang.html");
            return View(cart);
        }

        [HttpPost]
        public JsonResult CreateOrder(string orderViewModel)
        {
            var order = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderViewModel);
            var orderDao = new OrderDao();
            var orderNew = new Order();
            orderNew.CreatedDate = DateTime.Now;
            orderNew.CustomerAddress = order.CustomerAddress;
            orderNew.CustomerMobile = order.CustomerMobile;
            orderNew.CustomerName = order.CustomerName;
            orderNew.CustomerEmail = order.CustomerEmail;
            orderNew.CustomerMessage = order.CustomerMessage;
            orderNew.PaymentMethod = order.PaymentMethod;
            orderNew.PaymentStatus = order.PaymentStatus;
            orderNew.Status = true;
            orderDao.Insert(orderNew);
            var detailDao = new OrderDetailDao();
            var sessionCart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            decimal total = 0;
            foreach (var item in sessionCart)
            {
                var detail = new OrderDetail();
                detail.OrderID = orderNew.ID;
                detail.ProductID = item.ProductId;
                detail.Quantitty = item.Quantity;
                if (item.Product.PromotionPrice.HasValue)
                {
                    detail.Price = item.Product.PromotionPrice.Value;
                    total += (item.Product.PromotionPrice.GetValueOrDefault(0) * item.Quantity);
                }
                else
                {
                    detail.Price = item.Product.Price;
                    total += (item.Product.Price * item.Quantity);
                }
                detailDao.Insert(detail);
            }

            string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/template/neworder.html"));
            content = content.Replace("{{CustomerName}}", order.CustomerName);
            content = content.Replace("{{Phone}}", order.CustomerMobile);
            content = content.Replace("{{Email}}", order.CustomerEmail);
            content = content.Replace("{{Address}}", order.CustomerAddress);
            content = content.Replace("{{Total}}", total.ToString("N0"));
            MailHelper.SendMail(order.CustomerEmail, "Thông tin đơn đặt hàng từ BookStore", content);
            Session[CommonConstants.SessionCart] = null;
            return Json(new
            {
                status = true
            });
        }
    }
}