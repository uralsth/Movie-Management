using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class RoleViewModel
    {
        public int RoleID { get; set; }

        [Required]
        [Display(Name = "Title*")]
        public string RoleName { get; set; }

        [Required]
        [Display(Name = "Description*")]
        public string Description { get; set; }
    }
}
