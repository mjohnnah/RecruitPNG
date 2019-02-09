using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecruitPNG.Data;
using RecruitPNG.Models;
using Microsoft.AspNetCore.Authorization;

namespace RecruitPNG.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LocationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Location
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Counties.Include(c => c.City).Include(c => c.Country);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Location/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var county = await _context.Counties
                .Include(c => c.City)
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (county == null)
            {
                return NotFound();
            }

            return View(county);
        }

        // GET: Admin/Location/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            return View();
        }

        // POST: Admin/Location/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CountryId,CityId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,IPAddress")] County county)
        {
            if (ModelState.IsValid)
            {
                _context.Add(county);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Id", county.CityId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", county.CountryId);
            return View(county);
        }

        // GET: Admin/Location/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var county = await _context.Counties.FindAsync(id);
            if (county == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", county.CityId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", county.CountryId);
            return View(county);
        }

        // POST: Admin/Location/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,CountryId,CityId,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,IPAddress")] County county)
        {
            if (id != county.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(county);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountyExists(county.Id))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Id", county.CityId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", county.CountryId);
            return View(county);
        }

        // GET: Admin/Location/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var county = await _context.Counties
                .Include(c => c.City)
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (county == null)
            {
                return NotFound();
            }

            return View(county);
        }

        // POST: Admin/Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var county = await _context.Counties.FindAsync(id);
            _context.Counties.Remove(county);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountyExists(string id)
        {
            return _context.Counties.Any(e => e.Id == id);
        }
    }
}
