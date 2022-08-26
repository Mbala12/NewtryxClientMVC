using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NewtryxClientMVC.Models;
using NToastNotify;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewtryxClientMVC.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IToastNotification _toastNotification;

        HttpClientHandler clientHandler = new HttpClientHandler();

        Reservation reservation = new Reservation();
        List<ReservationDetailsVm> reservations = new List<ReservationDetailsVm>();


        public ReservationController(IToastNotification toastNotification)
        {
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, ssPolicyErrors) => { return true; };
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<ReservationDetailsVm>> GetAllReservations()
        {
            reservations = new List<ReservationDetailsVm>();
            using (var client = new HttpClient(clientHandler))
            {
                using (var res = await client.GetAsync("https://localhost:44346/api/Reservation"))
                {
                    string apiRes = await res.Content.ReadAsStringAsync();
                    reservations = JsonConvert.DeserializeObject<List<ReservationDetailsVm>>(apiRes);
                }
            }
            return reservations;
        }

        [HttpGet]
        public async Task<Reservation> GetById(int id)
        {
            reservation = new Reservation();
            using (var client = new HttpClient(clientHandler))
            {
                using (var res = await client.GetAsync("https://localhost:44346/api/Reservation/" + id))
                {
                    string apiRes = await res.Content.ReadAsStringAsync();
                    reservation = JsonConvert.DeserializeObject<Reservation>(apiRes);
                }
            }
            return reservation;
        }

        [HttpPost]
        public async Task<Reservation> AddUpdateReservation(Reservation _reservation)
        {
            //if (ModelState.IsValid)
            //{
                reservation = new Reservation();
                using (var client = new HttpClient(clientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(_reservation), Encoding.UTF8, "application/json");
                    using (var res = await client.PostAsync("https://localhost:44346/api/Reservation/", content))
                    {
                        string apiRes = await res.Content.ReadAsStringAsync();
                        reservation = JsonConvert.DeserializeObject<Reservation>(apiRes);
                    }
                }
                _toastNotification.AddSuccessToastMessage("Data Successfully Added");

            //    return reservation;
            //}

            //_toastNotification.AddErrorToastMessage("Please fill in any empty textboxes");

            return reservation;
        }

        [HttpDelete]
        public async Task<string> Delete(int id)
        {
            string msg = "";
            using (var client = new HttpClient(clientHandler))
            {
                using (var res = await client.DeleteAsync("https://localhost:44346/api/Reservation/" + id))
                {
                    msg = await res.Content.ReadAsStringAsync();
                }
            }
            _toastNotification.AddSuccessToastMessage("Record Deleted Successfully");

            return msg;
        }
    }
}
