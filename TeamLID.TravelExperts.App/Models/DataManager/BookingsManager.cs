/*
Author: Ibraheem
Purpose: Get bookings history for customers
*/
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
                .Include(bookingDetail => bookingDetail.BookingDetails)
                .Where(booking => booking.CustomerId == customerId)
                .ToList();
                                        
            return bookings;

        }

        public static Bookings Find(int id)
        {
            var context = new TravelExpertsContext();

            var booking = context.Bookings
                .Include(customer => customer.Customer)
                .Include(trip => trip.TripType)
                .Include(package => package.Package)
                .Include(bookingDetail => bookingDetail.BookingDetails)
                .SingleOrDefault(bk => bk.BookingId == id);

            return booking;

        }
    }
}
