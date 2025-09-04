using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.WebAPI.Models.DataModels
{
    [Table("Login", Schema = "dbo")]

    public class LoginModel
    {
        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "int")]
        public int ID { get; set; }

        [Column("RefId", TypeName = "int")]
        public int RefId { get; set; }

        [Column("RefType", TypeName = "varchar(200)")]
        public string RefType { get; set; }

        [Column("UserName", TypeName = "varchar(200)")]
        public string UserName { get; set; }

        [Column("Password", TypeName = "varchar(50)")]
        public string Password { get; set; }

        [Column("RoleId", TypeName = "int")]
        public int RoleId { get; set; }

        [Column("IsActive", TypeName = "bit")]
        public bool IsActive { get; set; }

        [Column("LoginDate", TypeName = "datetime")]
        public Nullable<DateTime> LoginDate { get; set; }

        [Column("LastLoginDate", TypeName = "datetime")]
        public Nullable<DateTime> LastLoginDate { get; set; }
        #endregion


        #region View Models
        [NotMapped]
        public string ConfirmPassword { get; set; }
        #endregion
    }
}
