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
    public class UserController : Controller
    {
        private readonly IToastNotification _toastNotification;

        HttpClientHandler clientHandler = new HttpClientHandler();

        User user = new User();
        List<User> users = new List<User>();
        List<Restaurant> restaurants = new List<Restaurant>();

        public UserController(IToastNotification toastNotification)
        {
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, ssPolicyErrors) => { return true; };
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<User>> GetAllUsers()
        {
            users = new List<User>();
            using (var client = new HttpClient(clientHandler))
            {
                using (var res = await client.GetAsync("https://localhost:44346/api/User"))
                {
                    string apiRes = await res.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<User>>(apiRes);
                }
            }
            return users;
        }

        [HttpGet]
        public async Task<User> GetById(int id)
        {
            user = new User();
            using (var client = new HttpClient(clientHandler))
            {
                using (var res = await client.GetAsync("https://localhost:44346/api/User/" + id))
                {
                    string apiRes = await res.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(apiRes);
                }
            }
            return user;
        }


        [HttpGet]
        public async Task<List<User>> GetControlNumbers()
        {
            users = new List<User>();
            using (var client = new HttpClient(clientHandler))
            {
                using (var res = await client.GetAsync("https://localhost:44346/api/UserControlNumber"))
                {
                    string apiRes = await res.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<User>>(apiRes);
                }
            }
            return users;
        }

        [HttpGet]
        public async Task<List<Restaurant>> GetRestoNames()
        {
            restaurants = new List<Restaurant>();
            using (var client = new HttpClient(clientHandler))
            {
                using (var res = await client.GetAsync("https://localhost:44346/api/RestoName"))
                {
                    string apiRes = await res.Content.ReadAsStringAsync();
                    restaurants = JsonConvert.DeserializeObject<List<Restaurant>>(apiRes);
                }
            }
            return restaurants;
        }

        [HttpPost]
        public async Task<User> AddUpdateUser(User _user)
        {
            if (ModelState.IsValid)
            {
                user = new User();
                using (var client = new HttpClient(clientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(_user), Encoding.UTF8, "application/json");
                    using (var res = await client.PostAsync("https://localhost:44346/api/User/", content))
                    {
                        string apiRes = await res.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<User>(apiRes);
                    }
                }
                
               //_toastNotification.AddInfoToastMessage("Your Booking Number: " + user.Controlnumber);

                return user;
            }
            _toastNotification.AddErrorToastMessage("Please fill in all the textboxes!! ");
            return user;
        }

        [HttpDelete]
        public async Task<string> Delete(int id)
        {
            string msg = "";
            using (var client = new HttpClient(clientHandler))
            {
                using (var res = await client.DeleteAsync("https://localhost:44346/api/User/" + id))
                {
                    msg = await res.Content.ReadAsStringAsync();
                }
            }
            _toastNotification.AddSuccessToastMessage("Record Deleted Successfully");

            return msg;
        }
    }
}
