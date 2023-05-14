using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MovieManagement.Models
{
    public class GenreViewModel
    {
        public int GenreID { get; set; }
       
        [Required]
        [Display(Name = "Name*")]
        public string GenreName { get; set; }

        [Required]
        [Display(Name = "Description*")]
        public string Description { get; set; }
    }
}
