using System;

namespace hotel_site.Models
{
    public class Book
    {
        public int Id { get; set; }
        public DateTime MomentStart { get; set; }
        public DateTime MomentEnd { get; set; }
        public int PersonCount { get; set; }
        public float Price { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int? RoomId { get; set; }
        public virtual Room Room { get; set; }

        public Book(int id, DateTime momentStart, DateTime momentEnd, int personCount, float price)
        {
            Id = id;
            MomentStart = momentStart;
            MomentEnd = momentEnd;
            PersonCount = personCount;
            Price = price;
        }

        public void Link(User user, Room room)
        {
            UserId = user.Id;
            UserName = user.FirstName;
            Room = room;
            RoomId = room.Id;
        }

        public bool IsActive()
        {
            return DateTime.Now > MomentStart && DateTime.Now < MomentEnd;
        }
    }
}
