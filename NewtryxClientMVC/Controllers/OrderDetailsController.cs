using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NewtryxClientMVC.Models;
using NToastNotify;
using System.Text;

namespace NewtryxClientMVC.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly IToastNotification _toastNotification;

        HttpClientHandler clientHandler = new HttpClientHandler();

        Order orders = new Order();
        OrderDetail orderDetails = new OrderDetail();
        List<OrderDetailVm> reservations = new List<OrderDetailVm>();


        public OrderDetailsController(IToastNotification toastNotification)
        {
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, ssPolicyErrors) => { return true; };
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<OrderDetailVm>> GetAllOrderDetails()
        {
            reservations = new List<OrderDetailVm>();
            using (var client = new HttpClient(clientHandler))
            {
                using (var res = await client.GetAsync("https://localhost:44346/api/OrderDetail"))
                {
                    string apiRes = await res.Content.ReadAsStringAsync();
                    reservations = JsonConvert.DeserializeObject<List<OrderDetailVm>>(apiRes);
                }
            }
            return reservations;
        }

        [HttpGet]
        public async Task<OrderDetail> GetById(int id)
        {
            orderDetails = new OrderDetail();
            using (var client = new HttpClient(clientHandler))
            {
                using (var res = await client.GetAsync("https://localhost:44346/api/OrderDetail/" + id))
                {
                    string apiRes = await res.Content.ReadAsStringAsync();
                    orderDetails = JsonConvert.DeserializeObject<OrderDetail>(apiRes);
                }
            }
            return orderDetails;
        }

        [HttpPost]
        public async Task<Order> AddUpdateOrderDetail(Order _order)
        {
            //if (ModelState.IsValid)
            //{
            orders = new Order();
            using (var client = new HttpClient(clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(_order), Encoding.UTF8, "application/json");
                using (var res = await client.PostAsync("https://localhost:44346/api/OrderDetail/", content))
                {
                    string apiRes = await res.Content.ReadAsStringAsync();
                    orders = JsonConvert.DeserializeObject<Order>(apiRes);
                }
            }
            _toastNotification.AddSuccessToastMessage("Data Successfully Added");

            //    return reservation;
            //}

            //_toastNotification.AddErrorToastMessage("Please fill in any empty textboxes");

            return orders;
        }

        [HttpDelete]
        public async Task<string> Delete(int id)
        {
            string msg = "";
            using (var client = new HttpClient(clientHandler))
            {
                using (var res = await client.DeleteAsync("https://localhost:44346/api/OrderDetail/" + id))
                {
                    msg = await res.Content.ReadAsStringAsync();
                }
            }
            _toastNotification.AddSuccessToastMessage("Record Deleted Successfully");

            return msg;
        }
    }
}
