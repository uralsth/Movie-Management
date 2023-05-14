using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MovieManagement.Models
{
    public class PeopleViewModel
    {
        public int PeopleID { get; set; }

        [Required]
        [Display(Name = "Name*")]
        public string PeopleName { get; set; }

        [Required]
        [Display(Name = "Gender*")]
        public string Gender { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? FormFile { get; set; }
        [Required]
        public int[]? RoleIDs { get; set; }

        public DateTime DateOfBirth { get ; set; }
    }
}
