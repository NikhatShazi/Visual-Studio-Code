using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models.DataModels
{
    [Table("Address", Schema = "dbo")]
    public class AddressModel : BaseModel
    {
        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "int")]
        public int ID { get; set; }

        [Column("Street1", TypeName = "varchar(200)")]
        public  string Street1 { get; set; }

        [Column("Street2", TypeName = "varchar(200)")]
        public string Street2 { get; set; }

        [Column("CountryId", TypeName = "int")]
        public int CountryId { get; set; }

        [Column("StateId", TypeName = "int")]
        public int StateId { get; set; }

        [Column("CityId", TypeName = "int")]
        public int CityId { get; set; }

        [Column("Pincode", TypeName = "varchar(50)")]
        public string Pincode { get; set; }
        #endregion

        #region View Models
        [NotMapped]
        public string CityName { get; set; }
        [NotMapped]
        public string StateName { get; set; }
        [NotMapped]
        public string CountryName { get; set; }
        #endregion   
    }
}
