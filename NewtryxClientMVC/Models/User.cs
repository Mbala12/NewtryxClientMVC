using System.ComponentModel.DataAnnotations;

namespace NewtryxClientMVC.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int RestaurantId { get; set; }

        [Required(ErrorMessage = "Please enter the First Name")]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Please enter the Last Name")]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Please enter the Email Address")]
        [EmailAddress(ErrorMessage = "Please enter a correct Email")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter the Phone number")]
        [Display(Name = "Telephone")]
        public string Phone { get; set; }
        //[Required]
        public string Controlnumber { get; set; }
        
        public string Restaurant { get; set; }
    }
}
