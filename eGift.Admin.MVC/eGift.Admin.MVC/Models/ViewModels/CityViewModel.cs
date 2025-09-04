using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class CityViewModel : BaseViewModel
    {
        #region Constructors
        public CityViewModel()
        {
            StateList = new SelectList("");
        }
        #endregion

        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "This field is required.")]
        public int ID { get; set; }

        [Display(Name = "City Code")]
        [Required(ErrorMessage = "This field is required.")]
        public string CityCode { get; set; }

        [Display(Name = "City Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string CityName { get; set; }

        //Foreign Key
        [Display(Name = "State")]
        [Required(ErrorMessage = "This field is required.")]
        public int StateId { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        #endregion

        #region View Models
        [Display(Name = "State")]
        public string StateName { get; set; }
        #endregion

        #region Dropdowns
        public SelectList StateList { get; set; }
        #endregion
    }
}
