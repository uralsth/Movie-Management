using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class MoviePeopleViewModel
    {
        public int MoviePeopleMapID { get; set; }
        public int MovieID { get; set; }

        public int PeopleID { get; set; }

        public string PeopleName { get; set; }
    }

}
