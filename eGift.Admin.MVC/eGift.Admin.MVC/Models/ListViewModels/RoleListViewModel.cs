using eGift.Admin.MVC.Models.ViewModels;
using System.Collections.Generic;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class RoleListViewModel
    {
        #region Constructors
        public RoleListViewModel()
        {
            RoleList = new List<RoleViewModel>();
            RoleModel = new RoleViewModel();
        }
        #endregion

        #region List View Models
        public  List<RoleViewModel> RoleList { get; set; }
        #endregion

        #region Reference View Models
        public RoleViewModel RoleModel { get; set; }
        #endregion


    }
}
