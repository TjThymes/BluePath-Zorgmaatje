using Microsoft.AspNetCore.Identity;

namespace BluePathBackend.Objects
{
    public class RegisterModel
    {
        public string Fullname { get; set; }
        public string Email { get; set; } // wordt UserName
        public string Password { get; set; }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class ApplicationUser : IdentityUser
    {
        public string Fullname { get; set; }
        public string Avatar { get; set; }
    }
}
