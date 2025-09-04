using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class OrderDetailsViewModel : BaseViewModel
    {
        #region Constructors
        public OrderDetailsViewModel()
        {

        }
        #endregion

        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "This field is required.")]
        public int ID { get; set; }

        [Display(Name = "Order Id")]
        [Required(ErrorMessage = "This field is required.")]
        public int OrderId { get; set; }

        [Display(Name = "Product Id")]
        [Required(ErrorMessage = "This field is required.")]
        public int ProductId { get; set; }

        [Display(Name = "Unit Price")]
        [Required(ErrorMessage = "This field is required.")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "This field is required.")]
        public int Quantity { get; set; }

        [Display(Name = "Discount")]
        public Nullable<decimal> Discount { get; set; }

        [Display(Name = "Tax")]
        public Nullable<decimal> Tax { get; set; }

        [Display(Name = "Net Amount")]
        [Required(ErrorMessage = "This field is required.")]
        public decimal NetAmount { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        #endregion

        #region Dropdowns

        #endregion
    }
}
