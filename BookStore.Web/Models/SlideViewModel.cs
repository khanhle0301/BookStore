using System.ComponentModel.DataAnnotations;

namespace BookStore.Web.Models
{
    public class SlideViewModel
    {
        public int ID { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập tên")]
        [Display(Name = "Tên")]
        public string Name { set; get; }

        public string Description { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập hình ảnh")]
        [Display(Name = "Hình ảnh")]
        public string Image { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập url")]
        public string Url { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập thứ tự")]
        [Display(Name = "Thứ tự")]
        public int? DisplayOrder { set; get; }

        [Display(Name = "Trạng thái")]
        public bool Status { set; get; }
    }
}