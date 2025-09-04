using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class GenderViewModel : BaseViewModel
    {
        #region Constructors
        public GenderViewModel()
        {

        }
        #endregion

        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "This field is required.")]
        public int ID { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "This field is required.")]
        public string GenderName { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "This field is required.")]
        public string Description { get; set; }
        #endregion

        #region Dropdowns

        #endregion
    }
}
