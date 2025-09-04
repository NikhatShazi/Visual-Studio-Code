using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        #region Constructors
        public CategoryViewModel()
        {
            
        }
        #endregion

        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "This field is required.")]
        public int ID { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "This field is required.")]
        public string CategoryName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        #endregion

        #region View Models

        #endregion

        #region Dropdowns

        #endregion
    }
}
