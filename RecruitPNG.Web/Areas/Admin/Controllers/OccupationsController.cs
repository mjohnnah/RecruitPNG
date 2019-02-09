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
    public class OccupationsController : Controller
    {        
        private readonly IOccupationService occupationService;

        public OccupationsController(IOccupationService occupationService)
        {
            this.occupationService = occupationService;
        }

        // GET: Admin/Occupations
        public IActionResult Index()
        {
            return View(occupationService.GetAll());
        }

        // GET: Admin/Occupations/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occupation = occupationService.Get(id);
            if (occupation == null)
            {
                return NotFound();
            }

            return View(occupation);
        }

        // GET: Admin/Occupations/Create
        public IActionResult Create()
        {
            var occupation = new Occupation();
            return View(occupation);
        }

        // POST: Admin/Occupations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,IPAddress")] Occupation occupation)
        {
            if (ModelState.IsValid)
            {
                occupationService.Insert(occupation);
                return RedirectToAction(nameof(Index));
            }
            return View(occupation);
        }

        // GET: Admin/Occupations/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occupation = occupationService.Get(id);
            if (occupation == null)
            {
                return NotFound();
            }
            return View(occupation);
        }

        // POST: Admin/Occupations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Name,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,IPAddress")] Occupation occupation)
        {
            if (id != occupation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    occupationService.Update(occupation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OccupationExists(occupation.Id))
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
            return View(occupation);
        }

        // GET: Admin/Occupations/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occupation = occupationService.Get(id);
            if (occupation == null)
            {
                return NotFound();
            }

            return View(occupation);
        }

        // POST: Admin/Occupations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var occupation = occupationService.Get(id);
            return RedirectToAction(nameof(Index));
        }

        private bool OccupationExists(string id)
        {
            return occupationService.GetAll().Any(e => e.Id == id);
        }
    }
}
