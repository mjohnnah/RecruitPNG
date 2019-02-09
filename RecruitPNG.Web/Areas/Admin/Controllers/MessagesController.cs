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

namespace RecruitPNG.Web.Areas.Admin.Controllers
{
   

    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class MessagesController : Controller
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

        // GET: Admin/Messages
        public IActionResult Index()
        {
            return View(messageService.GetAll());
        }

        // GET: Admin/Messages/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MessageService = messageService.Get(id);
            if (MessageService == null)
            {
                return NotFound();
            }

            return View(MessageService);
        }

        // GET: Admin/Messages/Create
        public IActionResult Create()
        {
            var message = new Message();
            return View(message);
        }

        // POST: Admin/Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("From,FromName,To,ToName,Title,Description,IsRead,IsApproved,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,IPAddress")] Message message)
        {
            if (ModelState.IsValid)
            {
                messageService.Insert(message);
                return RedirectToAction(nameof(Index));
            }
           
            return View(message);
        }

        // GET: Admin/Messages/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = messageService.Get(id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        // POST: Admin/Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("From,FromName,To,ToName,Title,Description,IsRead,IsApproved,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,IPAddress")] Message message)
        {
            if (id != message.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    messageService.Update(message);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.Id))
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
            return View(message);
        }

        // GET: Admin/Messages/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = messageService.Get(id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Admin/Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            messageService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(string id)
        {
            return messageService.GetAll().Any(e => e.Id == id);
        }
    }
}
