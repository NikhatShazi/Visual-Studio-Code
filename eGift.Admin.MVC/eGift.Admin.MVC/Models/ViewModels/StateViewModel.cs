using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using eGift.Admin.MVC.Models.ListViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class StateViewModel: BaseViewModel
    {
        #region Constructors
        public StateViewModel()
        {
            CountryList = new SelectList("");
        }
        #endregion

        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "This field is required.")]
        public int ID { get; set; }

        [Display(Name = "State Code")]
        [Required(ErrorMessage = "This field is required.")]
        public string StateCode { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "This field is required.")]
        public string StateName { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "This field is required.")]
        public int CountryId { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        #endregion

        #region View Models
        [Display(Name = "Country")]
        public string CountryName { get; set; }

        #endregion

        #region Dropdowns
        public SelectList CountryList { get; set; }
        #endregion
    }
}
