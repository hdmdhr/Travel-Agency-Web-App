using System;
using System.Collections.Generic;

namespace TeamLID.TravelExperts.Repository.Domain
{
    public partial class Bookings
    {
        public Bookings()
        {
            BookingDetails = new HashSet<BookingDetails>();
        }

        public int BookingId { get; set; }
        public DateTime? BookingDate { get; set; }
        public string BookingNo { get; set; }
        public double? TravelerCount { get; set; }
        public int? CustomerId { get; set; }
        public string TripTypeId { get; set; }
        public int? PackageId { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Packages Package { get; set; }
        public virtual TripTypes TripType { get; set; }
        public virtual ICollection<BookingDetails> BookingDetails { get; set; }

    }
}
