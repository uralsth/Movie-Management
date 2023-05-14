using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class MovieViewModel
    {

        public int MovieID { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string MovieName { get; set; }

        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        [Display(Name = "Plot Summary")]
        public string PlotSummary { get; set; }

        //[DataType(DataType.Time)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        public TimeSpan Runtime { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? FormFile { get; set; }

        public string TrailerLink { get; set; }

        [Required]
        public int[]? GenreIDs { get; set; }
        [Required]
        public int[]? ActorIDs { get; set; }
        [Required]
        public int[]? DirectorIDs { get; set; }
        [Required]
        public int[]? ScreenwriterIDs { get; set; }
        [Required]
        public int[]? PlatformIDs { get; set; }

    }
}
