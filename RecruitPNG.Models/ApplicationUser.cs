using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecruitPNG.Models
{
    public class ApplicationUser: IdentityUser
    {
        [StringLength(200)]
        [Display(Name = "FullName")]
        public string FullName { get; set; }
        [StringLength(200)]
        [Display(Name = "Id Photo")]
        public string Photo { get; set; }
    }
}
