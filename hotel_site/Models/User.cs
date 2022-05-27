using System;
using System.Collections.Generic;

namespace hotel_site.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int? CurrentRoomId { get; set; }
        public virtual Room CurrentRoom { get; set; }
        public DateTime DateLeave { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public User(int id, string firstName, string lastName, string phoneNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }

        public void BookRoom(Room room, DateTime dateLeave)
        {
            if (CurrentRoom != null)
                throw new Exception("Ошибка. Пользователь уже заселён.");
            if (!room.IsAvailable)
                throw new Exception("Ошибка. Комната занята.");
            CurrentRoom = room;
            CurrentRoomId = room.Id;
            CurrentRoom.Book(this);
            DateLeave = dateLeave;
        }
    }
}
