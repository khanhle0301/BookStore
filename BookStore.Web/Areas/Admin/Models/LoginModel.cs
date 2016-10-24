using System.ComponentModel.DataAnnotations;

namespace BookStore.Web.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tài khoản.")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        public string PassWord { set; get; }
    }
}