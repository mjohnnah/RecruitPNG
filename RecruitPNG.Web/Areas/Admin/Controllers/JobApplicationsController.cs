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
    public class JobApplicationsController : Controller
    {
        private readonly IJobApplicationService jobApplicationService;
        private readonly IJobService jobService;
        private readonly IResumeService resumeService;

        public JobApplicationsController(IJobApplicationService jobApplicationService, IJobService jobService, IResumeService resumeService)
        {
            this.jobApplicationService = jobApplicationService;
            this.jobService = jobService;
            this.resumeService = resumeService;
        }

        // GET: Admin/JobApplications
        public IActionResult Index()
        {
            return View(jobApplicationService.GetAll());
        }

        // GET: Admin/JobApplications/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplication = jobApplicationService.Get(id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            return View(jobApplication);
        }

        // GET: Admin/JobApplications/Create
        public IActionResult Create()
        {
            
            ViewData["JobId"] = new SelectList(jobService.GetAll(), "Id", "Title");
            ViewData["ResumeId"] = new SelectList(resumeService.GetAll(), "Id", "ResumeName");
            var jobApplication = new JobApplication();
            return View(jobApplication);
        }

        // POST: Admin/JobApplications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ResumeId,JobId,Title,Description,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,IPAddress")] JobApplication jobApplication)
        {
            if (ModelState.IsValid)
            {
                jobApplicationService.Insert(jobApplication);
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobId"] = new SelectList(jobService.GetAll(), "Id", "Title", jobApplication.JobId);
            ViewData["ResumeId"] = new SelectList(resumeService.GetAll(), "Id", "ResumeName", jobApplication.ResumeId);
            return View(jobApplication);
        }

        // GET: Admin/JobApplications/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplication = jobApplicationService.Get(id);
            if (jobApplication == null)
            {
                return NotFound();
            }
            ViewData["JobId"] = new SelectList(jobService.GetAll(), "Id", "Title", jobApplication.JobId);
            ViewData["ResumeId"] = new SelectList(resumeService.GetAll(), "Id", "ResumeName", jobApplication.ResumeId);
            return View(jobApplication);
        }

        // POST: Admin/JobApplications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("ResumeId,JobId,Title,Description,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,IPAddress")] JobApplication jobApplication)
        {
            if (id != jobApplication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    jobApplicationService.Update(jobApplication);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobApplicationExists(jobApplication.Id))
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
            ViewData["JobId"] = new SelectList(jobService.GetAll(), "Id", "Title", jobApplication.JobId);
            ViewData["ResumeId"] = new SelectList(resumeService.GetAll(), "Id", "ResumeName", jobApplication.ResumeId);
            return View(jobApplication);
        }

        // GET: Admin/JobApplications/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplication = jobApplicationService.Get(id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            return View(jobApplication);
        }

        // POST: Admin/JobApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            
            jobApplicationService.Delete(id);
            
            return RedirectToAction(nameof(Index));
        }

        private bool JobApplicationExists(string id)
        {
            return jobApplicationService.GetAll().Any(e => e.Id == id);
        }
    }
}
