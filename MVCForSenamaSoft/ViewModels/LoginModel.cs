using System.ComponentModel.DataAnnotations;

namespace MVCForSenamaSoft.ViewModels
{
    public class LoginModel
    {
        [Required]
        public string Domain { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsRemberMe { get; set; }
    }
}
