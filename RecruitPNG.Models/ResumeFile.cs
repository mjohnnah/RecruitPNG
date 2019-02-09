using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RecruitPNG.Models
{
    public class ResumeFile:BaseEntity
    {
        [Display(Name = "File")]
        [Required(ErrorMessage = "Resume file required.")]
        [StringLength(200, ErrorMessage = "Resume file name must be no longer than 200 characters.")]
        public string File { get; set; }
        [Display(Name = "Resume")]
        [Required(ErrorMessage = "Resume required.")]
        public string ResumeId { get; set; }
        [ForeignKey("ResumeId")]
        public Resume Resume { get; set; }
    }
}
