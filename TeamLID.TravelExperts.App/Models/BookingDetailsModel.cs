/*
Author: Ibraheem
Purpose: Get booking details for customers
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamLID.TravelExperts.App.Models
{
    /// <summary>
    /// Booking details model.
    /// </summary>
    /// Not used. TODO: Nuke
    public class BookingDetailsModel
    {

        public double? ItineraryNo { get; set; }
        [DataType(DataType.Date), DisplayName("Trip Start")]
        public DateTime? TripStart { get; set; }
        [DataType(DataType.Date), DisplayName("Trip End")]
        public DateTime? TripEnd { get; set; }
        public string Description { get; set; }
        public string Destination { get; set; }
        [DataType(DataType.Currency), DisplayName("Price")]
        public decimal? BasePrice { get; set; }
    }
}
