using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models.DataModels
{
    [Table("Gender", Schema = "dbo")]

    public class GenderModel : BaseModel
    {
        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "int")]
        public int ID { get; set; }

        [Column("GenderName", TypeName = "varchar(50)")]
        public string GenderName { get; set; }

        [Column("Description", TypeName = "varchar(500)")]
        public string Description { get; set; }
        #endregion

    }
}
