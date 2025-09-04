using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models.DataModels
{
    [Table("SubCategory", Schema = "dbo")]
    public class SubCategoryModel : BaseModel
    {
        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "int")]
        public int ID { get; set; }

        [Column("CategoryId", TypeName = "int")]
        public int CategoryId { get; set; }

        [Column("SubCategoryName", TypeName = "varchar(200)")]
        public string SubCategoryName { get; set; }

        [Column("Description", TypeName = "varchar(500)")]
        public string Description { get; set; }
        #endregion

        #region View Models
        [NotMapped]
        public string CategoryName { get; set; }
        #endregion
    }
}
