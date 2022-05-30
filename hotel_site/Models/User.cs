using Microsoft.AspNetCore.Identity;

namespace hotel_site.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User()
        {
        }

        public User(string userName, string firstName, string lastName) : base(userName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
