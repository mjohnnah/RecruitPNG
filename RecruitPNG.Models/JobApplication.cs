using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RecruitPNG.Models
{
    public class JobApplication:BaseEntity
    {
        
        
        [Display(Name = "Application Title")]
        [StringLength(200, ErrorMessage = "Job Description can not be more than 200 chraracters long.")]
        public string Title { get; set; }

        [Display(Name = "Application Letter")]
        public string Description { get; set; }

        //relation to job
        [Display(Name = "Job Titel")]
        [Required(ErrorMessage = "Job title required.")]
        public string JobId { get; set; }
        [ForeignKey("JobId")]
        [Display(Name = "Job Title")]
        public Job Job { get; set; }

        //relation to resume
        [Display(Name = "Attach your resume")]
        [Required(ErrorMessage = "Resume required.")]
        public string ResumeId { get; set; }
        [ForeignKey("ResumeId")]
        [Display(Name = "Resume")]
        public Resume Resume { get; set; }    
    }
}
