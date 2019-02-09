using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecruitPNG.Models;
using RecruitPNG.Services;
using RecruitPNG.Web.ViewModels;

namespace RecruitPNG.Web.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobService jobService;
        private readonly ICompanyService companyService;
        private readonly IResumeService resumeService;
        private readonly IJobApplicationService jobApplicationService;
        //addaed
        private readonly ICountryService countryService;
        private readonly ICityService cityService;
        private readonly ICountyService countyService;

        public JobController(IJobService jobService, ICompanyService companyService, IResumeService resumeService,
            IJobApplicationService jobApplicationService, ICityService cityService, ICountyService countyService, ICountryService countryService)
        {
            this.jobService = jobService;
            this.companyService = companyService;
            this.resumeService = resumeService;
            this.jobApplicationService = jobApplicationService;
            this.countryService = countryService;
            this.cityService = cityService;
            this.countyService = countyService;
        }
        public IActionResult Index()
        {
            var jobs = jobService.GetAll();
            return View(jobs);
        }

        [Authorize(Roles = "Company")]
        //Create
        public IActionResult Create()
        {

            var job = new Job();
            ViewBag.CompanyId = new SelectList(companyService.GetAllByUserName(User.Identity.Name), "Id", "Name");
            //add
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name");
            ViewData["CountyId"] = new SelectList(countyService.GetAll(), "Id", "Name");
            ViewData["CityId"] = new SelectList(cityService.GetAll(), "Id", "Name");
            job.PublishDate = DateTime.Now;
            job.EndDate = job.PublishDate.AddDays(30);
            return View(job);

        }
        [Authorize(Roles = "Company")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Job job)
        {
            if (ModelState.IsValid)
            {
                jobService.Insert(job);
                return RedirectToAction(nameof(Edit), new { id = job.Id, saved = true });
            }
            ViewBag.CompanyId = new SelectList(companyService.GetAllByUserName(User.Identity.Name), "Id", "Name", job.CompanyId);
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name");
            ViewData["CountyId"] = new SelectList(countyService.GetAll(), "Id", "Name");
            ViewData["CityId"] = new SelectList(cityService.GetAll(), "Id", "Name");
            return View(job);

        }
        [Authorize(Roles = "Company")]
        //Edit
        public IActionResult Edit(string id, bool saved)
        {
            var job = jobService.Get(id);
            ViewBag.Saved = saved;
            ViewBag.CompanyId = new SelectList(companyService.GetAllByUserName(User.Identity.Name), "Id", "Name", job.CompanyId);
            ViewData["CountryId"] = new SelectList(countryService.GetAll(), "Id", "Name");
            ViewData["CountyId"] = new SelectList(countyService.GetAll(), "Id", "Name");
            ViewData["CityId"] = new SelectList(cityService.GetAll(), "Id", "Name");
            return View(job);
        }
        [Authorize(Roles = "Company")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Job job)
        {
            if (ModelState.IsValid)
            {
                jobService.Update(job);
                return RedirectToAction(nameof(Edit), new { id = job.Id, saved = true });
            }
            ViewBag.CompanyId = new SelectList(companyService.GetAllByUserName(User.Identity.Name), "Id", "Name", job.CompanyId);
            return View(job);

        }
        [Authorize(Roles = "Company")]
        //Delete
        public IActionResult Delete(string id)
        {
            jobService.Delete(id);
            return RedirectToAction("MyJobs");
        }
        [Authorize(Roles = "Company")]
        //MyJobs
        public IActionResult MyJobs()
        {
            var myJobs = jobService.GetAllByUserName(User.Identity.Name).ToList();
            return View(myJobs);
        }

        //Details
        public IActionResult Details(string id)
        {
            var job = jobService.Get(id);
            return View(job);

        }
        [Authorize(Roles = "Candidate")]
        public IActionResult Apply(string jobId)
        {
            var jobEmployer = "";

            var job = jobService.Get(jobId);
            jobEmployer = job.Company.Name; // kime / firma adı

            //var message = new Message() { To = job.CompanyId, ToName = jobEmployer, Title = job.Title };

            var ja = new JobApplication() { JobId = jobId};
            ViewBag.MyResumes = new SelectList(resumeService.GetAllByUserName(User.Identity.Name), "Id", "ResumeName");
            ViewData["JobId"] = new SelectList(jobApplicationService.GetAll(), "Id", "Name");
            return View(ja);
        }
        [Authorize(Roles = "Candidate")]
        [HttpPost]
        public IActionResult Apply(JobApplication ja)
        {
            if (ModelState.IsValid)
            {
                jobApplicationService.Insert(ja);
                return RedirectToAction("Success");
            }
            ViewBag.MyResumes = new SelectList(resumeService.GetAllByUserName(User.Identity.Name), "Id", "ResumeName", ja.ResumeId);
            return View(ja);

        }

        public IActionResult Success()
        {
            return View();
        }

    }
}
