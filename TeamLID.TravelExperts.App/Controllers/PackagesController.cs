/*
Author: Ibraheem
Purpose: Retrieve and display all packages
*/
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
    public class PackagesController : Controller
    {
        private readonly TravelExpertsContext _context;

        public object PackagesManager { get; private set; }

        public PackagesController(TravelExpertsContext context)
        {
            _context = context;
        }

        // GET: Packages
        //async Task<>
        //await _context.Packages.ToListAsync()
        public ActionResult Index()
        {
            var packages = PackagesDataManager.GetAll()
                .Select(pck => new PackagesModel
                {
                    PackageId = pck.PackageId,
                    PkgName = pck.PkgName,
                    PkgStartDate = pck.PkgStartDate,
                    PkgEndDate = pck.PkgEndDate,
                    PkgDesc = pck.PkgDesc,
                    PkgBasePrice = Convert.ToString(Math.Round((decimal)(pck.PkgBasePrice + pck.PkgAgencyCommission), 0))
                }).ToList();

            return View(packages);

        }

        // GET: Packages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packages = await _context.Packages
                .FirstOrDefaultAsync(m => m.PackageId == id);
            if (packages == null)
            {
                return NotFound();
            }

            return View(packages);
        }

        // GET: Packages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Packages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PackageId,PkgName,PkgStartDate,PkgEndDate,PkgDesc,PkgBasePrice,PkgAgencyCommission")] Packages packages)
        {
            if (ModelState.IsValid)
            {
                _context.Add(packages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(packages);
        }

        // GET: Packages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packages = await _context.Packages.FindAsync(id);
            if (packages == null)
            {
                return NotFound();
            }
            return View(packages);
        }

        // POST: Packages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PackageId,PkgName,PkgStartDate,PkgEndDate,PkgDesc,PkgBasePrice,PkgAgencyCommission")] Packages packages)
        {
            if (id != packages.PackageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(packages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackagesExists(packages.PackageId))
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
            return View(packages);
        }

        // GET: Packages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packages = await _context.Packages
                .FirstOrDefaultAsync(m => m.PackageId == id);
            if (packages == null)
            {
                return NotFound();
            }

            return View(packages);
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var packages = await _context.Packages.FindAsync(id);
            _context.Packages.Remove(packages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PackagesExists(int id)
        {
            return _context.Packages.Any(e => e.PackageId == id);
        }
    }
}
