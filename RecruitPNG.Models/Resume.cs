using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RecruitPNG.Models
{
    public class Resume:BaseEntity
    {
        [Display(Name = "Resume Name")]
        [Required(ErrorMessage = "Resume name is required.")]
        [StringLength(200, ErrorMessage = "Rsume name should not be more than 200 characters.")]
        public string ResumeName { get; set; }
        [Display(Name="First Name")]
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(100, ErrorMessage = "Fisrt name must noto be more than 100 characters.")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100, ErrorMessage = "Lastname must not be longer than 100 characters.")]
        public string LastName { get; set; }
        [Display(Name = "Photo")]
        [StringLength(200, ErrorMessage = "Photo name must not be more than 200 characters.")]
        public string Photo { get; set; }
        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Gender required.")]
        public Gender Gender { get; set; }
        [Display(Name = "Marital Status")]
        [Required(ErrorMessage = "Marital status required.")]
        public MaritalStatus MaritalStatus { get; set; }
        [Display(Name = "Mobile Phone")]
        [Required(ErrorMessage = "Mobile phone required.")]
        [StringLength(20, ErrorMessage = "Mobile phone number must not be more than 20 characters.")]
        [Phone(ErrorMessage = "Mobile number required.")]
        public string MobilePhone { get; set; } 
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(200, ErrorMessage = "Email must be 200 characters long.")]
        [EmailAddress(ErrorMessage = "Incorrect email address!")]
        public string Email { get; set; } 
        [Display(Name = "Years of Experience")]
        [Required(ErrorMessage = "Years of Experience is required.")]
        [Range(0,50, ErrorMessage = "Years of experience must be less than 50 years.")]
        public int TotalExperience { get; set; }
        public virtual ICollection<Experience> Experiences { get; set; }
        [Display(Name = "Are you are smoker?")]
        public bool UsingCigarette{ get; set; }
        [Display(Name = "Ban on travel?")]
        public bool IsTravelDisabled { get; set; }
        [Display(Name = "Do you have any disability")]
        public bool IsDisabled{ get; set; }
        [Display(Name = "Receive any subsidy?")]
        public bool IsSubsidized { get; set; }
        [Display(Name = "Are you currently employed?")]
        public bool IsCurrentlyWorking { get; set; }
        [Display(Name = "Are looking for employment?")]
        public bool IsSeekingJob { get; set; }
        [Display(Name = "Military Status")]
        public MilitaryStatus? MilitaryStatus { get; set; }
        [Display(Name = "Last Formal Education")]
        [Required(ErrorMessage = "Mezun olduğu okul gereklidir.")]
        [StringLength(200, ErrorMessage = "Mezun olduğu okul en fazla 200 karakter uzunluğunda olabilir.")]
        public string LastEducation { get; set; }
        [Display(Name = "Last Department")]
        [Required(ErrorMessage = "Last department is required.")]
        [StringLength(200, ErrorMessage = "Mezun olduğu bölüm en fazla 200 karakter uzunluğunda olabilir.")]
        public string LastDepartment { get; set; }
        [Display(Name = "Graduation Year")]
        [Range(1920, 2018, ErrorMessage = "Mezuniyet yılı 1920 ila 2018 yılları arasında olabilir.")]
        public int? GraduationYear { get; set; }
        [Display(Name = "Foreign Languages")]
        [StringLength(200, ErrorMessage = "Foreign language filed must be no more than 200 characters.")]
        public string ForeignLanguages { get; set; }
        [Display(Name = "Resume File")]
        public virtual ICollection<ResumeFile> ResumeFiles { get; set; }
        [Display(Name = "Approved?")]
        public bool IsApproved { get; set; }
        [Display(Name = "Active?")]
        public bool IsActive { get; set; }
        [Display(Name = "Job Application")]
        public virtual ICollection<JobApplication> JobApplications { get; set; }
        [Display(Name = "Occupation")]
        [Required(ErrorMessage = "Occupation is required.")]
        public string OccupationId { get; set; }
        [Display(Name = "Occupation")]
        [ForeignKey("OccupationId")]
        public Occupation Occupation { get; set; }
        [Display(Name = "Objective")]
        [StringLength(4000, ErrorMessage = "Objective must be no more than 400 characters.")]
        public string Objective { get; set; }
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country required.")]
        public string CountryId { get; set; }
        [ForeignKey("CountryId")]
        [Display(Name = "Country")]
        public Country Country { get; set; }
        [Display(Name = "Province")]
        [Required(ErrorMessage = "Province is required.")]
        public string CityId { get; set; }
        [ForeignKey("CityId")]
        [Display(Name = "Province")]
        public City City { get; set; }
        //check required----for location
        [Display(Name = "Work Location")]
        [Required(ErrorMessage = "Location is required.")]
        public string CountyId { get; set; }
        [ForeignKey("CountyId")]
        [Display(Name = "Work Location")]
        public County County{ get; set; }
        [Display(Name = "Address")]
        [StringLength(1000, ErrorMessage = "Adress must be no more tah 1000 characters long.")]
        public City Address { get; set; }
        [Display(Name = "Project")]
        public string Projects { get; set; }
        [Display(Name = "Skills")]
        public string Skills { get; set; }
        [Display(Name = "Hobbies")]
        [StringLength(4000, ErrorMessage = "Hobbies must be 4000 characters long.")]
        public string Hobbies { get; set; }
        [Display(Name = "Driving Lisence")]
        [StringLength(20, ErrorMessage = "Driving lisence information is required.")]
        public string DrivingLicense { get; set; }
        [Display(Name = "Courses")]
        public string Courses { get; set; }
        [StringLength(100, ErrorMessage = "Blog en fazla 100 karakter uzunluğunda olabilir.")]
        public string Blog { get; set; }
        [StringLength(100, ErrorMessage = "LinkedIn en fazla 100 karakter uzunluğunda olabilir.")]
        public string LinkedIn { get; set; }
        [StringLength(100, ErrorMessage = "GitHub en fazla 100 karakter uzunluğunda olabilir.")]
        public string GitHub { get; set; }
        [StringLength(100, ErrorMessage = "Dribbble en fazla 100 karakter uzunluğunda olabilir.")]
        public string Dribbble { get; set; }
        [StringLength(100, ErrorMessage = "Behance en fazla 100 karakter uzunluğunda olabilir.")]
        public string Behance { get; set; }
        [StringLength(100, ErrorMessage = "Youtube en fazla 100 karakter uzunluğunda olabilir.")]
        public string Youtube { get; set; }
        [StringLength(100, ErrorMessage = "Instagram en fazla 100 karakter uzunluğunda olabilir.")]
        public string Instagram { get; set; }
        [StringLength(100, ErrorMessage = "Facebook en fazla 100 karakter uzunluğunda olabilir.")]
        public string Facebook { get; set; }
        [StringLength(100, ErrorMessage = "Twitter en fazla 100 karakter uzunluğunda olabilir.")]
        public string Twitter { get; set; }
    }
}
