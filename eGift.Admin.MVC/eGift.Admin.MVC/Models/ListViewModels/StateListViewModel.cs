using eGift.Admin.MVC.Models.ViewModels;
using System.Collections.Generic;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class StateListViewModel
    {
        #region Constructors
        public StateListViewModel()
        {
            StateList = new List<StateViewModel>();
            StateModel = new StateViewModel();
        }
        #endregion

        #region List View Models
        public List<StateViewModel> StateList { get; set; }
        #endregion

        #region Reference View Models
        public StateViewModel StateModel { get; set; }
        #endregion


    }
}
