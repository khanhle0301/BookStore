using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Web.Models
{
    public class PostViewModel
    {
        public int ID { set; get; }

        [Required(ErrorMessage = "Nhập tiều đề")]
        [Display(Name = "Tiêu đề")]
        public string Name { set; get; }

        public string Alias { set; get; }      

        [Required(ErrorMessage = "Chọn hình ảnh")]
        [Display(Name = "Hình ảnh")]
        public string Image { set; get; }

        [Required(ErrorMessage = "Nhập mô tả ngắn")]
        [Display(Name = "Mô tả ngắn")]
        [MaxLength(500)]
        public string Description { set; get; }

        [Required(ErrorMessage = "Nhập chi tiết")]
        [Display(Name = "Chi tiết")]
        public string Content { set; get; }

        public int? ViewCount { set; get; }

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