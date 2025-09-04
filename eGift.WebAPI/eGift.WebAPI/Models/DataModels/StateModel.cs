using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models.DataModels
{
    [Table("State", Schema = "dbo")]
    public class StateModel : BaseModel
    {
        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "int")]
        public int ID { get; set; }

        [Column("StateCode", TypeName = "varchar(50)")]
        public string StateCode { get; set; }

        [Column("StateName", TypeName = "varchar(200)")]
        public string StateName { get; set; }

        [Column("CountryId", TypeName = "int")]
        public int CountryId { get; set; }

        [Column("Description", TypeName = "varchar(500)")]
        public string Description { get; set; }
        #endregion

        #region View Models
        [NotMapped]
        public string CountryName { get; set; }
        #endregion
    }
}
