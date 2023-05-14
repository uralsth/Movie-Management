using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class TrailerViewModel
    {
        public int TrailerID { get; set; }
        [Required]
        [Display(Name = "Link*")]
        public string TrailerLink { get; set; }

        [Required]
        [Display(Name ="Title*")]
        public string TrailerTitle { get; set; }
    }
}
