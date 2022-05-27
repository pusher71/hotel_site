using System;

namespace hotel_site.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public long Timestamp { get; set; }
        public string UserId { get; set; }
        //public int? UserId { get; set; }
        //public virtual User User { get; set; }

        public Comment(int id, string text, int rating, long timestamp)
        {
            Id = id;
            Text = text;
            Rating = rating;
            Timestamp = timestamp;
        }

        //public void SetUser(User user)
        //{
        //    if (User != null)
        //        throw new Exception("Ошибка. Пользователь уже определён.");
        //    User = user;
        //    UserId = user.Id;
        //}
    }
}
