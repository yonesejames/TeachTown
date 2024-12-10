using Microsoft.Data.SqlClient;

namespace HotelReservationLibrary
{
    internal static class ReservationDb
    {
        public static long AddReservation(Reservation reservation)
        {
            using (var connection = new SqlConnection("ConnectionString"))
            {
                connection.Open();
                // Add reservation to database
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Reservations (GuestFirstName, GuestLastName, GuestEmail, CheckInDate, CheckOutDate, NumberOfAdditionalGuests, RoomType, SmokingOrNonSmoking, Total) " +
                    "VALUES (@GuestFirstName, @GuestLastName, @GuestEmail, @CheckInDate, @CheckOutDate, @NumberOfAdditionalGuests, @RoomType, @SmokingOrNonSmoking, @Total)";
                command.Parameters.AddWithValue("@GuestFirstName", reservation.GuestFirstName);
                command.Parameters.AddWithValue("@GuestLastName", reservation.GuestLastName);
                command.Parameters.AddWithValue("@GuestEmail", reservation.guestEmail);
                command.Parameters.AddWithValue("@CheckInDate", reservation.CheckInDate);
                command.Parameters.AddWithValue("@CheckOutDate", reservation.CheckOutDate);
                command.Parameters.AddWithValue("@NumberOfAdditionalGuests", reservation.NumberOfAdditionalGuests);
                command.Parameters.AddWithValue("@RoomType", reservation.RoomType);
                command.Parameters.AddWithValue("@SmokingOrNonSmoking", reservation.SmokingOrNonSmoking);
                command.Parameters.AddWithValue("@Total", reservation.Total);
                command.ExecuteNonQuery();
            }

            return DateTime.Now.Ticks;
        }
    }

    
}
