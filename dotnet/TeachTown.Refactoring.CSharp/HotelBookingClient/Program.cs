using HotelReservationLibrary;

namespace HotelReservationClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var reservationService = new ReservationService();
            var reservation = new Reservation() {
                GuestFirstName = "John",
                GuestLastName = "Doe",
                guestEmail = "jdoe@teachtown.hiring.com",
                CheckInDate = new DateTime(2022, 1, 1),
                CheckOutDate = new DateTime(2022, 1, 8),
                NumberOfAdditionalGuests = 1,
                RoomType = "Single",
                SmokingOrNonSmoking = "Non-Smoking"
            };
            var resevationNumber = reservationService.AddReservation(reservation);
            Console.WriteLine("Reservation number: " + resevationNumber);
        }
    }
}
