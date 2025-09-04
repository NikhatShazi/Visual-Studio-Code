using eGift.Admin.MVC.Models.ViewModels;
using System.Collections.Generic;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class GenderListViewModel
    {
        #region Constructors
        public GenderListViewModel()
        {
            GenderList = new List<GenderViewModel>();
            GenderModel = new GenderViewModel();
            
        }
        #endregion

        #region List View Models
        public List<GenderViewModel> GenderList { get; set; }
        #endregion

        #region Reference View Models
        public  GenderViewModel GenderModel  { get; set; }
        #endregion


    }
}
