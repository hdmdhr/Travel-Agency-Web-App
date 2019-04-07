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

        // First time user try to access register page
        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }

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
                    CustPostal = user.CustPostal.Replace('-',' '),  // T2G-1X6 => T2G 1X6
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
                    // 4. if insert successfully, login user(create session), redirect to history page
                    await CustomerProfileManager.Add(newCust);
                    // TODO: login user (save user in session)
                    return RedirectToAction("History", new { customerId = 1 });  // TODO: hard code, need change
                }
                catch (Exception e)
                {
                    // 5. if insert failed, go to register page with old inputs and error msg
                    ViewBag.msg = "username is already in use, please login.";
                    return View("Register", user);
                }
            }
        }

        // TODO: ----- Louise -----
        public async Task<IActionResult> History(int customerId)
        {
            // TODO: use customer id to do some query, find out purchase history, make a IEnumerable<Packages> object as model, pass it into view to display

            IEnumerable<Packages> model = null;  // instead of null, use actual query result

            return View(model);
        }

        // ------------------ Convenient Methods ---------------------

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
