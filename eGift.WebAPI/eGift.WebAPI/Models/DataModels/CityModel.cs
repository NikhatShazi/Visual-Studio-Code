
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models.DataModels
{
    [Table("City", Schema = "dbo")]
    public class CityModel : BaseModel
    {
        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "int")]
        public int ID { get; set; }

        [Column("CityCode", TypeName = "varchar(50)")]
        public string CityCode { get; set; }

        [Column("CityName", TypeName = "varchar(50)")]
        public string CityName { get; set; }

        //Foreign Key
        [Column("StateId", TypeName = "int")]
        public int StateId { get; set; }

        [Column("Description", TypeName = "varchar(50)")]
        public string Description { get; set; }
        #endregion

        #region View Models
        [NotMapped]
        public string StateName { get; set; }
        #endregion


    }
}
