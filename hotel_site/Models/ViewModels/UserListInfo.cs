using System.Collections.Generic;

namespace hotel_site.Models.ViewModels
{
    public class UserListInfo
    {
        public IEnumerable<User> Users { get; set; }
        public string AdminId { get; set; }
    }
}
