using BookStore.Model.Models;
using System.Collections.Generic;

namespace BookStore.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Slide> Slide { set; get; }
        public IEnumerable<ProductCategory> Category { set; get; }
        public IEnumerable<Product> ProductNew { set; get; }
        public IEnumerable<Product> ProductOnsale { set; get; }
        public IEnumerable<Product> ProductHot { set; get; }
        public IEnumerable<Product> GetAllHome { set; get; }
        public Banner SlideBanner { set; get; }
        public Banner QuangCao1 { set; get; }
        public Banner QuangCao2 { set; get; }
    }
}