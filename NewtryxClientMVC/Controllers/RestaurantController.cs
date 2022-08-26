using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NewtryxClientMVC.Models;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NewtryxClientMVC.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IToastNotification _toastNotification;

        HttpClientHandler clientHandler = new HttpClientHandler();

        Restaurant restaurant = new Restaurant();
        List<Restaurant> restaurants = new List<Restaurant>();

        public RestaurantController(IToastNotification toastNotification)
        {
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, ssPolicyErrors) => { return true; };
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<Restaurant>> GetAllRestaurants()
        {
            restaurants = new List<Restaurant>();
            using (var client = new HttpClient(clientHandler))
            {
                using (var res = await client.GetAsync("https://localhost:44346/api/Restaurant"))
                {
                    string apiRes = await res.Content.ReadAsStringAsync();
                    restaurants = JsonConvert.DeserializeObject<List<Restaurant>>(apiRes);
                }
            }
            return restaurants;
        }

        [HttpGet]
        public async Task<Restaurant> GetById(int id)
        {
            restaurant = new Restaurant();
            using (var client = new HttpClient(clientHandler))
            {
                using (var res = await client.GetAsync("https://localhost:44346/api/Restaurant/"+id))
                {
                    string apiRes = await res.Content.ReadAsStringAsync();
                    restaurant = JsonConvert.DeserializeObject<Restaurant>(apiRes);
                }
            }
            return restaurant;
        }

        [HttpPost]
        public async Task<Restaurant> AddUpdateRestaurant(Restaurant _restaurant)
        {
            //if (ModelState.IsValid)
            //{
                restaurant = new Restaurant();
                using (var client = new HttpClient(clientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(_restaurant), Encoding.UTF8, "application/json");
                    using (var res = await client.PostAsync("https://localhost:44346/api/Restaurant/", content))
                    {
                        string apiRes = await res.Content.ReadAsStringAsync();
                        restaurant = JsonConvert.DeserializeObject<Restaurant>(apiRes);
                    }
                }
                _toastNotification.AddSuccessToastMessage("Data Successfully Added");

                return restaurant;

            //}
            //return restaurant;
        }

            [HttpDelete]
        public async Task<string> Delete(int id)
        {
            string msg = "";
            using (var client = new HttpClient(clientHandler))
            {
                using (var res = await client.DeleteAsync("https://localhost:44346/api/Restaurant/" + id))
                {
                    msg = await res.Content.ReadAsStringAsync();
                }
            }
            _toastNotification.AddSuccessToastMessage("Record Deleted Successfully");

            return msg;
        }
    }
}
