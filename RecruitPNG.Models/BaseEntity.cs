using System;
using System.ComponentModel.DataAnnotations;

namespace RecruitPNG.Models
{
    public class BaseEntity
    {
        public string Id { get; set; }  //as Guid
        [Display(Name = "Date created")]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Created by")]
        [StringLength(200)]
        public string CreatedBy { get; set; }
        [Display(Name = "Date updated")]
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        [Display(Name = "Updated by")]
        [StringLength(200)]
        public string UpdatedBy { get; set; }
        [Display(Name = "Ip Address")]
        [StringLength(200)]
        public string IPAddress { get; set; }
    }
}
