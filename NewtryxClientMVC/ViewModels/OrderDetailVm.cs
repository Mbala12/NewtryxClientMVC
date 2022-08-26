using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewtryxClientMVC.Models
{
    public class OrderDetailVm
    {
        public int OrderDetailId { get; set; }
        public string OrderNumber { get; set; }
        public string ItemName { get; set; }
        public string BookingNo { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public string OrderDate { get; set; }
    }
}
