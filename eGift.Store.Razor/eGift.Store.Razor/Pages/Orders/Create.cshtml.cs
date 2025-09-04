using eGift.Store.Razor.Common;
using eGift.Store.Razor.Helpers;
using eGift.Store.Razor.Models;
using eGift.Store.Razor.Models.ViewModels;
using eGift.Store.Razor.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGift.Store.Razor.Pages.Orders
{
    public class CreateModel : PageModel
    {
        #region Variables
        public WebApiHelper _webApiHelper;
        #endregion

        #region Constructors
        public CreateModel(IConfiguration configuration)
        {
            this._webApiHelper = new WebApiHelper(configuration);
        }
        #endregion

        #region Properties
        [BindProperty]
        public OrderViewModel OrderModel { get; set; }

        [BindProperty]
        public List<OrderDetailsViewModel> OrderDetailsList { get; set; }
        #endregion

        #region Order Actions
        public IActionResult OnGet()
        {
            var orderDetail = new OrderDetailsViewModel();
            OrderModel = new OrderViewModel();
            OrderDetailsList = new List<OrderDetailsViewModel>();
            var loginUserId = HttpContext.Session.GetInt32("UserId");

            //Webapiclient call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Customer", loginUserId));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //De-serialize to model
                CustomerViewModel customer = JsonConvert.DeserializeObject<CustomerViewModel>(response);
                OrderModel.CustomerId = customer.ID;

                OrderModel.OrderNumber = "EG-O-" + RandomNumberOrString.RandomString(3) + "-" + RandomNumberOrString.FourDigitRandomNumber();
                OrderModel.CustomerName = customer.Name;
                OrderModel.Address = customer.AddressName;
                OrderModel.ContactNo = customer.Mobile;

                List<AddToCartModel> addToCartList = HttpContext.Session.GetComplexData<List<AddToCartModel>>("AddToCartList");
                if (addToCartList != null)
                {
                    foreach (var item in addToCartList)
                    {
                        //De-serialize to model
                        orderDetail = new OrderDetailsViewModel();

                        //Webapiclient call
                        var productResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Product", item.Id));
                        if (!string.IsNullOrWhiteSpace(productResponse))
                        {
                            ProductViewModel product = JsonConvert.DeserializeObject<ProductViewModel>(productResponse);

                            orderDetail.ProductId = product.ID;
                            orderDetail.UnitPrice = product.UnitPrice;
                            orderDetail.Quantity = item.Quantity;
                            orderDetail.Discount = product.Discount;
                            orderDetail.Tax = product.Tax ?? 0;
                            orderDetail.NetAmount = (product.UnitPrice + item.Quantity) - (product.Discount ?? 0) + (product.Tax ?? 0);

                            orderDetail.ProductName = product.Name;
                            orderDetail.OrderNumber = OrderModel.OrderNumber;

                            OrderDetailsList.Add(orderDetail);
                        }
                    }
                    OrderModel.TotalAmount = OrderDetailsList.Sum(x => x.NetAmount);
                    OrderModel.TotalDiscount = OrderDetailsList.Sum(x => x.Discount);
                    OrderModel.TotalTax = OrderDetailsList.Sum(x => x.Tax);

                    HttpContext.Session.SetComplexData("Order", OrderModel);
                    HttpContext.Session.SetComplexData("OrderDetails", OrderDetailsList);
                }
                else
                {
                    return RedirectToPage("/Index");
                }
            }

            return Page();
        }

        [BindProperty]
        public OrderViewModel OrderViewModel { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var loginUserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            OrderModel = HttpContext.Session.GetComplexData<OrderViewModel>("Order");
            if (OrderModel != null)
            {
                OrderDetailsList = HttpContext.Session.GetComplexData<List<OrderDetailsViewModel>>("OrderDetails");
                if (OrderDetailsList != null)
                {
                    // Order
                    OrderModel.StatusId = (int)Status.New;
                    OrderModel.IsDeleted = false;
                    OrderModel.CreatedBy = loginUserId;   // Need To Change
                    OrderModel.CreatedDate = DateTime.Now;

                    //_context.Order.Add(OrderModel);
                    //await _context.SaveChangesAsync();

                    //model to json string
                    var modelData = JsonConvert.SerializeObject(OrderModel);

                    //Webapiclient call
                    var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "Order"), modelData);

                    //Response after create
                    if (!string.IsNullOrWhiteSpace(response))
                    {
                        OrderModel = JsonConvert.DeserializeObject<OrderViewModel>(response);
                        foreach (var item in OrderDetailsList)
                        {
                            item.OrderId = OrderModel.ID;
                            // Order Details
                            item.IsDeleted = false;
                            item.CreatedBy = loginUserId;   // Need To Change
                            item.CreatedDate = DateTime.Now;

                            //model to json string
                            var productModelData = JsonConvert.SerializeObject(item);

                            //Webapiclient call
                            _webApiHelper.WebApiClientPost(string.Format("api/{0}", "OrderDetails"), productModelData);

                        }
                    }

                    HttpContext.Session.Remove("Order");
                    HttpContext.Session.Remove("OrderDetails");
                    HttpContext.Session.Remove("AddToCartList");
                    HttpContext.Session.Remove("ProductList");

                }
            }

            return RedirectToPage("/Orders/OrderSuccess");
        }

        #endregion
    }
}

    

