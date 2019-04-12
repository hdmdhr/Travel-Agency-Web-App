using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamLID.TravelExperts.Repository.Domain;

namespace TeamLID.TravelExperts.App.Controllers
{
    public class AgentsController : Controller
    {
        private readonly TravelExpertsContext _context;

        public AgentsController(TravelExpertsContext context)
        {
            _context = context;
        }

        // GET: Agents
        public async Task<IActionResult> Index()
        {
            var travelExpertsContext = _context.Agents.Include(a => a.Agency);
            return View(await travelExpertsContext.ToListAsync());
        }


    }
}
