using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class TrailerItemModel
    {
        public int TrailerID { get; set; }  
        public string TrailerTitle { get; set;}
        public string TrailerLink { get; set; }
        public DateTime AddedOn { get; set; }
        public string AddedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
