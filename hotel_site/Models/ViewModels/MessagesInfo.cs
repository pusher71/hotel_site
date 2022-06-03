using System.Collections.Generic;

namespace hotel_site.Models.ViewModels
{
    public class MessagesInfo
    {
        public IEnumerable<Message> Messages { get; set; }
        public string UserFromId { get; set; }
        public string UserToId { get; set; }
    }
}
