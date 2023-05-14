using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class PlatformViewModel
    {
        public int PlatformID { get; set; }

        [Required]
        [Display(Name ="Name")]
        public string PlatformName { get; set; }

    }
}
