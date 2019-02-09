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
    public class JobsController : Controller
	{
		private readonly IJobService jobService;
		private readonly ICompanyService companyService;
        private readonly ICountryService countryService;
        private readonly ICityService cityService;
        private readonly ICountyService countyService;

        public JobsController(IJobService jobService, ICompanyService companyService, ICountryService countryService, ICityService cityService, ICountyService countyService)
		{
			this.jobService = jobService;
			this.companyService = companyService;
            this.countryService = countryService;
            this.cityService = cityService;
            this.countyService = countyService;
		}


		// GET: Admin/Jobs
		public IActionResult Index()
		{
			var jobs = jobService.GetAll();
			return View(jobs);
		}

		// GET: Admin/Jobs/Details/5
		public IActionResult Details(string id)
		{
			var job = jobService.Get(id);
			return View(job);

		}

		// GET: Admin/Jobs/Create
		public IActionResult Create()
		{
			ViewBag.CompanyId = new SelectList(companyService.GetAll(), "Id", "Name");
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name");
            ViewData["CityId"] = new SelectList(cityService.GetAll(), "Id", "Name");
            ViewData["CountyId"] = new SelectList(countyService.GetAll(), "Id", "Name");
            var job = new Job
            {
                PublishDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1)
            };
            return View(job);
		}

		// POST: Admin/Jobs/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Title,Description,CompanyId,CountryId,CityId,CountyId,PublishDate,EndDate,IsApproved,IsActive,IsFeatured,Position,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,IPAddress")] Job job)
		{
			if (ModelState.IsValid)
			{
				jobService.Insert(job);
				return RedirectToAction(nameof(Index));
			}
			
			// GET: Admin/Jobs/Edit/5
			ViewBag.CompanyId = new SelectList(companyService.GetAll(), "Id", "Name");
			return View(job);
		}
        public IActionResult Edit(string id)
        {
            var job = jobService.Get(id);
            ViewBag.CompanyId = new SelectList(companyService.GetAll(), "Id", "Name", job.CompanyId);
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name");
            ViewData["CityId"] = new SelectList(cityService.GetAll(), "Id", "Name");
            ViewData["CountyId"] = new SelectList(countyService.GetAll(), "Id", "Name");
            //ViewBag.CityId = new SelectList(cityService.GetAll(), "Id", "Name", job.CityId);
            //ViewBag.CountyId = new SelectList(countyService.GetAll(), "Id", "Name", job.CountyId);
            return View(job);
            
        }
        // POST: Admin/Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(string id, [Bind("Title,Description,CompanyId,CountryId,CityId,CountyId,PublishDate,EndDate,IsApproved,IsActive,IsFeatured,Position,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,IPAddress")] Job job)
		{
			if (id != job.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					jobService.Update(job);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!JobExists(job.Id))
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
			ViewBag.CompanyId = new SelectList(companyService.GetAll(), "Id", "Name"); 
			return View(job);
		}

		// GET: Admin/Jobs/Delete/5
		public IActionResult Delete(string id)
		{
			var job = jobService.Get(id);
			return View(job);
		}

		// POST: Admin/Jobs/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(string id)
		{
			jobService.Delete(id);
			return RedirectToAction(nameof(Index));
		}

		private bool JobExists(string id)
		{
			return jobService.GetAll().Any(e => e.Id == id);
		}
	}
}
