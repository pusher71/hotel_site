using System;

namespace hotel_site.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserFromId { get; set; }
        public string UserFromName { get; set; } //на случай пропажи UserFrom из базы данных
        public string UserToId { get; set; }
        public string UserToName { get; set; } //на случай пропажи UserTo из базы данных

        public Message(int id, string text, DateTime timestamp)
        {
            Id = id;
            Text = text;
            Timestamp = timestamp;
        }

        public void SetUsers(User userFrom, User userTo)
        {
            if (UserFromId != null || UserToId != null)
                throw new Exception("Ошибка. Пользователи уже определены.");
            UserFromId = userFrom.Id;
            UserToId = userTo.Id;
            UserFromName = userFrom.FirstName;
            UserToName = userTo.FirstName;
        }
    }
}
