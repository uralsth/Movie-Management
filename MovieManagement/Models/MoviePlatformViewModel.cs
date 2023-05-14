using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class MoviePlatformViewModel
    {
        public int MoviePlatformMapID { get; set; }
        public int MovieID { get; set; }
        public int PlatformID { get; set; }

        public string PlatformName { get; set; }
    }
}
