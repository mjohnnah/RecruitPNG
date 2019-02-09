using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecruitPNG.Models
{
    public class Sector:BaseEntity
    {
        [Display(Name = "Sector")]
        [Required(ErrorMessage = "Sector required.")]
        [StringLength(200)]
        public string Name { get; set; }
    }
}
