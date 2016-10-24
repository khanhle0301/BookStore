using System.ComponentModel.DataAnnotations;

namespace BookStore.Web.Models
{
    public class UserGroupViewModel
    {
        public int ID { set; get; }

        [Required(ErrorMessage = "Nhập tên")]
        public string Name { set; get; }

        public string Role { set; get; }
    }
}