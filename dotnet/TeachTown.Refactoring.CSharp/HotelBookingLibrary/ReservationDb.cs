using Microsoft.Extensions.Configuration;
using Npgsql;

namespace HotelReservationLibrary
{
    internal static class ReservationDb
    {
        public static long AddReservation(Reservation reservation)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var dbUsername = configuration["Database:Username"];
            var dbPassword = configuration["Database:Password"];
            
            using (var connection = new NpgsqlConnection($"Host=localhost; Port=5432; Database=TeachTown; Username={dbUsername}; Password={dbPassword}"))
            {
                connection.Open();
                // Add reservation to database
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Reservations (GuestFirstName, GuestLastName, GuestEmail, CheckInDate, CheckOutDate, NumberOfAdditionalGuests, RoomType, SmokingOrNonSmoking, Total) " +
                                      "VALUES (@GuestFirstName, @GuestLastName, @GuestEmail, @CheckInDate, @CheckOutDate, @NumberOfAdditionalGuests, @RoomType, @SmokingOrNonSmoking, @Total)";
                    
                var parameters = new Dictionary<string, object>
                {
                    { "@GuestFirstName", reservation.GuestFirstName },
                    { "@GuestLastName", reservation.GuestLastName },
                    { "@GuestEmail", reservation.guestEmail },
                    { "@CheckInDate", reservation.CheckInDate },
                    { "@CheckOutDate", reservation.CheckOutDate },
                    { "@NumberOfAdditionalGuests", reservation.NumberOfAdditionalGuests },
                    { "@RoomType", reservation.RoomType },
                    { "@SmokingOrNonSmoking", reservation.SmokingOrNonSmoking },
                    { "@Total", reservation.Total }
                };
                    
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
                    
                command.ExecuteNonQuery();
            }

            return DateTime.Now.Ticks;
        }
    }

    
}
