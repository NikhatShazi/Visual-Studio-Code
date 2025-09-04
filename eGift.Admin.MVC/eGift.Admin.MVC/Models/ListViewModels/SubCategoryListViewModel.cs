using eGift.Admin.MVC.Models.ViewModels;
using System.Collections.Generic;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class SubCategoryListViewModel
    {
        #region Constructors
        public SubCategoryListViewModel()
        {
            SubCategoryList = new List<SubCategoryViewMode>();
            SubCategoryModel = new SubCategoryViewMode();
        }
        #endregion

        #region List View Models
        public List<SubCategoryViewMode> SubCategoryList { get; set; }
        #endregion

        #region Reference View Models
        public SubCategoryViewMode SubCategoryModel { get; set; }
        #endregion
    }
}

