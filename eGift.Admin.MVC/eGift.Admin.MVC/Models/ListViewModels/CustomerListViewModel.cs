﻿using eGift.Admin.MVC.Models.ViewModels;
using System.Collections.Generic;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class CustomerListViewModel
    {
        #region Constructors
        public CustomerListViewModel()
        {
            CustomerList=new List<CustomerViewModel>();
            CustomerModel=new CustomerViewModel();  
        }

        #endregion

        #region List View Models
        public List<CustomerViewModel> CustomerList { get; set; }
        #endregion

        #region Reference View Models
        public CustomerViewModel CustomerModel { get; set; }
        #endregion
    }
}
