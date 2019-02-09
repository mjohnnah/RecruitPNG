using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecruitPNG.Models
{
    public class Message:BaseEntity
    {
        [Display(Name = "From:")]
        [Required(ErrorMessage = "From is required.")]
        [StringLength(200)]
        public string From { get; set; } // companyId or resumeId
        [Display(Name = "From:")]
        [Required(ErrorMessage = "From name is required.")]
        [StringLength(200)]
        public string FromName { get; set; }
        [Display(Name = "To:")]
        [Required(ErrorMessage = "Message to is required.")]
        [StringLength(200)]
        public string To { get; set; } // companyId or resumeId
        [Display(Name = "To:")]
        [Required(ErrorMessage = "To name is required.")]
        [StringLength(200)]
        public string ToName { get; set; }
        [Display(Name = "Message Title:")]
        [Required(ErrorMessage = "Message title is required.")]
        [StringLength(200, ErrorMessage = "Message title must be no more than 200 characters long.")]
        public string Title { get; set; }
        [Display(Name = "Message Body:")]
        [Required(ErrorMessage = "Message body is required.")]
        public string Description { get; set; }
        [Display(Name = "Display Name ?")]
        public bool IsRead { get; set; }
        [Display(Name = "Approved ?")]
        public bool IsApproved { get; set; }
    }
}
