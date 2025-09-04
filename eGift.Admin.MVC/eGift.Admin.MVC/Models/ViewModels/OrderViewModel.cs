using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Security;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using eGift.Admin.MVC.Helpers;
using eGift.Admin.MVC.Common;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        #region Constructors
        public OrderViewModel()
        {
            //DropDown Initialization
            OrderDetailsList = new List<OrderDetailsViewModel>();
            StatusList = DataSourceHelper.ParseEnumName<Status>();
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
        [Display(Name = "Status")]
        public string StatusName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Customer Name")]
        public string Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Display(Name = "Customer Name")]
        [NotMapped]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Address")]
        [NotMapped]
        public string CustomerAddress { get; set; }

        [Display(Name = "Customer Contact")]
        [NotMapped]
        public string CustomerContact { get; set; }
        #endregion

        #region Reference View Model
        public List<OrderDetailsViewModel> OrderDetailsList { get; set; }
        #endregion

        #region Dropdowns
        [NotMapped]
        public SelectList StatusList { get; set; }
        #endregion
    }
}
