using System.ComponentModel.DataAnnotations;

namespace MVCForSenamaSoft.ViewModels
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; } 
        public bool IsRememberMe { get; set; }
    }
}
