using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace eGift.Store.Razor.Models.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        #region Constructors
        public OrderViewModel()
        {

        }
        #endregion

        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "This field is required.")]
        public int ID { get; set; }

        [Display(Name = "Customer Id")]
        [Required(ErrorMessage = "This field is required.")]
        public int CustomerId { get; set; }

        [Display(Name = "Total Amount")]
        [Required(ErrorMessage = "This field is required.")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Total Discount")]
        public Nullable<decimal> TotalDiscount { get; set; }

        [Display(Name = "Total Tax")]
        public Nullable<decimal> TotalTax { get; set; }

        [Display(Name = "Order Number")]
        [Required(ErrorMessage = "This field is required.")]
        public string OrderNumber { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Display(Name = "Dispatch Date")]
        public Nullable<DateTime> DispatchDate { get; set; }

        [Display(Name = "Shipped Date")]
        public Nullable<DateTime> ShippedDate { get; set; }

        [Display(Name = "Delivery Date")]
        public Nullable<DateTime> DeliveryDate { get; set; }

        [Display(Name = "Cancel Date")]
        public Nullable<DateTime> CancelDate { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "This field is required.")]
        public int StatusId { get; set; }


        #endregion

        #region View Models
        [Display(Name = "StatusName")]
        public string StatusName { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Contact No")]
        public string ContactNo { get; set; }

        #endregion

        #region Dropdowns

        #endregion
    }
}
