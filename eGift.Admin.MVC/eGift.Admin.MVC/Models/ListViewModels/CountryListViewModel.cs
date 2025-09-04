using eGift.Admin.MVC.Models.ViewModels;
using System.Collections.Generic;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class CountryListViewModel
    {
        #region Constructors
        public CountryListViewModel()
        {
            CountryList = new List<CountryViewModel>();
            CountryModel = new CountryViewModel();
        }
        #endregion

        #region List View Models
        public List<CountryViewModel> CountryList { get; set; }
        #endregion

        #region Reference View Models
        public CountryViewModel CountryModel { get; set; }
        #endregion
    }
}
