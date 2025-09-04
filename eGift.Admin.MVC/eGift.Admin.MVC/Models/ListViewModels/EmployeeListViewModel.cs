using eGift.Admin.MVC.Models.ViewModels;
using System.Collections.Generic;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class EmployeeListViewModel
    {
        #region Constructors
        public EmployeeListViewModel()
        {
            EmployeeList = new List<EmployeeViewModel>();
            EmployeeModel = new EmployeeViewModel();
        }
        #endregion

        #region List View Models
        public List<EmployeeViewModel> EmployeeList { get; set; }
        #endregion

        #region Reference View Models
        public EmployeeViewModel EmployeeModel { get; set; }
        #endregion
    }
}
