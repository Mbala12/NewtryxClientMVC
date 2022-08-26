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
    public class ReservationStatusController : Controller
    {
        private readonly IToastNotification _toastNotification;

        HttpClientHandler clientHandler = new HttpClientHandler();

        ReservationStatus reservationStatus = new ReservationStatus();
        List<ReservationStatus> reservationStatuses = new List<ReservationStatus>();


        public ReservationStatusController(IToastNotification toastNotification)
        {
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, ssPolicyErrors) => { return true; };
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<ReservationStatus>> GetAllReservationStatuses()
        {
            reservationStatuses = new List<ReservationStatus>();
            using (var client = new HttpClient(clientHandler))
            {
                using (var res = await client.GetAsync("https://localhost:44346/api/ReservationStatus"))
                {
                    string apiRes = await res.Content.ReadAsStringAsync();
                    reservationStatuses = JsonConvert.DeserializeObject<List<ReservationStatus>>(apiRes);
                }
            }
            return reservationStatuses;
        }

        [HttpGet]
        public async Task<ReservationStatus> GetById(int id)
        {
            reservationStatus = new ReservationStatus();
            using (var client = new HttpClient(clientHandler))
            {
                using (var res = await client.GetAsync("https://localhost:44346/api/ReservationStatus/" + id))
                {
                    string apiRes = await res.Content.ReadAsStringAsync();
                    reservationStatus = JsonConvert.DeserializeObject<ReservationStatus>(apiRes);
                }
            }
            return reservationStatus;
        }

        [HttpPost]
        public async Task<ReservationStatus> AddUpdateReservationStatus(ReservationStatus _reservationStatus)
        {
            if (ModelState.IsValid)
            {
                reservationStatus = new ReservationStatus();
                using (var client = new HttpClient(clientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(_reservationStatus), Encoding.UTF8, "application/json");
                    using (var res = await client.PostAsync("https://localhost:44346/api/ReservationStatus/", content))
                    {
                        string apiRes = await res.Content.ReadAsStringAsync();
                        reservationStatus = JsonConvert.DeserializeObject<ReservationStatus>(apiRes);
                    }
                }

                _toastNotification.AddSuccessToastMessage("Data Successfully Added");

                return reservationStatus;
            }
            return reservationStatus;
        }

        [HttpDelete]
        public async Task<string> Delete(int id)
        {
            string msg = "";
            using (var client = new HttpClient(clientHandler))
            {
                using (var res = await client.DeleteAsync("https://localhost:44346/api/ReservationStatus/" + id))
                {
                    msg = await res.Content.ReadAsStringAsync();
                }
            }
            _toastNotification.AddSuccessToastMessage("Record Deleted Successfully");

            return msg;
        }
    }
}
