﻿namespace hotel_site.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public long Timestamp { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }

        public void SetUser(User user)
        {
            if (User != null)
                throw new System.Exception("Ошибка. Пользователь уже определён.");
            User = user;
            UserId = user.Id;
        }

        public Comment(int id, string text, int rating, long timestamp)
        {
            Id = id;
            Text = text;
            Rating = rating;
            Timestamp = timestamp;
        }
    }
}
