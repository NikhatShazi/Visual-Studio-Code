using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class SigninViewModel
    {
        #region View Models
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$", ErrorMessage = "● At least one upper case letter(A-Z).<br>" +
       "● At least one lower case letter(a-z).<br>" +
       "● At least one digit(0,1,2...).<br>" +
       "● At least one special character(!@#$%&*).<br>" +
       "● Minimum six in length.")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required.")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        #endregion
    }
}
