using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models.DataModels
{
    [Table("Country", Schema = "dbo")]
    public class CountryModel : BaseModel
    {
        #region Data Model
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "int")]
        public int ID { get; set; }


        [Column("CountryCode", TypeName = "varchar(50)")]
        public string CountryCode { get; set; }


        [Column("CountryName", TypeName = "varchar(200)")]
        public string CountryName { get; set; }


        [Column("Description", TypeName = "varchar(500)")]
        public string Description { get; set; }
        #endregion
    }
}
