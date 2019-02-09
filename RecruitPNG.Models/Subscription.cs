using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecruitPNG.Models
{
    public class Subscription:BaseEntity
    {
        [Display(Name= "Subscription")]
        [StringLength(200)]
        public string Name{ get; set; }
        [Display(Name="Email")]
        [StringLength(200)]
        public string Email { get; set; }
        public bool IsSubscribed { get; set; }
    }
}
