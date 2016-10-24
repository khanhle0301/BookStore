using System.ComponentModel.DataAnnotations;

namespace BookStore.Web.Models
{
    public class BannerViewModel
    {
        public int ID { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập tên")]
        [Display(Name = "Tên")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập hình ảnh")]
        [Display(Name = "Hình ảnh")]
        public string Image { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập url")]
        public string Url { set; get; }

        public string Type { set; get; }
    }
}