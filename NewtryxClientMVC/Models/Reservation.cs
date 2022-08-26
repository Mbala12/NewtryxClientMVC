using System;


namespace NewtryxClientMVC.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int RestaurantId { get; set; }
        public string BookingNo { get; set; }
        public int UserId { get; set; }
        public int ReservationStatusId { get; set; }
        public DateTime StartDateTime { get; set; }
        public string Description { get; set; }
    }
}
