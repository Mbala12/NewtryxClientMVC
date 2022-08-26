using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewtryxClientMVC.Models
{
    public class ReservationDetailsVm
    {
        public int ReservationId { get; set; }
        public string BookingNo { get; set; }
        public string RestoNames { get; set; }
        public string UserFullname { get; set; }
        public string StatusType { get; set; }
        public DateTime BookedDT { get; set; }
        public string Description { get; set; }
    }
}
