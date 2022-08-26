using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewtryxClientMVC.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string Description { get; set; }
        public decimal FinalTotal { get; set; }
        public DateTime OrderDateTime { get; set; }
        public string OrderDate { get; set; }
    }
}
