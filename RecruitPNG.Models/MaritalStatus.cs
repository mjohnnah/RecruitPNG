using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecruitPNG.Models
{
    public enum MaritalStatus
    {

        [Display(Name = "Single")]
        Single = 1,
        [Display(Name = "Married")]
        Married = 2
    }
}
