using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Model.Models
{
    [Table("Credentials")]
    public class Credential
    {
        [Key]
        [Column(Order = 1)]        
        public int UserGroupID { set; get; }

        [Key]
        [Column(TypeName = "varchar", Order = 2)]
        [MaxLength(50)]
        public string RoleID { set; get; }    

        [ForeignKey("UserGroupID")]
        public virtual UserGroup UserGroup { set; get; }

        [ForeignKey("RoleID")]
        public virtual Role Role { set; get; }
    }
}