using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class SubCategoryViewMode : BaseViewModel
    {
        #region Constructors
        public SubCategoryViewMode()
        {
            CategoryList = new SelectList("");
        }
        #endregion

        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "This field is required.")]
        public int ID { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "This field is required.")]
        public int CategoryId { get; set; }

        [Display(Name = "Sub Category")]
        [Required(ErrorMessage = "This field is required.")]
        public string SubCategoryName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        #endregion

        #region View Models
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
        #endregion

        #region Dropdowns
        public SelectList CategoryList { get; set;}
        #endregion
    }
}
