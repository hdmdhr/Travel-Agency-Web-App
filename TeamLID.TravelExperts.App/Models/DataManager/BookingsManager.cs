using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TeamLID.TravelExperts.Repository.Domain;

namespace TeamLID.TravelExperts.App.Models.DataManager
{
    public class BookingsManager
    {
        public static List<Bookings> GetAllBookingsByCustomer(int customerId)
        {
            var context = new TravelExpertsContext();
            var bookings = context.Bookings
                .Include(customer => customer.Customer)
                .Include(trip => trip.TripType)
                .Include(package => package.Package)
                .Where(bk => bk.CustomerId == customerId)
                .ToList();
                                        
            return bookings;
        }
    }
}
