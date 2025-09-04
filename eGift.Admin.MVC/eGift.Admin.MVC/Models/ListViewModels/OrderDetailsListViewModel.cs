using eGift.Admin.MVC.Models.ViewModels;
using System.Collections.Generic;

namespace eGift.Admin.MVC.Models.ListViewModels
{
    public class OrderDetailsListViewModel
    {
        #region Constructors
        public OrderDetailsListViewModel()
        {
            OrderDetailsList = new List<OrderDetailsViewModel>();
            OrderDetailsModel = new OrderDetailsViewModel();
        }
        #endregion

        #region List View Models
        public List<OrderDetailsViewModel> OrderDetailsList { get; set; }
        #endregion

        #region Reference View Models
        public OrderDetailsViewModel OrderDetailsModel { get; set; }
        #endregion


    }
}
