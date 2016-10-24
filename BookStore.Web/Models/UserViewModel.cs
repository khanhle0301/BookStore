using System.ComponentModel.DataAnnotations;

namespace BookStore.Web.Models
{
    public class UserViewModel
    {
        public int ID { set; get; }

        public string UserName { set; get; }

        public string Password { set; get; }

        public int GroupID { set; get; }

        [Required(ErrorMessage = "Nhập tên")]
        [Display(Name = "Tên")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Nhập địa chỉ")]
        [Display(Name = "Địa chỉ")]
        public string Address { set; get; }

        [Required(ErrorMessage = "Nhập email")]
        public string Email { set; get; }

        [Required(ErrorMessage = "Nhập địa chỉ")]
        [Display(Name = "Số điện thoại")]
        public string Phone { set; get; }
    }
}