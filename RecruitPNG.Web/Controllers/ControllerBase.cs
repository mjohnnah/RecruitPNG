using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecruitPNG.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using RecruitPNG.Data;
using Microsoft.EntityFrameworkCore;

namespace RecruitPNG.Web.Controllers
{
    public class ControllerBase : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var _messageService = HttpContext.RequestServices.GetService(typeof(IMessageService)) as MessageService;
            if (User.IsInRole("Candidate"))
            {
                var _resumeService = HttpContext.RequestServices.GetService(typeof(IResumeService)) as ResumeService;
                var myresumes = _resumeService.GetAllByUserName(User.Identity.Name).Select(c => c.Id).ToList(); // resumes
                var mymessages = _messageService.GetAllByTo(myresumes);
                
                ViewBag.MessageCount = mymessages.Count().ToString();
                ViewBag.Messages = mymessages;             
                
            }
            else
            {
                var _companyService = HttpContext.RequestServices.GetService(typeof(ICompanyService)) as CompanyService;
                var mycompanies = _companyService.GetAllByUserName(User.Identity.Name).Select(c => c.Id).ToList();
                var mymessages = _messageService.GetAllByTo(mycompanies);
                ViewBag.MessageCount = mymessages.Count().ToString();
                ViewBag.Messages = mymessages;
                var _jobapplicationService = HttpContext.RequestServices.GetService(typeof(IJobApplicationService)) as JobApplicationService;
                var jobService = HttpContext.RequestServices.GetService(typeof(IJobService)) as JobService;
                var jobids = jobService.GetAllByUserName(User.Identity.Name).Select(s => s.Id).ToList();
                var myapplications = _jobapplicationService.GetAllByJobs(jobids);
                ViewBag.jobApplicationCount = myapplications.Count().ToString();

            }
            base.OnActionExecuting(context);
        }
    }
}