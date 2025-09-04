using eGift.Store.Razor.Models.ViewModels;
using System.Collections.Generic;

namespace eGift.Store.Razor.Models.ListViewModels
{
    public class OrderListViewModel
    {
        #region Constructors
        public OrderListViewModel()
        {
            OrderList = new List<OrderViewModel>();
            OrderModel = new OrderViewModel();
        }
        #endregion

        #region List View Models
        public List<OrderViewModel> OrderList { get; set; }
        #endregion

        #region Reference View Models
        public OrderViewModel OrderModel { get; set; }
        #endregion


    }

}
