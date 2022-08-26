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
    public class ItemController : Controller
    {
        private readonly IToastNotification _toastNotification;

        HttpClientHandler clientHandler = new HttpClientHandler();

        Item item = new Item();
        List<Item> items = new List<Item>();
        //List<Restaurant> restaurants = new List<Restaurant>();

        public ItemController(IToastNotification toastNotification)
        {
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, ssPolicyErrors) => { return true; };
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<Item>> GetAllItems()
        {
            items = new List<Item>();
            using (var client = new HttpClient(clientHandler))
            {
                using (var res = await client.GetAsync("https://localhost:44346/api/Item"))
                {
                    string apiRes = await res.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<List<Item>>(apiRes);
                }
            }
            return items;
        }

        [HttpGet]
        public async Task<Item> GetById(int id)
        {
            item = new Item();
            using (var client = new HttpClient(clientHandler))
            {
                using (var res = await client.GetAsync("https://localhost:44346/api/Item/" + id))
                {
                    string apiRes = await res.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<Item>(apiRes);
                }
            }
            return item;
        }

        [HttpPost]
        public async Task<Item> AddUpdateItem(Item _item)
        {
            if (ModelState.IsValid)
            {
                item = new Item();
                using (var client = new HttpClient(clientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(_item), Encoding.UTF8, "application/json");
                    using (var res = await client.PostAsync("https://localhost:44346/api/Item/", content))
                    {
                        string apiRes = await res.Content.ReadAsStringAsync();
                        item = JsonConvert.DeserializeObject<Item>(apiRes);
                    }
                }
                _toastNotification.AddSuccessToastMessage("New Item Successfully Added");

                return item;
            }
            return item;
        }

        [HttpDelete]
        public async Task<string> Delete(int id)
        {
            string msg = "";
            using (var client = new HttpClient(clientHandler))
            {
                using (var res = await client.DeleteAsync("https://localhost:44346/api/Item/" + id))
                {
                    msg = await res.Content.ReadAsStringAsync();
                }
            }
            _toastNotification.AddSuccessToastMessage("Record Deleted Successfully");

            return msg;
        }
    }
}
