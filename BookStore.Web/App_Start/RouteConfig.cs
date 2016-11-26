using System.Web.Mvc;
using System.Web.Routing;

namespace BookStore.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
           name: "CancelOrder",
           url: "thanh-toan-that-bai.html",
           defaults: new { controller = "ShoppingCart", action = "CancelOrder", alias = UrlParameter.Optional },
           namespaces: new string[] { "BookStore.Web.Controllers" }
           );

            routes.MapRoute(
           name: "Checkout",
           url: "thanh-toan.html",
           defaults: new { controller = "ShoppingCart", action = "Checkout", alias = UrlParameter.Optional },
           namespaces: new string[] { "BookStore.Web.Controllers" }
           );

            routes.MapRoute(
            name: "Login",
            url: "dang-nhap.html",
            defaults: new { controller = "Account", action = "Login", alias = UrlParameter.Optional },
            namespaces: new string[] { "BookStore.Web.Controllers" }
            );

            routes.MapRoute(
           name: "Register",
           url: "dang-ky.html",
           defaults: new { controller = "Account", action = "Register", alias = UrlParameter.Optional },
           namespaces: new string[] { "BookStore.Web.Controllers" }
           );

            routes.MapRoute(
            name: "About",
            url: "pages/gioi-thieu.html",
            defaults: new { controller = "Page", action = "About", alias = UrlParameter.Optional },
            namespaces: new string[] { "BookStore.Web.Controllers" }
            );

            routes.MapRoute(
           name: "Search",
           url: "tim-kiem.html",
           defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
           namespaces: new string[] { "BookStore.Web.Controllers" }
           );

            routes.MapRoute(
           name: "Contact",
           url: "pages/lien-he.html",
           defaults: new { controller = "Page", action = "Contact", alias = UrlParameter.Optional },
           namespaces: new string[] { "BookStore.Web.Controllers" }
           );

            routes.MapRoute(
              name: "Post",
              url: "blogs/news.html",
              defaults: new { controller = "Post", action = "Index", id = UrlParameter.Optional },
              namespaces: new string[] { "BookStore.Web.Controllers" }
          );

            routes.MapRoute(
               name: "Gio hang",
               url: "gio-hang.html",
               defaults: new { controller = "ShoppingCart", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "BookStore.Web.Controllers" }
           );

            routes.MapRoute(
               name: "ProductAll",
               url: "sach/all.html",
               defaults: new { controller = "Product", action = "All", id = UrlParameter.Optional },
               namespaces: new string[] { "BookStore.Web.Controllers" }
           );

            routes.MapRoute(
              name: "Ban chay",
              url: "sach/ban-chay.html",
              defaults: new { controller = "Product", action = "Banchay", id = UrlParameter.Optional },
              namespaces: new string[] { "BookStore.Web.Controllers" }
          );

            routes.MapRoute(
             name: "ProductNews",
             url: "sach/news.html",
             defaults: new { controller = "Product", action = "News", id = UrlParameter.Optional },
             namespaces: new string[] { "BookStore.Web.Controllers" }
         );

            routes.MapRoute(
        name: "Pages",
        url: "pages/{alias}.html",
        defaults: new { controller = "Page", action = "Index", alias = UrlParameter.Optional },
        namespaces: new string[] { "BookStore.Web.Controllers" }
    );

            routes.MapRoute(
        name: "Post Detail",
        url: "blogs/news/{alias}-{id}.html",
        defaults: new { controller = "Post", action = "Detail", id = UrlParameter.Optional },
        namespaces: new string[] { "BookStore.Web.Controllers" }
    );

            routes.MapRoute(
           name: "Product Category",
           url: "sach/{alias}.html",
           defaults: new { controller = "Product", action = "ProductCategory", alias = UrlParameter.Optional },
           namespaces: new string[] { "BookStore.Web.Controllers" }
       );

            routes.MapRoute(
        name: "Product Detail",
        url: "sach/{catealias}/{alias}-{id}.html",
        defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
        namespaces: new string[] { "BookStore.Web.Controllers" }
    );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "BookStore.Web.Controllers" }
            );
        }
    }
}