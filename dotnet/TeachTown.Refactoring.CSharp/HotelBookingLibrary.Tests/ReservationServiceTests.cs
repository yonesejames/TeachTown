using Xunit;

namespace HotelReservationLibrary.Tests
{
    public class ReservationServiceTests
    {
        private ReservationService _target;
        private Reservation _reservation;

        public ReservationServiceTests()
        {
            _target = new ReservationService();
            
            _reservation = new Reservation()
            {
                GuestFirstName = "John",
                GuestLastName = "Brown",
                guestEmail = "johnbrown@gmail.com",
                CheckInDate = new DateTime(2024, 12, 12, 13, 30, 00),
                CheckOutDate = new DateTime(2024, 12, 15, 13, 30, 00),
                NumberOfAdditionalGuests = 2,
                RoomType = "Suite",
                SmokingOrNonSmoking = "Non-Smoking"
            };
        }

        [Fact]
        public void BookReservation_WhenReservationIsNull_ThrowArgumentNullException()
        {
            Reservation reservation = null;
            
            var exception = Assert.Throws<ArgumentNullException>(() => _target.BookReservation(reservation));
            
            Assert.Equal("reservation", exception.ParamName);
            Assert.Equal("Nonexistent reservation (Parameter 'reservation')", exception.Message);
        }
        
        [Fact]
        public void BookReservation_WhenReservationIsNotNull_ReturnLong()
        {
            var result = _target.BookReservation(_reservation);
            
            Assert.IsType<long>(result);
            Assert.True(result > 0);
        }
        
        [Theory]
        [InlineData("", "Brown")]
        [InlineData(null, "Brown")]
        [InlineData("John", "")]
        [InlineData("John", null)]
        public void BookReservation_WhenNameIsNullOrEmpty_ReturnZero(string? firstName, string? lastName)
        {
            _reservation.GuestFirstName = firstName;
            _reservation.GuestLastName = lastName;

            var result = _target.BookReservation(_reservation);
            
            Assert.Equal(0, result);
        }
        
        [Fact]
        public void BookReservation_WhenEmailDoesNotContainAtSign_ReturnZero()
        {
            _reservation.guestEmail = "johnbrown.com";

            var result = _target.BookReservation(_reservation);
            
            Assert.Equal(0, result);
        }
        
        [Fact]
        public void BookReservation_WhenCheckInDateIsLaterThanCheckOutDate_ReturnZero()
        {
            _reservation.CheckInDate = new DateTime(2024, 12, 12, 13, 30, 00);
            _reservation.CheckOutDate = new DateTime(2024, 12, 11, 12, 30, 00);

            var result = _target.BookReservation(_reservation);
            
            Assert.Equal(0, result);
        }
        
        [Fact]
        public void BookReservation_WhenNumberOfAdditionalGuestsIsMoreThanTwo_ReturnZero()
        {
            _reservation.NumberOfAdditionalGuests = 3;

            var result = _target.BookReservation(_reservation);
            
            Assert.Equal(0, result);
        }
        
        [Fact]
        public void BookReservation_WhenIsSmoking_ReturnCorrectTotal()
        {
            _reservation.SmokingOrNonSmoking = "Smoking";

            _target.BookReservation(_reservation);
            
            Assert.Equal(945, _reservation.Total);
        }
    }
}
