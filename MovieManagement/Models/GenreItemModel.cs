using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class GenreItemModel
    {
        public int GenreID { get; set; }
        public string GenreName { get; set; }

        public string Description { get; set; }
        public DateTime AddedOn { get; set; }

        public string AddedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }

        public string LastUpdatedBy { get; set; }
    }
}
