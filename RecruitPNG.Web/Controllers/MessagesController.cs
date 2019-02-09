using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecruitPNG.Models;
using RecruitPNG.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecruitPNG.Web.Controllers
{
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService messageService;
        private readonly ICompanyService companyService;
        private readonly IResumeService resumeService;
        private readonly IJobService jobService;
        public MessagesController(IMessageService messageService, ICompanyService companyService, IResumeService resumeService, IJobService jobService)
        {
            this.messageService = messageService;
            this.companyService = companyService;
            this.resumeService = resumeService;
            this.jobService = jobService;
        }
        [Authorize(Roles = "Company")]
        public IActionResult MyMessagesSentToMyCompanies()
        {
            var mycompanies = companyService.GetAllByUserName(User.Identity.Name).Select(c=>c.Id).ToList();
            var mymessages = messageService.GetAllByTo(mycompanies);
            return View(mymessages);
        }
        [Authorize(Roles = "Candidate")]
        public IActionResult MyMessagesSentToMyResumes()
        {
            var myresumes = resumeService.GetAllByUserName(User.Identity.Name).Select(c => c.Id).ToList(); // resumes
            var mymessages = messageService.GetAllByTo(myresumes);
            return View(mymessages);
        }
        [Authorize(Roles = "Company")]
        public IActionResult SendMessageToResume(string resumeId)
        {
            var toName = "";
            
            var resume = resumeService.Get(resumeId);
            toName = resume.ResumeName; // resuem name
           
            var message = new Message(){ To = resumeId, ToName = toName };
            ViewBag.MyCompanies = new SelectList(companyService.GetAllByUserName(User.Identity.Name), "Id", "Name"); // resume
            return View(message);
        }

        [HttpPost]
        [Authorize(Roles = "Company")]
        public IActionResult SendMessageToResume(Message message)
        {
            if (ModelState.IsValid)
            {
                message.FromName = companyService.Get(message.From).Name; // kimden
                message.ToName = resumeService.Get(message.To).ResumeName; // kime
                messageService.Insert(message);
                return RedirectToAction("Success");
            }
            ViewBag.MyCompanies = new SelectList(companyService.GetAllByUserName(User.Identity.Name), "Id", "Name", message.From);
            return View(message);
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Details(string id)
        {
            var message = messageService.Get(id);

            return View(message);

        }
        // Candidate to employer
        [Authorize(Roles = "Candidate")]
        public IActionResult SendMessageToCompany(string companyId) // companyId: message to employer job
        {
            
            var toName = "";

            var company = companyService.Get(companyId);
            toName = company.Name + " " + company.ContactName; // employer contat

            var message = new Message() { To = companyId, ToName = toName };
            ViewBag.MyResumes = new SelectList(resumeService.GetAllByUserName(User.Identity.Name), "Id", "ResumeName"); // select to 
            return View(message);
        }

        [HttpPost]
        [Authorize(Roles = "Candidate")]
        public IActionResult SendMessageToCompany(Message message)
        {
            if (ModelState.IsValid)
            {
                message.FromName = resumeService.Get(message.From).ResumeName; // kimden / özgeçmiş
                message.ToName = companyService.Get(message.To).Name; // kime / firma
                messageService.Insert(message);
                return RedirectToAction("Success");
            }
            ViewBag.MyResumes = new SelectList(resumeService.GetAllByUserName(User.Identity.Name), "Id", "ResumeName", message.From);
            return View(message);
        }

        // aday ilan sahibi firmaya mesaj gönderiyor
        [Authorize(Roles = "Candidate")]
        public IActionResult SendMessageToJob(string jobId)
        {
            var toName = "";

            var job = jobService.Get(jobId);
            toName = job.Company.Name; // kime / firma adı

            var message = new Message() { To = job.CompanyId, ToName = toName, Title = job.Title };
            ViewBag.MyResumes = new SelectList(resumeService.GetAllByUserName(User.Identity.Name), "Id", "ResumeName"); // kimden
            return View(message);
        }

        [HttpPost]
        [Authorize(Roles = "Candidate")]
        public IActionResult SendMessageToJob(string jobId, Message message)
        {
            if (ModelState.IsValid)
            {
                message.FromName = resumeService.Get(message.From).ResumeName; // kimden
                message.ToName = companyService.Get(message.To).Name; // kime
                messageService.Insert(message);
                return RedirectToAction("Success");
            }
            ViewBag.MyResumes = new SelectList(resumeService.GetAllByUserName(User.Identity.Name), "Id", "ResumeName", message.From);
            return View(message);
        }
    }
}