using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RecruitPNG.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<RecruitPNG.Models.ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public UsersController(UserManager<RecruitPNG.Models.ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var users = userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            await userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            ViewBag.Roles = roleManager.Roles.ToList();
            ViewBag.UserRoles = await userManager.GetRolesAsync(user);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, string currentPassword, string newPassword, string[] SelectedRoles)
        {
            var user = await userManager.FindByIdAsync(id);
            if (!string.IsNullOrEmpty(newPassword)) { 
                var result = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                if (result.Errors.Count()>0) { 
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("Password", error.Description);
                    }
                    return View(user);
                }
            }
            var roles = await userManager.GetRolesAsync(user);
            userManager.RemoveFromRolesAsync(user, roles).Wait();
            await userManager.AddToRolesAsync(user, SelectedRoles);
            return RedirectToAction("Index");
        }
        
    }
}