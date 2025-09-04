using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models.DataModels
{
    [Table("Role", Schema = "dbo")]
    public class RoleModel : BaseModel
    {
        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "int")]
        public int ID { get; set; }

        [Column("RoleName", TypeName = "varchar(200)")]
        public string RoleName { get; set; }

        [Column("Description", TypeName = "varchar(500)")]
        public string Description { get; set; }
        #endregion
    }
}
