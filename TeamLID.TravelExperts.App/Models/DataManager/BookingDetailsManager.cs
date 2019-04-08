using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamLID.TravelExperts.Repository.Domain;

namespace TeamLID.TravelExperts.App.Models.DataManager
{
    public class BookingDetailsManager
    {
        public static List<BookingDetails> GetAllBookingsDetailsByCustomer(int bookingId)
        {
            var context = new TravelExpertsContext();
            var bookingDetails = context.BookingDetails
                .Include(itinerary => itinerary.ItineraryNo)
                .Include(tripStart => tripStart.TripStart)
                .Include(tripEnd => tripEnd.TripEnd)
                .Include(description => description.Description)
                .Include(destination => destination.Destination)
                .Include(price => price.BasePrice)
                .Where(bk => bk.BookingId == bookingId)
                .ToList();

            return bookingDetails;

        }
    }
}
