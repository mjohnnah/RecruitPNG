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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace RecruitPNG.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class ResumesController : Controller
    {
        private readonly IResumeService  resumeService;
        private readonly ICityService cityService;
        private readonly ICountyService countyService;
        private readonly ICountryService countryService;
        private readonly IOccupationService occupationService;
        private readonly IHostingEnvironment _environment;
        public ResumesController(IResumeService resumeService, ICityService cityService, ICountyService countyService,
           ICountryService countryService, IOccupationService occupationService, IHostingEnvironment _environment)
        {
            this.resumeService = resumeService;
            this.cityService = cityService;
            this.countryService = countryService;
            this.countyService = countyService;
            this.occupationService = occupationService;
            this._environment = _environment;
        }

        // GET: Admin/Resumes
        public IActionResult Index()
        {
           
            return View(resumeService.GetAll());
        }

        // GET: Admin/Resumes/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resume = resumeService.Get(id);
            if (resume == null)
            {
                return NotFound();
            }

            return View(resume);
        }

        // GET: Admin/Resumes/Create
        public IActionResult Create()
        {
            var resume = new Resume();
            ViewData["CityId"] = new SelectList(cityService.GetAll(), "Id", "Name");
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name");
            ViewData["CountyId"] = new SelectList(countyService.GetAll(), "Id", "Name");
            ViewData["OccupationId"] = new SelectList(occupationService.GetAll(), "Id", "Name");
            return View(resume);
        }

        // POST: Admin/Resumes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ResumeName,FirstName,LastName,Photo,Gender,MaritalStatus,MobilePhone,Email,TotalExperience,UsingCigarette,IsTravelDisabled,IsDisabled,IsSubsidized,IsCurrentlyWorking,IsSeekingJob,LastCompany,LastPosition,MilitaryStatus,LastEducation,LastDepartment,GraduationYear,ForeignLanguages,IsApproved,IsActive,OccupationId,Objective,CountryId,CityId,CountyId,Projects,Skills,Hobbies,DrivingLicense,Courses,Blog,LinkedIn,GitHub,Dribbble,Behance,Youtube,Instagram,Facebook,Twitter,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,IPAddress")] Resume resume, IFormFile upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.Length > 0)
                {
                    //upload işlemi burada yapılır.
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                    var path = Path.Combine(_environment.WebRootPath, "Uploads", "Resumes");
                    var filePath = Path.Combine(path, fileName);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        upload.CopyTo(stream);
                    }
                    resume.Photo = fileName;
                }
                resumeService.Insert(resume);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(cityService.GetAll(), "Id", "Name", resume.CityId);
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name", resume.CountryId);
            ViewData["CountyId"] = new SelectList(countyService.GetAll(), "Id", "Name", resume.CountyId);
            ViewData["OccupationId"] = new SelectList(occupationService.GetAll(), "Id", "Name", resume.OccupationId);
            return View(resume);
        }

        // GET: Admin/Resumes/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resume = resumeService.Get(id);
            if (resume == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(cityService.GetAll(), "Id", "Name", resume.CityId);
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name", resume.CountryId);
            ViewData["CountyId"] = new SelectList(countyService.GetAll(), "Id", "Name", resume.CountyId);
            ViewData["OccupationId"] = new SelectList(occupationService.GetAll(), "Id", "Name", resume.OccupationId);
            return View(resume);
        }

        // POST: Admin/Resumes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("ResumeName,FirstName,LastName,Photo,Gender,MaritalStatus,MobilePhone,Email,TotalExperience,UsingCigarette,IsTravelDisabled,IsDisabled,IsSubsidized,IsCurrentlyWorking,IsSeekingJob,LastCompany,LastPosition,MilitaryStatus,LastEducation,LastDepartment,GraduationYear,ForeignLanguages,IsApproved,IsActive,OccupationId,Objective,CountryId,CityId,CountyId,Projects,Skills,Hobbies,DrivingLicense,Courses,Blog,LinkedIn,GitHub,Dribbble,Behance,Youtube,Instagram,Facebook,Twitter,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,IPAddress")] Resume resume, IFormFile upload)
        {
            if (id != resume.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (upload != null && upload.Length > 0)
                    {
                        //upload işlemi burada yapılır.
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                        var path = Path.Combine(_environment.WebRootPath, "Uploads", "Resumes");
                        var filePath = Path.Combine(path, fileName);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            upload.CopyTo(stream);
                        }
                        resume.Photo = fileName;
                    }
                    resumeService.Update(resume);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResumeExists(resume.Id))
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
            ViewData["CityId"] = new SelectList(cityService.GetAll(), "Id", "Name", resume.CityId);
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name", resume.CountryId);
            ViewData["CountyId"] = new SelectList(countyService.GetAll(), "Id", "Name", resume.CountyId);
            ViewData["OccupationId"] = new SelectList(occupationService.GetAll(), "Id", "Name", resume.OccupationId);
            return View(resume);
        }

        // GET: Admin/Resumes/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resume = resumeService.Get(id);

            if (resume == null)
            {
                return NotFound();
            }

            return View(resume);
        }

        // POST: Admin/Resumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var resume = resumeService.Get(id);

            return RedirectToAction(nameof(Index));
        }

        private bool ResumeExists(string id)
        {
            return resumeService.GetAll().Any(e => e.Id == id);
        }
    }
}
