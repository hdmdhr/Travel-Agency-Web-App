/*
Author: Louise, Ibraheem
Purpose: Get agents for contact page
*/

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

        // GET: Agents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agents = await _context.Agents
                .Include(a => a.Agency)
                .FirstOrDefaultAsync(m => m.AgentId == id);
            if (agents == null)
            {
                return NotFound();
            }

            return View(agents);
        }

        // GET: Agents/Create
        public IActionResult Create()
        {
            ViewData["AgencyId"] = new SelectList(_context.Agencies, "AgencyId", "AgencyId");
            return View();
        }

        // POST: Agents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgentId,AgtFirstName,AgtMiddleInitial,AgtLastName,AgtBusPhone,AgtEmail,AgtPosition,AgencyId")] Agents agents)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agents);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgencyId"] = new SelectList(_context.Agencies, "AgencyId", "AgencyId", agents.AgencyId);
            return View(agents);
        }

        // GET: Agents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agents = await _context.Agents.FindAsync(id);
            if (agents == null)
            {
                return NotFound();
            }
            ViewData["AgencyId"] = new SelectList(_context.Agencies, "AgencyId", "AgencyId", agents.AgencyId);
            return View(agents);
        }

        // POST: Agents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AgentId,AgtFirstName,AgtMiddleInitial,AgtLastName,AgtBusPhone,AgtEmail,AgtPosition,AgencyId")] Agents agents)
        {
            if (id != agents.AgentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentsExists(agents.AgentId))
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
            ViewData["AgencyId"] = new SelectList(_context.Agencies, "AgencyId", "AgencyId", agents.AgencyId);
            return View(agents);
        }

        // GET: Agents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agents = await _context.Agents
                .Include(a => a.Agency)
                .FirstOrDefaultAsync(m => m.AgentId == id);
            if (agents == null)
            {
                return NotFound();
            }

            return View(agents);
        }

        // POST: Agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agents = await _context.Agents.FindAsync(id);
            _context.Agents.Remove(agents);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentsExists(int id)
        {
            return _context.Agents.Any(e => e.AgentId == id);
        }
    }
}
