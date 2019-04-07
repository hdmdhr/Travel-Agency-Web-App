using System;
using System.ComponentModel;

namespace TeamLID.TravelExperts.App.Models
{
    public class BookingsModel
    {
        public int BookingId { get; set; }

        [DisplayName("Booking Date")]
        public DateTime? BookingDate { get; set; }

        [DisplayName("Booking Number")]
        public string BookingNo { get; set; }

        [DisplayName("Traveler Count")]
        public double? TravelerCount { get; set; }

        [DisplayName("Customer")]
        public int? CustomerId { get; set; }

        [DisplayName("Trip Type Number")]
        public string TripTypeId { get; set; }

        [DisplayName("Package Booked")]
        public int? PackageId { get; set; }
    }
}
