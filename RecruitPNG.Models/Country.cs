using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecruitPNG.Models
{
    public class Country : BaseEntity
    {
        [Display(Name = "Name")]
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        //adaded
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Resume> Resumes { get; set; }
        //added
        public virtual ICollection<Job> Jobs { get; set; }
    }
}