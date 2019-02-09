using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecruitPNG.Models
{
    public enum MilitaryStatus
    {
        [Display(Name = "Note Done")]
        NotDone = 1,
        [Display(Name = "Done")]
        Done = 2,
        [Display(Name = "Posponed")]
        Postponed = 3,
        [Display(Name = "Exempt")]
        Exempt = 4
    }
}
