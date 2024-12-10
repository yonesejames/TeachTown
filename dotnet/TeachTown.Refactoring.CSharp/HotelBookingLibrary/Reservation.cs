namespace HotelReservationLibrary
{
    public class Reservation
    {
        public string GuestFirstName { get; set; }
        public string GuestLastName { get; set; }        
        public string guestEmail { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfAdditionalGuests { get; set; }
        public string RoomType { get; set; }
        public string SmokingOrNonSmoking { get; set; }
        internal double PricePerNight { get; set; } = 100;
        internal double Total { get; set; }

    }
}