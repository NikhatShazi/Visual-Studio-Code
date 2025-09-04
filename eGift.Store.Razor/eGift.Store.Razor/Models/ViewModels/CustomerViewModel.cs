using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using eGift.Store.Razor.Helpers;
using eGift.Store.Razor.Common;

namespace eGift.Store.Razor.Models.ViewModels
{
    public class CustomerViewModel : BaseViewModel
    {
        #region Constructors
        public CustomerViewModel()
        {
            LoginModel = new LoginViewModel();
            GenderList = DataSourceHelper.ParseEnumName<Gender>();
        }
        #endregion

        #region Data Models
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "This field is required.")]
        public int ID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "This field is required.")]
        public string LastName { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "This field is required.")]
        public Nullable<DateTime> DateOfBirth { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "This field is required.")]
        public int GenderId { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "This field is required.")]
        public string Mobile { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Address")]
        public Nullable<int> AddressId { get; set; }

        [Display(Name = "Active")]
        [Required(ErrorMessage = "This field is required.")]
        public bool IsActive { get; set; }

        [Display(Name = "Profile Image")]
        public string ProfileImagePath { get; set; }

        [Display(Name = "Profile Image")]
        public byte[] ProfileImageData { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "This field is required.")]
        public int RoleId { get; set; }

        [Display(Name = "Default")]
        [Required(ErrorMessage = "This field is required.")]
        public bool IsDefault { get; set; }
        #endregion

        #region View Models
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Display(Name = "Address")]
        public string AddressName { get; set; }

        [Display(Name = "Gender")]
        public string GenderName { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Role")]
        public string RoleName { get; set; }

        [Display(Name = "Profile Image")]
        public IFormFile ProfileImage { get; set; }

        [Display(Name = "Customer Name")]
        [NotMapped]
        public string Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        #endregion

        #region Dropdown Lists
        public SelectList GenderList { get; set; }
        public SelectList RoleList { get; set; }
        #endregion

        #region Reference View Models
        public LoginViewModel LoginModel { get; set; }

        #endregion
    }

}
