using BookStore.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Model.Models
{
    [Table("Products")]
    public class Product : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [Required]
        [MaxLength(256)]
        public string Alias { set; get; }

        [Required]
        public int CategoryID { set; get; }

        [Required]
        public int ProviderID { set; get; }

        public int? Quantity { set; get; }

        [MaxLength(256)]
        public string Image { set; get; }

        [Column(TypeName = "xml")]
        public string MoreImages { set; get; }   

        public decimal? PromotionPrice { set; get; }
        
        public decimal Price { set; get; }      

        [MaxLength(500)]
        public string Description { set; get; }

        public string Content { set; get; }

        public bool? HomeFlag { set; get; }

        public bool? SaleFlag { set; get; }

        public bool? HotFlag { set; get; }

        public int? ViewCount { set; get; }       

        [ForeignKey("CategoryID")]
        public virtual ProductCategory ProductCategory { set; get; }

        [ForeignKey("ProviderID")]
        public virtual Provider Provider { set; get; }
    }
}