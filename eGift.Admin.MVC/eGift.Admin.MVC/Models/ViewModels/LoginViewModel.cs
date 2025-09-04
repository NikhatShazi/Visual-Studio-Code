using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class LoginViewModel
    {
        #region Data Models
        [Display(Name = "ID")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Reference")]
        public int RefId { get; set; }

        [Display(Name = "Reference Type")]
        public string RefType { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage ="This field is required.")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$", ErrorMessage = "● At least one upper case letter(A-Z).<br>" +
       "● At least one lower case letter(a-z).<br>" +
       "● At least one digit(0,1,2...).<br>" +
       "● At least one special character(!@#$%&*).<br>" +
       "● Minimum six in length.")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="This field is required.")]
        public string Password { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage ="This field is required.")]
        public int RoleId { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Login Date")]
        public Nullable<DateTime> LoginDate { get; set; }

        [Display(Name = "Last Login Date")]
        public Nullable<DateTime> LastLoginDate { get; set; }
        #endregion

        #region View Models
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage ="This field is required.")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        #endregion
    }
}
