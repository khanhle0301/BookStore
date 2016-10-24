using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Model.Models
{
    [Table("ContactDetails")]
    public class ContactDetail
    {
        [Key]
        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string ID { set; get; }

        [MaxLength(250)]
        [Required]
        public string CompanyName { set; get; }

        [MaxLength(50)]
        public string CompanyPhone { set; get; }

        [MaxLength(250)]
        public string CompanyEmail { set; get; }

        [MaxLength(250)]
        public string CompanyWebsite { set; get; }

        [MaxLength(250)]
        public string CompanyAddress { set; get; }

        [MaxLength(50)]
        public string CompanyFax { set; get; }

        public double? Lat { set; get; }

        public double? Lng { set; get; }
    }
}