namespace WebApp.Models
{
    // constructor
    //blue print
    public class HomePageViewModel
    {
         public HomePageViewModel(string title)
        {
            this.Title = title;
        }
        public string Title { get; private set; }
       public string CurrentUser { get; set; }
        

    }
}
