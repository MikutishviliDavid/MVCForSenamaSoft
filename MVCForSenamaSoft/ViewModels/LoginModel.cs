using System.ComponentModel.DataAnnotations;

namespace MVCForSenamaSoft.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "The email address is not specified")]
        public string Domain { get; set; }

        [Required(ErrorMessage = "The user name is not specified")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The password is not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsRemberMe { get; set; }
    }
}
