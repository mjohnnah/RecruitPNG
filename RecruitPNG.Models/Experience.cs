using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RecruitPNG.Models
{
    public class Experience:BaseEntity
    {
        [Display(Name = "Employer")]
        [StringLength(200, ErrorMessage = "Employer name must be no more than 200 characters long.")]
        [Required(ErrorMessage = "Employer name required.")]
        public string Company { get; set; }
        [Display(Name = "Position")]
        [StringLength(200, ErrorMessage = "Position must must be no longer 200 characters.")]
        [Required(ErrorMessage = "Position required.")]
        public string Position { get; set; }
        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start date required.")]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "Resume")]
        public string ResumeId { get; set; }
        [ForeignKey("ResumeId")]
        [Display(Name = "Resume")]
        public Resume Resume { get; set; }
    }
}
