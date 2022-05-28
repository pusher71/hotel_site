namespace hotel_site.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public long Timestamp { get; set; }
        public int? UserFromId { get; set; }
        public virtual User UserFrom { get; set; }
        public string UserFromName { get; set; } //на случай пропажи UserFrom из базы данных
        public int? UserToId { get; set; }
        public virtual User UserTo { get; set; }
        public string UserToName { get; set; } //на случай пропажи UserTo из базы данных

        public Message(int id, string text, long timestamp)
        {
            Id = id;
            Text = text;
            Timestamp = timestamp;
        }

        public void SetUsers(User userFrom, User userTo)
        {
            if (UserFrom != null || UserTo != null)
                throw new System.Exception("Ошибка. Пользователи уже определены.");
            UserFrom = userFrom;
            UserFromId = userFrom.Id;
            UserTo = userTo;
            UserToId = userTo.Id;
        }
    }
}
