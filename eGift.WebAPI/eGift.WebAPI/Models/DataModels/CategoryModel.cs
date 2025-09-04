using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models.DataModels
{
    [Table("Category", Schema = "dbo")]
    public class CategoryModel : BaseModel
    {
        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "int")]
        public int ID { get; set; }

        [Column("CategoryName", TypeName = "varchar(200)")]
        public string CategoryName { get; set; }

        [Column("Description", TypeName = "varchar(500)")]
        public string Description { get; set; }
        #endregion
    }
}

