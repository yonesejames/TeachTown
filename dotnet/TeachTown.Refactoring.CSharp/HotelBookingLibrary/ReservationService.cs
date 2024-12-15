namespace HotelReservationLibrary
{
    public class ReservationService
    {
        public long BookReservation(Reservation reservation)
        {
           try
           {
               if (reservation == null)
               {
                    
                   throw new ArgumentNullException(nameof(reservation), "Nonexistent reservation");
               }

               if (!ValidateReservation(reservation.GuestFirstName, 
                reservation.GuestLastName, 
                reservation.guestEmail, 
                reservation.CheckInDate, 
                reservation.CheckOutDate, 
                reservation.NumberOfAdditionalGuests))
               {
                   Console.WriteLine("Reservation cannot be book if name is empty or null, check in date is later than check out date, and number of guests exceeds 2. Please try again!");
                   return 0;
               }

               if (!string.IsNullOrEmpty(reservation.SmokingOrNonSmoking) && !string.IsNullOrEmpty(reservation.RoomType))
               {
                   reservation.PricePerNight = SetPricePerNight(reservation.RoomType, reservation.SmokingOrNonSmoking);
               }
               else
               {
                   throw new NullReferenceException("Smoking or Non-Smoking room or room type has not been selected. Please choose one!");
               }
               
               reservation.Total = PriceReservation(reservation.PricePerNight, reservation.CheckInDate, reservation.CheckOutDate);

               return ReservationDb.AddReservation(reservation);
           }
           catch (ArgumentNullException exception)
           {
               Console.WriteLine("Experienced issue within ReservationService.BookReservation due to nonexistent reservation: " + exception);
               throw;
           }
           catch (NullReferenceException exception)
           {
               Console.WriteLine("Experienced issue within ReservationService.BookReservation: " + exception);
               throw;
           }
           catch(Exception exception)
           {
               Console.WriteLine("Experienced issue within ReservationService.BookReservation: " + exception);
               throw;
           }
        }
        
        private bool ValidateReservation(string? firstName, string? lastName, string? email, DateTime checkInDate, DateTime checkOutDate, int numberOfAdditionalGuests)
        {
            try
            {
                if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || !email.Contains('@') || checkInDate >= checkOutDate || checkOutDate <= checkInDate || numberOfAdditionalGuests > 2)
                {
                    return false;
                }

                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Experienced issue within ReservationService.ValidateReservation: " + exception);
                throw;
            }
        }
        
        private double SetPricePerNight(String roomType, string smokingOrNonSmoking)
        {
            try
            {
                double pricePerNight;
            
                switch (roomType)
                {
                    case "Single":
                        pricePerNight = 100;
                        break;
                    case "Double":
                        pricePerNight = 200;
                        break;
                    case "Suite":
                        pricePerNight = 300;
                        break;
                    default: 
                        pricePerNight = 100; 
                        Console.WriteLine("This room does not exist. Please try again."); 
                        break;
                }
                
                return smokingOrNonSmoking == "Smoking" ? pricePerNight *= 1.05 : pricePerNight;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Experienced issue within ReservationService.SetPricePerNight: " + exception);
                throw;
            }
        }
        
        private double PriceReservation(double pricePerNight, DateTime checkInDate, DateTime checkOutDate)
        {
            try
            {
                var weatherService = new ExternalWeatherApi();
                var forecast = weatherService.GetForecast(DateOnly.FromDateTime(checkInDate), DateOnly.FromDateTime(checkOutDate));
                
                var reservationTotal = pricePerNight * (checkOutDate - checkInDate).Days;

                if (forecast.Summary == "Freezing" || forecast.Summary == "Sweltering")
                {
                    reservationTotal *= 1.2;
                }

                return reservationTotal;

            }
            catch (Exception exception)
            {
                Console.WriteLine("Experienced issue within ReservationService.PriceReservation: " + exception);
                throw;
            }
        }
    }
}