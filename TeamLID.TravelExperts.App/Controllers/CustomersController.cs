/*
Author: Ibraheem, DongMing
Purpose: Direct customers to register, login, profile, logout pages.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamLID.TravelExperts.App.Models;
using TeamLID.TravelExperts.App.Models.DataManager;
using TeamLID.TravelExperts.Repository.Domain;

namespace TeamLID.TravelExperts.App.Controllers
{
    public class CustomersController : Controller
    {
        private readonly TravelExpertsContext _context;

        public CustomersController(TravelExpertsContext context)
        {
            _context = context;
        }

        // ------------------------ DongMing Hu ----------------------------------

        // GET: user access register page
        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }

        // POST: user submit regsiter form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserViewModel user)
        {
            // 1. do server side validation. 2. if validation failed, go to register page with old inputs and error message.
            if (!ValidateUser(user))
            {
                ViewBag.msg = "validation failed, please fill in valid information and try again.";
                return View("Register", user);
            }
            else
            {
                // 3. if validation passed, create a Customers obj from received UserViewModel, insert into DB
                var newCust = new Customers
                {
                    CustLastName = user.CustLastName,
                    CustFirstName = user.CustFirstName,
                    CustBusPhone = Regex.Replace(user.CustBusPhone,@"[-.]","") ,  // remove . and -
                    CustPostal = user.CustPostal.Replace('-',' ').ToUpper(),  // T2G-1X6 => T2G 1X6
                    CustHomePhone = user.CustHomePhone != null ? Regex.Replace(user.CustHomePhone, @"[-.]", "") : null,
                    CustAddress = user.CustAddress,
                    CustCity = user.CustCity,
                    CustCountry = user.CustCountry,
                    CustEmail = user.CustEmail,
                    CustProv = user.CustProv.ToUpper(),  // ab => AB
                    Password = user.Password,
                    Username = user.Username
                };
                try
                {
                    // 4. if insert successfully, go to login page show success msg
                    await CustomerProfileManager.Add(newCust);
                    ViewBag.success = "Congratulations! Your account is active now, please log in.";
                    return View("Login");
                }
                catch (Exception e)
                {
                    // 5. if insert failed, go to register page with old inputs and error msg
                    ViewBag.msg = "username is already in use, please login.";
                    ViewBag.reason = e.InnerException.Message;  // sqlserver exception message, not very readable
                    return View("Register", user);
                }
            }
        }

        // GET: user access login page
        public async Task<IActionResult> Login()
        {
            return View();
        }

        // POST: user try to login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            // compare input username and password against database
            var cust = await CustomerProfileManager.CompareLogin(login.username, login.password);

            if (cust != null)
            {
                // if username and pin match, add user to session
                HttpContext.Session.SetObject("login", cust);
                // direct to history page, with parameter customerId
                return RedirectToAction("CustomerHistory", new { customerId = cust.CustomerId });
            }
            else
            {
                // if username and pin don't match, go back to login page with error msg
                ViewBag.msg = "Sorry, username or password is invalid.";
                return View("Login");
            }
        }

        // GET: logout
        public IActionResult Logout()
        {
            // unset session
            HttpContext.Session.Remove("login");
            return View("Login");
        }

        // GET: see profile if logged in
        public IActionResult Profile()
        {
            // if try access profile without login, go to login page
            var loginCust = HttpContext.Session.GetObject<Customers>("login");

            if (loginCust == null)
                return View("Login");
            else
            {
                var id = loginCust.CustomerId;
                var profile = CustomerProfileManager.Find(id);

                //return View(loginCust);
                return View(profile);
            }
        }

        // ------------------------ END DongMing Hu -------------------------

        // GET: Bookings by customer (Customer Booking History)
        public ActionResult CustomerHistory()
        {
            if (HttpContext.Session.GetObject<CustomerProfileManager>("login") != null) {

                //var cust = HttpContext.Session.TryGetValue("login", );

                var cust = HttpContext.Session.GetObject<Customers>("login");

                // Change this to Id retrieved from sessions
                int id = cust.CustomerId;

                var bookings = BookingsManager.GetAllBookingsByCustomer(id)
                    .Select(bk => new BookingsModel
                    {
                        BookingId = bk.BookingId,
                        BookingDate = bk.BookingDate,
                        BookingNo = bk.BookingNo,
                        TravelerCount = bk.TravelerCount,
                        CustomerId = bk.Customer.CustFirstName,
                        TripTypeId = bk.TripType.Ttname,
                        PackageId = bk.Package.PkgName,
                        Price = Math.Round((decimal)(bk.Package.PkgBasePrice + bk.Package.PkgAgencyCommission), 0),
                        //Total = TotalOwing(bk.Package.PkgBasePrice).ToString("c")
                    }).ToList();

                return View(bookings);


            } else {
                return View("Login");
                //return RedirectToAction("Login");
            }
        }


        public ActionResult BookingDetail(int id)
        {
            var booking = BookingsManager.Find(id);

            var a = new BookingsModel
            {
                BookingId = booking.BookingId,
                BookingNo = booking.BookingNo,
                PkgStartDate = booking.Package.PkgStartDate,
                PkgEndDate = booking.Package.PkgEndDate,
                TravelerCount = booking.TravelerCount,
                CustomerId = booking.Customer.CustFirstName,
                TripTypeId = booking.TripType.Ttname,
                PkgDesc = booking.Package.PkgDesc,
                PackageId = booking.Package.PkgName,
                Price = Math.Round((decimal)(booking.Package.PkgBasePrice + booking.Package.PkgAgencyCommission), 0),
            };

            return View(a);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers.FindAsync(id);
            if (customers == null)
            {
                return NotFound();
            }
            ViewData["AgentId"] = new SelectList(_context.Agents, "AgentId", "AgentId", customers.AgentId);
            return View(customers);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,CustFirstName,CustLastName,CustAddress,CustCity,CustProv,CustPostal,CustCountry,CustHomePhone,CustBusPhone,CustEmail,AgentId,Username,Password")] Customers customers)
        {
            if (id != customers.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomersExists(customers.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Profile));
            }
            ViewData["AgentId"] = new SelectList(_context.Agents, "AgentId", "AgentId", customers.AgentId);
            return View(customers);
        }

        // Convenient Methods -------

        // Validate a UserViewModel object, return bool
        private bool ValidateUser(UserViewModel user)
        {
            bool isValid = true;
            var phoneRegex = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
            var zipRegex = @"^[A-Za-z]\d[A-Za-z][-\ ]?\d[A-Za-z]\d$";

            if (user.CustLastName == "" || user.CustFirstName == "" ||
                user.Password != user.PasswordConfirm ||
                !Regex.IsMatch(user.CustBusPhone, phoneRegex) ||
                !Regex.IsMatch(user.CustPostal, zipRegex))
            {
                isValid = false;
            }

            return isValid;
        }


        //This wasn't used eventually, TODO: Nuke it
        public decimal TotalOwing(decimal amount)
        {
            decimal total = 0;

            total += amount;

            return total;
        }


        // ------------------ Auto-generated Actions Below ------------------------

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var travelExpertsContext = _context.Customers.Include(c => c.Agent);
            return View(await travelExpertsContext.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .Include(c => c.Agent)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["AgentId"] = new SelectList(_context.Agents, "AgentId", "AgentId");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CustFirstName,CustLastName,CustAddress,CustCity,CustProv,CustPostal,CustCountry,CustHomePhone,CustBusPhone,CustEmail,AgentId,Username,Password")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgentId"] = new SelectList(_context.Agents, "AgentId", "AgentId", customers.AgentId);
            return View(customers);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .Include(c => c.Agent)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customers = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomersExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
