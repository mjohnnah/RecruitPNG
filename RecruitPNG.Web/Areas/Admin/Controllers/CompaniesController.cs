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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace RecruitPNG.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class CompaniesController : Controller
    {
        private readonly ICompanyService companyService;
        private readonly ICountyService countyService;
        private readonly ICountryService countryService;
        private readonly ICityService cityService;
        private readonly ISectorService sectorService;
        private readonly IHostingEnvironment _environment;
        public CompaniesController(ICompanyService companyService, ICountyService countyService, ICountryService countryService, ICityService cityService, ISectorService sectorService, IHostingEnvironment environment)
        {
            this.companyService = companyService;
            this.countyService = countyService;
            this.countryService = countryService;
            this.cityService = cityService;
            this.sectorService = sectorService;
            this._environment = environment;
        }

        // GET: Admin/Companies
        public IActionResult Index()
        {
            return View(companyService.GetAll());
        }


        // GET: Admin/Companies/Details/5
        public IActionResult Details(string id)
        {
            

            var company = companyService.Get(id);
            
                return View(company);
        }

        // GET: Admin/Companies/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(cityService.GetAll(), "Id", "Name");
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name");
            ViewData["CountyId"] = new SelectList(countyService.GetAll(), "Id", "Name");
            ViewData["SectorId"] = new SelectList(sectorService.GetAll(), "Id", "Name");
            var company = new Company();
            return View(company);
        }

        // POST: Admin/Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,CountryId,CityId,CountyId,Address,SectorId,IsApproved,IsActive,Phone,Email,ContactName,Logo,Website,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,IPAddress")] Company company, IFormFile File)
        {
            if (ModelState.IsValid)
            {
                if (File != null && File.Length > 0)
                {
                    // upload işlemi yapmak için konum belirle
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(File.FileName).ToLower();
                    string path = Path.Combine(_environment.WebRootPath, "Uploads", "Companies", fileName);

                    // uploads dizini yoksa oluştur
                    if (!Directory.Exists(Path.Combine(_environment.WebRootPath, "Uploads", "Companies")))
                    {
                        Directory.CreateDirectory(Path.Combine(_environment.WebRootPath, "Uploads", "Companies"));
                    }

                    // belirlenen konuma upload yapılır
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await File.CopyToAsync(stream);
                    }

                    // dosya adı entity'e atanır
                    company.Logo = fileName;
                }
                companyService.Insert(company);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(cityService.GetAll(), "Id", "Name");
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name");
            ViewData["CountyId"] = new SelectList(countyService.GetAll(), "Id", "Name");
            ViewData["SectorId"] = new SelectList(sectorService.GetAll(), "Id", "Name");
            return View(company);
        }

        // GET: Admin/Companies/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = companyService.Get(id);
            if (company == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(cityService.GetAll(), "Id", "Name", company.CityId);
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name", company.CountryId);
            ViewData["CountyId"] = new SelectList(countyService.GetAll(), "Id", "Name", company.CountyId);
            ViewData["SectorId"] = new SelectList(sectorService.GetAll(), "Id", "Name", company.SectorId);
            return View(company);
        }

        // POST: Admin/Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Description,CountryId,CityId,CountyId,Address,SectorId,IsApproved,IsActive,Phone,Email,ContactName,Logo,Website,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,IPAddress")] Company company, IFormFile File)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (File != null && File.Length > 0)
                    {
                        // upload işlemi yapmak için konum belirle
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(File.FileName).ToLower();
                        string path = Path.Combine(_environment.WebRootPath, "Uploads", "Companies", fileName);

                        // uploads dizini yoksa oluştur
                        if (!Directory.Exists(Path.Combine(_environment.WebRootPath, "Uploads", "Companies")))
                        {
                            Directory.CreateDirectory(Path.Combine(_environment.WebRootPath, "Uploads", "Companies"));
                        }

                        // belirlenen konuma upload yapılır
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await File.CopyToAsync(stream);
                        }

                        // dosya adı entity'e atanır
                        company.Logo = fileName;
                    }
                    companyService.Update(company);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!companyExists(company.Id))
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
        
            ViewData["CityId"] = new SelectList(cityService.GetAll(), "Id", "Name", company.CityId);
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name", company.CountryId);
            ViewData["CountyId"] = new SelectList(countyService.GetAll(), "Id", "Name", company.CountyId);
            ViewData["SectorId"] = new SelectList(sectorService.GetAll(), "Id", "Name", company.SectorId);
            return View(company);
        }

        // GET: Admin/Companies/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = companyService.Get(id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Admin/Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {

            companyService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool companyExists(string id)
        {
            return companyService.GetAll().Any(e => e.Id == id);
        }
    }
}
