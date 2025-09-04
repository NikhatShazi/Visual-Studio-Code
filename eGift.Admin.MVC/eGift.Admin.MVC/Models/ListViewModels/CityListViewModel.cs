using eGift.Admin.MVC.Models.ViewModels;
using System.Collections.Generic;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class CityListViewModel
    {
        #region Constructors
        public CityListViewModel()
        {
            CityList = new List<CityViewModel>();
            CityModel = new CityViewModel();
        }
        #endregion

        #region List View Models
        public List<CityViewModel> CityList { get; set; }
        #endregion

        #region Reference View Models
        public CityViewModel CityModel { get; set; }
        #endregion


    }
}
