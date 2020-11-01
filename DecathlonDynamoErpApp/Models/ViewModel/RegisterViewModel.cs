using System.ComponentModel.DataAnnotations;

namespace DecathlonDynamoErpApp.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please Enter a User Name")]
        [Display(Name = "User Name")]
        public string UserName { set; get; }

        [EmailAddress(ErrorMessage = "Please Enter a Valid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter a Password")]
        [DataType(DataType.Password)]
        public string Password { set; get; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password Do Not Match")]
        public string ConfirmPassword { get; set; }
    }
}
