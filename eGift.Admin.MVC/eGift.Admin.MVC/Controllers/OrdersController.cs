using eGift.Admin.MVC.Common;
using eGift.Admin.MVC.Helpers;
using eGift.Admin.MVC.Models.ListViewModels;
using eGift.Admin.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace eGift.Admin.MVC.Controllers
{
    public class OrdersController : Controller
    {
        #region Variables

        public IConfiguration _configuration;
        public WebApiHelper _webApiHelper;

        #endregion

        #region Constructors

        public OrdersController(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._webApiHelper = new WebApiHelper(configuration);
        }

        #endregion
        #region Product Actions
        // GET: OrdersController
        public ActionResult Index()
        {
            var list = new OrderListViewModel();

            //Webapiclient call
            var ordersResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}", "Order"));
            if (!string.IsNullOrWhiteSpace(ordersResponse))
            {
                list.OrderList = JsonConvert.DeserializeObject<OrderViewModel[]>(ordersResponse).ToList();

                //Webapiclient call
                var customerResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}", "Customer"));
                if (!string.IsNullOrWhiteSpace(customerResponse))
                {
                    var customerList = JsonConvert.DeserializeObject<CustomerViewModel[]>(customerResponse).ToList();
                    list.OrderList = list.OrderList.Join(customerList, o => o.CustomerId, c => c.ID, (o, c) => new { o, c }).Select(m => { m.o.FirstName = m.c.FirstName; m.o.LastName = m.c.LastName; return m.o; }).ToList();

                }
                return View(list);
            }
            return View();
        }

        // GET: OrdersController/Details/5
        public ActionResult Details(int id)
        {
            {
                var orderModel = new OrderViewModel();

                //Webapiclient call
                var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Order", id));
                if (!string.IsNullOrWhiteSpace(response))
                {
                    orderModel = JsonConvert.DeserializeObject<OrderViewModel>(response);

                    //Webapiclient call
                    var customerResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Customer", orderModel.CustomerId));
                    if (!string.IsNullOrWhiteSpace(customerResponse))
                    {
                        var customer = JsonConvert.DeserializeObject<CustomerViewModel>(customerResponse);
                        orderModel.CustomerId = customer.ID;

                        orderModel.CustomerName = customer.Name;
                        orderModel.CustomerAddress = customer.AddressName;
                        orderModel.CustomerContact = customer.Mobile;
                        orderModel.StatusName = ((Status)orderModel.StatusId).ToString();
                    }

                    //Webapiclient call
                    var orderDetailsResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}?orderId={2}", "OrderDetails", "GetByOrderId", id));
                    if (!string.IsNullOrWhiteSpace(orderDetailsResponse))
                    {
                        orderModel.OrderDetailsList = JsonConvert.DeserializeObject<OrderDetailsViewModel[]>(orderDetailsResponse).ToList();
                    }

                    //Webapiclient call
                    var productsResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}", "Product"));
                    if (!string.IsNullOrWhiteSpace(productsResponse))
                    {
                        var productList = JsonConvert.DeserializeObject<ProductViewModel[]>(productsResponse).ToList();

                        //Join
                        orderModel.OrderDetailsList = orderModel.OrderDetailsList.Join(productList, od => od.ProductId, p => p.ID, (od, p) => new { od, p }).Select(m => { m.od.ProductName = m.p.Name; return m.od; }).ToList();
                    }
                }
                return View(orderModel);
            }
        }
        // GET: OrdersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrdersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrdersController/Edit/5
        public ActionResult Edit(int id)
        {
            //Webapiclient call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Order", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                var OrderModel = JsonConvert.DeserializeObject<OrderViewModel>(response);
                OrderModel.StatusName = ((Status)OrderModel.StatusId).ToString();

                //Webapiclient call
                var customerResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Customer", OrderModel.CustomerId));
                if (!string.IsNullOrWhiteSpace(customerResponse))
                {
                    var customer = JsonConvert.DeserializeObject<CustomerViewModel>(customerResponse);
                    OrderModel.CustomerId = customer.ID;

                    OrderModel.CustomerName = customer.Name;
                    OrderModel.CustomerAddress = customer.AddressName;
                    OrderModel.CustomerContact = customer.Mobile;
                }

                //Webapiclient call
                var orderDetailsResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}?orderId={2}", "OrderDetails", "GetByOrderId", id));
                if (!string.IsNullOrWhiteSpace(orderDetailsResponse))
                {
                    OrderModel.OrderDetailsList = JsonConvert.DeserializeObject<OrderDetailsViewModel[]>(orderDetailsResponse).ToList();
                }

                //Webapiclient call
                var productsResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}", "Product"));
                if (!string.IsNullOrWhiteSpace(productsResponse))
                {
                    var productList = JsonConvert.DeserializeObject<ProductViewModel[]>(productsResponse).ToList();

                    //Join
                    OrderModel.OrderDetailsList = OrderModel.OrderDetailsList.Join(productList, od => od.ProductId, p => p.ID, (od, p) => new { od, p }).Select(m => { m.od.ProductName = m.p.Name; return m.od; }).ToList();
                }
                return View(OrderModel);
            }
            return View();
        }

        // POST: OrdersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OrderViewModel model)
        {
            try
            {
                //Webapiclient call
                var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Order", id));
                if (!string.IsNullOrWhiteSpace(response))
                {
                    var order = JsonConvert.DeserializeObject<OrderViewModel>(response);

                    order.StatusId = model.StatusId;
                    if (model.StatusId == (int)Status.Dispatched)
                    {
                        order.DispatchDate = DateTime.Now;
                    }
                    else if (model.StatusId == (int)Status.Shipped)
                    {
                        order.ShippedDate = DateTime.Now;
                    }
                    else if (model.StatusId == (int)Status.Delievered)
                    {
                        order.DeliveryDate = DateTime.Now;
                    }
                    else if (model.StatusId == (int)Status.Cancelled)
                    {
                        order.CancelDate = DateTime.Now;
                    }
                    order.UpdatedBy = HttpContext.Session.GetInt32("UserId");
                    order.UpdatedDate = DateTime.Now;

                    //conver model to json string
                    var modelData = JsonConvert.SerializeObject(order);

                    // Web client call
                    var updateResponse = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "Order"), modelData);
                    if (!string.IsNullOrWhiteSpace(updateResponse))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch
            {
            }
            return View();
        }

        // GET: OrdersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrdersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        #endregion
    }
}
