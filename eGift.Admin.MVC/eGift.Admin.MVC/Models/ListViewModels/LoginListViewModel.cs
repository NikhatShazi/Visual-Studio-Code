using eGift.Admin.MVC.Models.ViewModels;
using System.Collections.Generic;
using System.Threading;

namespace eGift.Admin.MVC.Models.ListViewModels
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
