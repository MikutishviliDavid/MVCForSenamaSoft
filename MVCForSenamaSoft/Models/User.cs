using Microsoft.AspNetCore.Identity;

namespace MVCForSenamaSoft.Models
{
    public class User 
    {
        public int Id { get; set; }
        public string Domain { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
