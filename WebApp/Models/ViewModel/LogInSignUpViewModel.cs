using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModel
{
    public class LogInSignUpViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me?")]
        public bool IsRemember { get; set; }
    }
}
