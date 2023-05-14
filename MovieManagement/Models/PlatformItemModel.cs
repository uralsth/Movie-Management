using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Models
{
    public class PlatformItemModel
    {
        public int PlatformID { get; set; }
        public string PlatformName { get; set; }
        public string AddedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
    }
}
