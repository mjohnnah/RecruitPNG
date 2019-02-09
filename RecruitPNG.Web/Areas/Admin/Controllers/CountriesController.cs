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
    public class CountriesController : Controller
    {
        private readonly ICountryService countryService;
        private readonly IResumeService resumeService;

        public CountriesController(ICountryService countriesService, IResumeService resumeService)
        {
            this.countryService = countriesService;
            this.resumeService = resumeService;
        }

        // GET: Admin/Countries
        public IActionResult Index()
        {
            return View(countryService.GetAll());
        }

        // GET: Admin/Countries/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = countryService.Get(id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Admin/Countries/Create
        public IActionResult Create()
        {
            var country = new Country();
            return View(country);
        }

        // POST: Admin/Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,IPAddress")] Country country)
        {
            if (ModelState.IsValid)
            {
              countryService.Insert(country);
                return RedirectToAction(nameof(Index));
            }

            return View(country);
        }

        // GET: Admin/Countries/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = countryService.Get(id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Admin/Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Name,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,IPAddress")] Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    countryService.Update(country);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id))
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

            return View(country);
        }

        // GET: Admin/Countries/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = countryService.Get(id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Admin/Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            try
            {
                countryService.Delete(id);

            }
            catch (Exception)
            {

                throw;
            }
            countryService.Delete(id);



            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(string id)
        {
            return countryService.GetAll().Any(c => c.Id == id);
        }
    }
}
