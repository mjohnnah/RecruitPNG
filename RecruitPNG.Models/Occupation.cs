using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecruitPNG.Models
{
    public class Occupation:BaseEntity
    {
        [Display(Name = "Occupation")]
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
    }
}
