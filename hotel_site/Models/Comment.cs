using System;

namespace hotel_site.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; } //на случай пропажи User из базы данных

        public Comment(int id, string text, int rating, DateTime timestamp)
        {
            Id = id;
            Text = text;
            Rating = rating;
            Timestamp = timestamp;
        }

        public void SetUser(User user)
        {
            if (UserId != null)
                throw new Exception("Ошибка. Пользователь уже определён.");
            UserId = user.Id;
            UserName = user.FirstName;
        }
    }
}
