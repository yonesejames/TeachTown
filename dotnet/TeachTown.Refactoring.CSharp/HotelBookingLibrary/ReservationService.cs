namespace HotelReservationLibrary
{
    public class ReservationService
    {

        public long AddReservation(Reservation reservashin)
        {
            ArgumentNullException.ThrowIfNull(reservashin);

            if (string.IsNullOrEmpty(reservashin.GuestFirstName))
            {
                return 0;
            }

            if (string.IsNullOrEmpty(reservashin.GuestLastName))
            {
                return 0;
            }

            if (!reservashin.guestEmail.Contains('@'))
            {
                return 0;
            }

            if(reservashin.CheckOutDate <= reservashin.CheckInDate)
            {
                return 0;
            }

            if(reservashin.CheckInDate >= reservashin.CheckOutDate) {
                return 0;
            }

            if(reservashin.NumberOfAdditionalGuests > 2)
            {
                return 0;
            }

            if (reservashin.RoomType == "Single")
            {
                reservashin.PricePerNight = 100;
            }
            else if (reservashin.RoomType == "Double")
            {
                reservashin.PricePerNight = 200;
            }
            else if (reservashin.RoomType == "Suite")
            {
                reservashin.PricePerNight = 300;
            }
            else
            {
                return 0;
            }

            if(reservashin.SmokingOrNonSmoking == "Smoking")
            {
                reservashin.PricePerNight *= 1.05;
            }            
            
            reservashin.Total = reservashin.PricePerNight * (reservashin.CheckOutDate - reservashin.CheckInDate).Days;

            var weatherService = new ExternalWeatherApi();
            try
            {
                var forecast = weatherService.GetForecast(DateOnly.FromDateTime(reservashin.CheckInDate), DateOnly.FromDateTime(reservashin.CheckOutDate));

                if (forecast.Summary == "Freezing" || forecast.Summary == "Sweltering")
                {
                    reservashin.Total *= 1.2;
                }
            }
            catch(Exception ex)
            {
                
            }

            return ReservationDb.AddReservation(reservashin);            
        }
    }
}