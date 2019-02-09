using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RecruitPNG.Models
{
    public class City:BaseEntity
    {
        [Display(Name="Province")]
        [StringLength(200)]
        [Required(ErrorMessage = "Province.")]
        public string Name { get; set; }
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country required")]
        public string CountryId { get; set; }
        [ForeignKey("CountryId")]
        [Display(Name = "Country")]
        public Country Country { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Resume> Resumes { get; set; }
        //added
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<County> Counties { get; set; }

    }
}
