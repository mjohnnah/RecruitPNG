using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RecruitPNG.Models;
using RecruitPNG.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecruitPNG.Web.Controllers
{
    public class ResumesController : ControllerBase
    {
        private readonly IResumeService resumeService;
        private readonly IOccupationService occupationService;
        private readonly IHostingEnvironment _environment;
        private readonly ICountryService countryService;
        private readonly ICityService cityService;
        private readonly ICountyService countyService;
        private readonly IExperienceService experienceService;
        public ResumesController(IResumeService resumeService, IExperienceService experienceService, IOccupationService occupationService,IHostingEnvironment _environment, ICountryService countryService, ICityService cityService, ICountyService countyService)
        {
            this._environment = _environment;
            this.resumeService = resumeService;
            this.occupationService = occupationService;
            this.countryService = countryService;
            this.cityService = cityService;
            this.countyService = countyService;
            this.experienceService = experienceService;
        }
        [Authorize(Roles = "Compnay")]
        public IActionResult Index()
        {
            var resumes = resumeService.GetAll();
            return View(resumes);
        }
        [Authorize(Roles = "Candidate")]
        public IActionResult Create()
        {
            var resume = new Resume();
            ViewData["OccupationId"] = new SelectList(occupationService.GetAll(), "Id", "Name");
            ViewData["CountryId"] = new SelectList(countryService.GetAll(),"Id","Name");            
            ViewData["CityId"] = new SelectList(cityService.GetAll(), "Id","Name");
            ViewData["CountyId"] = new SelectList(countyService.GetAll(),"Id", "Name");
            return View(resume);
        }
        [Authorize(Roles = "Candidate")]
        [HttpPost]
        public IActionResult Create(Resume resume, IFormFile upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.Length > 0)
                {
                    //upload işlemi burada yapılır.
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                    var path = Path.Combine(_environment.WebRootPath, "Uploads","Resumes");
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
                return RedirectToAction(nameof(MyResumes), new { id = resume.Id, saved = true });
            }
            ViewData["OccupationId"] = new SelectList(occupationService.GetAll(), "Id", "Name",resume.OccupationId);
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name", resume.CountryId);
            ViewData["CityId"] = new SelectList(cityService.GetAllByCountryId(resume.CountryId), "Id", "Name", resume.CityId);
            ViewData["CountyId"] = new SelectList(countyService.GetAllByCityId(resume.CityId), "Id", "Name", resume.CountyId);
            return View(resume);
        }
        [Authorize(Roles = "Candidate")]
        public IActionResult Edit(string id, bool saved)
        {
            ViewBag.Saved = saved;
            var resume = resumeService.Get(id);
            ViewData["OccupationId"] = new SelectList(occupationService.GetAll(), "Id", "Name",resume.OccupationId);
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name", resume.CountryId);
            ViewData["CityId"] = new SelectList(cityService.GetAllByCountryId(resume.CountryId), "Id", "Name", resume.CityId);
            ViewData["CountyId"] = new SelectList(countyService.GetAllByCityId(resume.CityId), "Id", "Name", resume.CountyId);
            return View(resume);
        }
        [Authorize(Roles = "Candidate")]
        [HttpPost]
        public IActionResult Edit(Resume resume, IFormFile upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.Length > 0)
                {
                    //upload image and resume files
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
                return RedirectToAction(nameof(Edit), new { id = resume.Id, saved = true });
            }
            ViewData["OccupationId"] = new SelectList(occupationService.GetAll(), "Id", "Name",resume.OccupationId);
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name", resume.CountryId);
            ViewData["CityId"] = new SelectList(cityService.GetAllByCountryId(resume.CountryId), "Id", "Name", resume.CityId);
            ViewData["CountyId"] = new SelectList(countyService.GetAllByCityId(resume.CityId), "Id", "Name", resume.CountyId);
            return View();
        }
        public IActionResult Details(string id)
        {
            var resume = resumeService.Get(id);
            return View(resume);
        }
        [Authorize(Roles = "Candidate")]
        public ActionResult Delete(string id)
        {
            resumeService.Delete(id);
            return RedirectToAction("MyResumes");
        }
        [Authorize(Roles = "Candidate")]
        public IActionResult MyResumes()
        {
            var myresumes = resumeService.GetAllByUserName(User.Identity.Name).ToList(); // resumes
            return View(myresumes);
        }

        [Authorize(Roles = "Candidate")]
        public IActionResult AddExperience(Experience experience)
        {
            if (ModelState.IsValid) {
                experienceService.Insert(experience);
                return Content("Experience added.");
            }
            return Content(ModelState.ErrorCount.ToString() + " Update error. Model state error updating your resume...");
        }
    }
}