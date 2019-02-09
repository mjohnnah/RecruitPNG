using RecruitPNG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitPNG.Web.ViewModels
{
    public class ApplyViewModel
    {       

        //public JobApplication application  { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
       public string Applicant { get; set; }
       public Job Job { get; set; }
       public Resume Resume { get; set; }

    }
}
