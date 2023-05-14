using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class MovieItemModel
    {
        public int MovieID { get; set; }
        public string MovieName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Language { get; set; }
        public string PlotSummary { get; set; }

        public TimeSpan Runtime { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public string AddedBy { get; set; }
        public string? ImagePath { get; set; }

        public string TrailerLink { get; set; }
        public string?[] MovieGenres { get; set; }
        public string?[] MovieDirectors { get; set; }
        public string?[] MovieActors { get; set; }
        public string?[] MovieScreenwriters { get; set; }
        public string?[] MoviePlatforms { get; set; }
    }
}
