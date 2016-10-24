using BookStore.Model.Models;
using System.Collections.Generic;

namespace BookStore.Web.Models
{
    public class FooterHomeViewModel
    {
        public ContactDetail ContactDetail { set; get; }
        public IEnumerable<Post> News { set; get; }
        public IEnumerable<Page> Page { set; get; }
    }
}