using eGift.Admin.MVC.Models.ViewModels;
using System.Collections.Generic;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class AddressListViewModel
    {
        #region Constructors
        public AddressListViewModel()
        {
            AddressList = new List<AddressViewModel>();
            AddressModel = new AddressViewModel();

        }
        #endregion

        #region List View Models
        public List<AddressViewModel> AddressList { get; set; }
        #endregion

        #region Reference View Models
        public AddressViewModel AddressModel { get; set; }
        #endregion


    }
}
