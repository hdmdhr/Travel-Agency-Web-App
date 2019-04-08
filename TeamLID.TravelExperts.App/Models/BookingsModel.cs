using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeamLID.TravelExperts.App.Models
{
    public class BookingsModel
    {
        public int BookingId { get; set; }

        [DataType(DataType.Date), DisplayName("Booking Date")]
        public DateTime? BookingDate { get; set; }

        [DisplayName("Booking Number")]
        public string BookingNo { get; set; }

        [DisplayName("Number of Travellers")]
        public double? TravelerCount { get; set; }

        [DisplayName("Customer")]
        public string CustomerId { get; set; }

        [DisplayName("Trip Type")]
        public string TripTypeId { get; set; }

        [DisplayName("Package Booked")]
        public string PackageId { get; set; }

        [DisplayName("CAD$ Price")]
        public decimal Price { get; set; }

        [DisplayName("Total Owing")]
        public string Total { get; set; }
    }
}
