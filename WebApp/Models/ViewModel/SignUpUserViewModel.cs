
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModel
{
    public class SignUpUserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter username")]
        [Remote(action: "UserNameIsExist",controller:"Account")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please Enter Valid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter MobileNo")]
        [Display(Name = "Mobile Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Mobile Number is Not Valid")]
        public long? MobileNo { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter ConfirmPassword")]
        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage =("Confirm Password can't match"))]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
       
        
    }
}
