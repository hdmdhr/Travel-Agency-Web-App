using System;
using System.Collections.Generic;

namespace TeamLID.TravelExperts.Repository.Domain
{
    public partial class BookingDetails
    {
        public int BookingDetailId { get; set; }
        public double? ItineraryNo { get; set; }
        public DateTime? TripStart { get; set; }
        public DateTime? TripEnd { get; set; }
        public string Description { get; set; }
        public string Destination { get; set; }
        public decimal? BasePrice { get; set; }
        public decimal? AgencyCommission { get; set; }
        public int? BookingId { get; set; }
        public string RegionId { get; set; }
        public string ClassId { get; set; }
        public string FeeId { get; set; }
        public int? ProductSupplierId { get; set; }

        public virtual Bookings Booking { get; set; }
        public virtual Classes Class { get; set; }
        public virtual Fees Fee { get; set; }
        public virtual ProductsSuppliers ProductSupplier { get; set; }
        public virtual Regions Region { get; set; }
    }
}
