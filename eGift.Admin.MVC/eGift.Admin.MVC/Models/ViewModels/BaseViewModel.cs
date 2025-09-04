using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace eGift.Admin.MVC.Models.ViewModels
{
    public class BaseViewModel
    {
        #region Data Models
        [Display(Name = "Deleted")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Created By")]
        public int CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Updated By")]
        public Nullable<int> UpdatedBy { get; set; }

        [Display(Name = "Updated Date")]
        public Nullable<DateTime> UpdatedDate { get; set; }
        #endregion
    }
}
