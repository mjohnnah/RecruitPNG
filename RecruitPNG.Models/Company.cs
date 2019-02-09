using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RecruitPNG.Models
{
    public class Company:BaseEntity
    {
        [Display(Name = "Company")]
        [StringLength(200, ErrorMessage = "Company name must be no more than 200 characters.")]
        [Required(ErrorMessage = "Compnay name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Company description is required.")]
        [Display(Name = "Company description")]
        public string Description { get; set; }
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Countery is required.")]
        public string CountryId { get; set; }
        [ForeignKey("CountryId")]
        [Display(Name = "Country")]
        public Country Country { get; set; }
        [Display(Name = "Province")]
        [Required(ErrorMessage = "Province is required.")]
        public string CityId { get; set; }
        [ForeignKey("CityId")]
        [Display(Name = "Province")]
        public City City {get; set;}
        [Display(Name = "Job location")]
        [Required(ErrorMessage = "Location is required.")]
        public string CountyId { get; set; }
        [ForeignKey("CountyId")]
        [Display(Name = "Job location")]
        public County County { get; set; }
        [Display(Name = "Address")]
        [StringLength(4000, ErrorMessage = "Address must be no more than 4000 characters.")]
        public string Address { get; set; }
        [Display(Name = "Sector")]
        [Required(ErrorMessage = "Sector is required .")]
        public string SectorId { get; set; }
        [ForeignKey("SectorId")]
        [Display(Name = "Sector")]
        public Sector Sector { get; set; }
        [Display(Name = "Approved?")]
        public bool IsApproved { get; set; }
        [Display(Name = "Active?")]
        public bool IsActive { get; set; }
        public ICollection<Job> Jobs { get; set; }

        [Display(Name="Phone")]
        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(20, ErrorMessage = "Phone number must be 20 character.")]
        [Phone(ErrorMessage = "Phone number is required!")]
        public string Phone { get; set; }
        [Display(Name="Email")]
        [Required(ErrorMessage = "Email address is required.")]
        [StringLength(200, ErrorMessage = "Email address must no more than 200 characters.")]
        [EmailAddress(ErrorMessage = "Email address is required!")]
        public string Email { get; set; }
        [Display(Name="Contact Name")]
        [Required(ErrorMessage = "Contact name is required.")]
        [StringLength(200, ErrorMessage ="Contatc name must be no more than 200 characters.")]
        public string ContactName { get; set; }

        [Display(Name="Logo")]
        [StringLength(200, ErrorMessage = "Logo name must be no longer than 200 characters.")]
        public string Logo { get; set; }
        [Display(Name="Web Site")]
        [StringLength(200, ErrorMessage = "Web side address must be no more than 200 characters.")]
        public string Website { get; set; }

    }
}