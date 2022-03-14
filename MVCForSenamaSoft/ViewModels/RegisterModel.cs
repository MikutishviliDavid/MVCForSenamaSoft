using System.ComponentModel.DataAnnotations;

namespace MVCForSenamaSoft.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "The email address is not specified")]
        public string Email { get; set; } 
        public bool IsRememberMe { get; set; }
    }
}
