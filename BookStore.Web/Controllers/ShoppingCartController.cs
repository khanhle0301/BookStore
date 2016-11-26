using AutoMapper;
using BookStore.Common;
using BookStore.Model.Dao;
using BookStore.Model.Models;
using BookStore.Web.Common;
using BookStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            var detail = new OrderDetail();
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
               
            }
            decimal totals = total * 100;
            string text = "";
            foreach (var item in sessionCart)
            {
                text = text + item.Quantity + "x" + item.Product.Name + ";";
            }
            if (order.PaymentMethod == "Thanh toán khi giao hàng")
            {
                detailDao.Insert(detail);
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
            else
            {             
                Session.Add(CommonConstants.SessionOrder, orderNew);
                string SECURE_SECRET = "A3EFDFABA8653DF2342E8DAC29B51AF0";
                // Khoi tao lop thu vien va gan gia tri cac tham so gui sang cong thanh toan
                VPCRequest conn = new VPCRequest("https://mtf.onepay.vn/onecomm-pay/vpc.op");
                conn.SetSecureSecret(SECURE_SECRET);
                // Add the Digital Order Fields for the functionality you wish to use
                // Core Transaction Fields
                conn.AddDigitalOrderField("Title", "onepay paygate");
                conn.AddDigitalOrderField("vpc_Locale", "vn");//Chon ngon ngu hien thi tren cong thanh toan (vn/en)
                conn.AddDigitalOrderField("vpc_Version", "2");
                conn.AddDigitalOrderField("vpc_Command", "pay");
                conn.AddDigitalOrderField("vpc_Merchant", "ONEPAY");
                conn.AddDigitalOrderField("vpc_AccessCode", "D67342C2");
                conn.AddDigitalOrderField("vpc_MerchTxnRef", MaHoaMD5(ngaunhien().ToString()));
                conn.AddDigitalOrderField("vpc_OrderInfo", text);
                conn.AddDigitalOrderField("vpc_Amount", totals.ToString());
                conn.AddDigitalOrderField("vpc_Currency", "VND");
                conn.AddDigitalOrderField("vpc_ReturnURL", Url.Action("ConfirmOrder", "ShoppingCart", null, Request.Url.Scheme));
                // Thong tin them ve khach hang. De trong neu khong co thong tin
                conn.AddDigitalOrderField("vpc_SHIP_Street01", "");
                conn.AddDigitalOrderField("vpc_SHIP_Provice", "");
                conn.AddDigitalOrderField("vpc_SHIP_City", "");
                conn.AddDigitalOrderField("vpc_SHIP_Country", "Vietnam");
                conn.AddDigitalOrderField("vpc_Customer_Phone", order.CustomerMobile);
                conn.AddDigitalOrderField("vpc_Customer_Email", order.CustomerEmail);
                conn.AddDigitalOrderField("vpc_Customer_Id", "onepay_paygate");
                // Dia chi IP cua khach hang
                conn.AddDigitalOrderField("vpc_TicketNo", "");
                // Chuyen huong trinh duyet sang cong thanh toan
                String url = conn.Create3PartyQueryString();
                return Json(new
                {
                    status = true,
                    urlCheckout = url,
                });
            }           
        }

        private string MaHoaMD5(string input)
        {
            byte[] pass = Encoding.UTF8.GetBytes(input);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(pass);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("X2"));
            return sb.ToString();
        }

        private int ngaunhien()
        {
            Random i = new Random();
            int i2;
            i2 = i.Next(-2147483648, 2147483647);
            return i2;
        }

        public ActionResult ConfirmOrder()
        {
            string SECURE_SECRET = "A3EFDFABA8653DF2342E8DAC29B51AF0";
            string hashvalidateResult = "";
            // Khoi tao lop thu vien
            VPCRequest conn = new VPCRequest("http://onepay.vn");
            conn.SetSecureSecret(SECURE_SECRET);

            // Xu ly tham so tra ve va kiem tra chuoi du lieu ma hoa
            try
            {
                hashvalidateResult = conn.Process3PartyResponse(Request.QueryString);
            }
            catch
            {
                return Redirect("/gio-hang.html");
            }

            // Lay gia tri tham so tra ve tu cong thanh toan
            String vpc_TxnResponseCode = conn.GetResultField("vpc_TxnResponseCode", "Unknown");
            string amount = conn.GetResultField("vpc_Amount", "Unknown");
            string localed = conn.GetResultField("vpc_Locale", "Unknown");
            string command = conn.GetResultField("vpc_Command", "Unknown");
            string version = conn.GetResultField("vpc_Version", "Unknown");
            string cardBin = conn.GetResultField("vpc_Card", "Unknown");
            string orderInfo = conn.GetResultField("vpc_OrderInfo", "Unknown");
            string merchantID = conn.GetResultField("vpc_Merchant", "Unknown");
            string authorizeID = conn.GetResultField("vpc_AuthorizeId", "Unknown");
            string merchTxnRef = conn.GetResultField("vpc_MerchTxnRef", "Unknown");
            string transactionNo = conn.GetResultField("vpc_TransactionNo", "Unknown");
            string txnResponseCode = vpc_TxnResponseCode;
            string message = conn.GetResultField("vpc_Message", "Unknown");

            if (hashvalidateResult == "CORRECTED" && txnResponseCode.Trim() == "0")
            {
                var order = (Order)Session[CommonConstants.SessionOrder];
                var orderNew = new Order();
                order.PaymentStatus = true;
                orderNew = order;
                List<OrderDetail> orderDetails = new List<OrderDetail>();
                var sessionCart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
                foreach (var item in sessionCart)
                {
                    var detail = new OrderDetail();
                    detail.ProductID = item.ProductId;
                    detail.Quantitty = item.Quantity;
                    if (item.Product.PromotionPrice.HasValue)
                    {
                        detail.Price = item.Product.PromotionPrice.Value;
                    }
                    else
                    {
                        detail.Price = item.Product.Price;
                    }                 
                    orderDetails.Add(detail);

                }              
                ViewBag.Amount = decimal.Parse(amount) / 100;
                ViewData["OrderID"] = orderNew.ID;
                ViewData["PaymentMethod"] = orderNew.PaymentMethod;

                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/template/neworder.html"));
                content = content.Replace("{{CustomerName}}", order.CustomerName);
                content = content.Replace("{{Phone}}", order.CustomerMobile);
                content = content.Replace("{{Email}}", order.CustomerEmail);
                content = content.Replace("{{Address}}", order.CustomerAddress);
                content = content.Replace("{{Total}}", (decimal.Parse(amount) / 100).ToString("N0"));
                var adminEmail = ConfigHelper.GetByKey("AdminEmail");
                MailHelper.SendMail(order.CustomerEmail, "Đơn hàng mới từ BookStore", content);
                MailHelper.SendMail(adminEmail, "Đơn hàng mới từ BookStore", content);
             
                Session[CommonConstants.SessionCart] = null;              
                Session[CommonConstants.SessionOrder] = null;
                return View();
            }
            else
            {
                return Redirect("/loi-thanh-toan.html");
            }
        }

        public ActionResult CancelOrder()
        {
            return View();
        }
    } 
}