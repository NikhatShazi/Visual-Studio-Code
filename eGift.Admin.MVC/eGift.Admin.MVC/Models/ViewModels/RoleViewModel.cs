using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class RoleViewModel : BaseViewModel
    {
        #region Constructors
        public RoleViewModel()
        {

        }
        #endregion

        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "This field is required.")]
        public int ID { get; set; }

        [Display(Name = "Role Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string RoleName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        #endregion

        #region Dropdowns

        #endregion
    }
}
