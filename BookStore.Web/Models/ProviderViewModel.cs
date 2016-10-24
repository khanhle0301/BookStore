using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Web.Models
{
    public class ProviderViewModel
    {
        public int ID { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập tên")]
        [Display(Name = "Tên danh mục")]
        public string Name { set; get; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { set; get; }

        [Display(Name = "Tạo bởi")]
        public string CreatedBy { set; get; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedDate { set; get; }

        [Display(Name = "Cập nhật bởi")]
        public string UpdatedBy { set; get; }

        [Display(Name = "Từ khóa")]
        public string MetaKeyword { set; get; }

        [Display(Name = "Description")]
        public string MetaDescription { set; get; }

        [Display(Name = "Trạng thái")]
        public bool Status { set; get; }
    }
}