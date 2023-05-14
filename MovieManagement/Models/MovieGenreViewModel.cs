using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class MovieGenreViewModel
    {
        public int MovieGenreMapID { get; set; }
        public int MovieID { get; set; }
        public int GenreID { get; set; }

        public string GenreName { get; set;}
    }
}
