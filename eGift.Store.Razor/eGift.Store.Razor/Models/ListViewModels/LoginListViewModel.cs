using eGift.Store.Razor.Models.ViewModels;
using System.Collections.Generic;

namespace eGift.Store.Razor.Models.ListViewModels
{
    public class LoginListViewModel
    {
        #region Constructors
        public LoginListViewModel()
        {
            LoginList = new List<LoginViewModel>();
            LoginModel = new LoginViewModel();
        }
        #endregion

        #region List View Models
        public List<LoginViewModel> LoginList { get; set; }
        #endregion

        #region Reference View Models
        public LoginViewModel LoginModel { get; set; }
        #endregion


    }

}
