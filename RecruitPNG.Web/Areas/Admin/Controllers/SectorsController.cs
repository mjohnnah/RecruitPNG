using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecruitPNG.Data;
using RecruitPNG.Models;
using RecruitPNG.Services;
using Microsoft.AspNetCore.Authorization;

namespace RecruitPNG.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class SectorsController : Controller
    {
        private readonly ISectorService sectorService;
        private readonly IResumeService resumeService;
        public SectorsController(ISectorService sectorsService, IResumeService resumeService)
        {
            this.sectorService = sectorsService;
            this.resumeService = resumeService;
        }

        // GET: Admin/Sectors
        public IActionResult Index()
        {
            return View(sectorService.GetAll());
        }

        // GET: Admin/Sectors/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sector = sectorService.Get(id);
            if (sector == null)
            {
                return NotFound();
            }

            return View(sector);
        }

        // GET: Admin/Sectors/Create
        public IActionResult Create()
        {
            var sector = new Sector();
            return View(sector);
        }

        // POST: Admin/Sectors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,IPAddress")] Sector sector)
        {
            if (ModelState.IsValid)
            {
                sectorService.Insert(sector);
                return RedirectToAction(nameof(Index));
            }

            return View(sector);
        }

        // GET: Admin/Sectors/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sector = sectorService.Get(id);
            if (sector == null)
            {
                return NotFound();
            }

            return View(sector);
        }

        // POST: Admin/Sectors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Name,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,IPAddress")] Sector sector)
        {
            if (id != sector.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    sectorService.Update(sector);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectorExists(sector.Id))
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

            return View(sector);
        }

        // GET: Admin/Sectors/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sector = sectorService.Get(id);
            if (sector == null)
            {
                return NotFound();
            }

            return View(sector);
        }

        // POST: Admin/Sectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {

            sectorService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool SectorExists(string id)
        {
            return sectorService.GetAll().Any(e => e.Id == id);
        }
    }
}
