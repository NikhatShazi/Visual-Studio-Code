using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models.DataModels
{
    [Table("Employee", Schema = "dbo")]

    public class EmployeeModel : BaseModel
    {
        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "int")]
        public int ID { get; set; }

        [Column("FirstName", TypeName = "varchar(200)")]
        public string FirstName { get; set; }

        [Column("LastName", TypeName = "varchar(200)")]
        public string LastName { get; set; }

        [Column("DateOfBirth", TypeName = "datetime")]
        public DateTime DateOfBirth { get; set; }

        [Column("GenderId", TypeName = "int")]
        public int GenderId { get; set; }

        [Column("Mobile", TypeName = "varchar(50)")]
        public string Mobile { get; set; }

        [Column("Email", TypeName = "varchar(200)")]
        public string Email { get; set; }

        [Column("AddressId", TypeName = "int")]
        public Nullable<int> AddressId { get; set; }

        [Column("IsActive", TypeName = "bit")]
        public bool IsActive { get; set; }

        [Column("ProfileImagePath", TypeName = "varchar(500)")]
        public string ProfileImagePath { get; set; }

        [Column("ProfileImageData", TypeName = "BLOB")]
        public byte[] ProfileImageData { get; set; }

        [Column("RoleId", TypeName = "int")]
        public int RoleId { get; set; }

        [Column("IsDefault", TypeName = "bit")]
        public bool IsDefault { get; set; }
        #endregion

        #region View Models
        [NotMapped]
        public int Age { get; set; }

        [NotMapped]
        public string AddressName { get; set; }

        [NotMapped]
        public string GenderName { get; set; }

        [NotMapped]
        public string RoleName { get; set; }

        [NotMapped]
        public IFormFile ProfileImage { get; set; }
        #endregion
    }
}
