namespace MovieManagement.Models
{
    public class PeopleItemModel
    {
        public int PeopleID { get; set; }
        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string ImagePath { get; set; }
        public string PeopleName { get; set; }
        public string AddedBy { get; set; }
        public string LastUpdatedBY { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime LastUpdtadeOn { get; set; }
        public string PeopleRoles { get; set; }
    }
}
