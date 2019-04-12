using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamLID.TravelExperts.App.Models;
using TeamLID.TravelExperts.App.Models.DataManager;
using TeamLID.TravelExperts.Repository.Domain;

namespace TeamLID.TravelExperts.App.Controllers
{
    public class BookingsController : Controller
    {
        private readonly TravelExpertsContext _context;

        public BookingsController(TravelExpertsContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var travelExpertsContext = _context.Bookings.Include(b => b.Customer).Include(b => b.Package).Include(b => b.TripType);
            return View(await travelExpertsContext.ToListAsync());
        }

        // GET: Bookings by customer (Customer Booking History)
        // This has been moved to CustomersController
        public ActionResult CustomerHistory()
        {
            // Change this to Id retrieved from sessions
            int id = 143;

            var bookings = BookingsManager.GetAllBookingsByCustomer(id)
                .Select(bk => new BookingsModel
                {
                    BookingId = bk.BookingId,
                    BookingDate = bk.BookingDate,
                    BookingNo = bk.BookingNo,
                    TravelerCount = bk.TravelerCount,
                    CustomerId = bk.Customer.CustFirstName,
                    TripTypeId = bk.TripType.Ttname,
                    PackageId = bk.Package.PkgName                            
                }).ToList();
                
            return View(bookings);

        }

        public decimal TotalOwing(decimal amount)
        {
            decimal total = 0;

            total += amount;

            return total;
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Package)
                .Include(b => b.TripType)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }
    }
}
