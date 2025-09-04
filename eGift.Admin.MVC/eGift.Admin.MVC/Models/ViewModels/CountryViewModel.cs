using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class CountryViewModel : BaseViewModel
    {
        #region Constructors
        public CountryViewModel()
        {
            
        }
        #endregion

        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "This field is required.")]
        public int ID { get; set; }

        [Display(Name = "Country Code")]
        [Required(ErrorMessage = "This field is required.")]
        public string CountryCode { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "This field is required.")]
        public string CountryName { get; set; }


        [Display(Name = "Description")]
        public string Description { get; set; }
        #endregion
               
        #region Dropdowns

        #endregion
    }
}
