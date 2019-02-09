using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RecruitPNG.Models
{
    public class County:BaseEntity
    {
        [Display(Name = "Work Location")]
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        //added-------------
        [Display(Name = "Country")]
        [Required]
        public string CountryId { get; set; }
        [ForeignKey("CountryId")]
        [Display(Name = "Country")]
        public Country Country { get; set; }
        [Display(Name = "Province")]
        [Required]
        public string CityId { get; set; }
        [ForeignKey("CityId")]
        [Display(Name = "Province")]
        public City City { get; set; }
        //------------    
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Resume> Resumes { get; set; }
        //added
        public virtual ICollection<Job> Jobs { get; set; }
        
    }
}
