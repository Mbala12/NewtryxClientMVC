using System.ComponentModel.DataAnnotations;

namespace NewtryxClientMVC.Models
{
    public class ReservationStatus
    {
        public int ReservationStatusId { get; set; }
        [StringLength(20, MinimumLength =6)]
        [Required(ErrorMessage ="Please enter the Status of Reservation")]
        public string ReservationStatusType { get; set; }
    }
}
