using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NewtryxClientMVC.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        //[Required]
        public string Name { get; set; }
        //[Required]
        public string Address { get; set; }
        //[Required]
        public string Email { get; set; }
        //[Required]
        public string Phone { get; set; }
    }
}
