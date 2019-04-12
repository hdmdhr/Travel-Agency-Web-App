/*
Author: Ibraheem
Purpose: Get booking history for customers
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TeamLID.TravelExperts.Repository.Domain;

namespace TeamLID.TravelExperts.App.Models
{
    //// <summary>
    /// Booking model
    /// </summary>
    public class BookingsModel
    {
        public int BookingId { get; set; }

        [DataType(DataType.Date), DisplayName("Booking Date")]
        public DateTime? BookingDate { get; set; }

        [DataType(DataType.Date), DisplayName("Trip Start Date")]
        public DateTime? PkgStartDate { get; set; }

        [DataType(DataType.Date), DisplayName("Trip End Date")]
        public DateTime? PkgEndDate { get; set; }

        [DisplayName("Trip Description")]
        public string PkgDesc { get; set; }

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

        [DataType(DataType.Currency), DisplayName("Price (CAD$)")]
        public decimal Price { get; set; }

        [DisplayName("Total Owing")]
        public string Total { get; set; }
    }
}
