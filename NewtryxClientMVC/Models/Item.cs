using System.ComponentModel.DataAnnotations;
using System;

namespace NewtryxClientMVC.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        [Required(ErrorMessage = "Please enter the Name of the Item")]
        //[Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Required(ErrorMessage="Please enter the Price of the Item")]
        //[Display(Name ="Item Price")]
        public decimal ItemPrice { get; set; }
    }
}
