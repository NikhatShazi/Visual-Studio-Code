using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class AddressViewModel : BaseViewModel
    {
        #region Constructors
        public AddressViewModel()
        {
            CountryList = new SelectList("");
            StateList = new SelectList("");
            CityList = new SelectList("");
        }
        #endregion

        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "This field is required.")]
        public int ID { get; set; }

        [Display(Name = "Street1")]
        public string Street1 { get; set; }

        [Display(Name = "Street2")]
        public string Street2 { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "This field is required.")]
        public int CountryId { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "This field is required.")]
        public int StateId { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "This field is required.")]
        public int CityId { get; set; }

        [Display(Name = "Pincode")]
        public string Pincode { get; set; }
        #endregion

        #region View Models
        [Display(Name = "City")]
        public string CityName { get; set; }

        [Display(Name = "State")]
        public string StateName { get; set; }

        [Display(Name = "Country")]
        public string CountryName { get; set; }
        #endregion

        #region Dropdowns
        public SelectList CountryList { get; set; }
        public SelectList StateList { get; set; }
        public SelectList CityList { get; set; }
        #endregion

    }
}
