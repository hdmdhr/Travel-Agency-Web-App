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
        private bool PackagesExists(int id)
        {
            return _context.Packages.Any(e => e.PackageId == id);
        }
    }
}
