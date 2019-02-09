using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RecruitPNG.Models
{
    public class Job:BaseEntity
    {
        [Display(Name ="Position")]
        [Required(ErrorMessage = "Position title required.")]
        [StringLength(200, ErrorMessage = "Position title must be no longer than 200 characters.")]
        public string Title { get; set; }
        [Display(Name = "Position Description")]
        [Required(ErrorMessage = "Position description required.")]
        public string Description { get; set; }
        [Display(Name = "Employer")]
        [Required(ErrorMessage = "Employer required.")]
        public string CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [Display(Name = "Employer")]
        public Company Company { get; set; }
        //----------insert province & work location
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country required.")]
        public string CountryId { get; set; }
        [ForeignKey("CountryId")]
        [Display(Name = "Country")]
        public Country Country { get; set; }
        //--
        [Display(Name = "Province")]
        [Required(ErrorMessage = "Province required.")]
        public string CityId { get; set; }
        [ForeignKey("CityId")]
        [Display(Name = "Province")]
        public City City { get; set; }
        [Display(Name = "Work Location")]
        [Required(ErrorMessage = "Work Location required.")]
        public string CountyId { get; set; }
        [ForeignKey("CountyId")]
        [Display(Name = "Work Location")]
        public County County { get; set; }
        //------------------end insert-----------
        [Display(Name = "Publsihed Date")]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; } = DateTime.UtcNow;
        [Display(Name = "Expire Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.UtcNow;
        public virtual ICollection<JobApplication> JobApplications { get; set; }//collection of job applications
        [Display(Name = "Approved?")]
        public bool IsApproved { get; set; }
        [Display(Name = "Active ?")]
        public bool IsActive { get; set; }
        [Display(Name = "Featured")]
        public bool IsFeatured { get; set; }
        [Display(Name = "Position Number")]
        public int Position { get; set; }
    }
}
