using eGift.Admin.MVC.Models.ViewModels;
using System.Collections.Generic;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class CategoryListViewModel
    {
        #region Constructors
        public CategoryListViewModel()
        {
            CategoryList = new List<CategoryViewModel>();
            CategoryModel = new CategoryViewModel();
        }
        #endregion

        #region List View Models
        public List<CategoryViewModel> CategoryList { get; set; }
        #endregion

        #region Reference View Models
        public CategoryViewModel CategoryModel { get; set; }

        #endregion


    }
}
