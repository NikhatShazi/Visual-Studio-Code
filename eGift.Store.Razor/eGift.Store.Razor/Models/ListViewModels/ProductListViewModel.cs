using eGift.Store.Razor.Models.ViewModels;
using System.Collections.Generic;

namespace eGift.Store.Razor.Models.ListViewModels
{
    public class ProductListViewModel
    {
        #region Constructors
        public ProductListViewModel()
        {
            ProductList = new List<ProductViewModel>();
            ProductModel = new ProductViewModel();
        }
        #endregion

        #region List View Models
        public List<ProductViewModel> ProductList { get; set; }
        #endregion

        #region Reference View Models
        public ProductViewModel ProductModel { get; set; }
        #endregion


    }

}
