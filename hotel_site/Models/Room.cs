using System.Collections.Generic;

namespace hotel_site.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public float Square { get; set; }
        public float Price { get; set; }
        public bool Available { get; set; }
        public int? HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<User> Users { get; set; } = new List<User>();

        public void SetHotel(Hotel hotel)
        {
            if (hotel != null)
                throw new System.Exception("Ошибка. Комната уже присвоена отелю.");
            Hotel = hotel;
            HotelId = hotel.Id;
        }

        public void Book(User user)
        {
            if (!Available || Users.Count > 0)
                throw new System.Exception("Ошибка. Комната занята.");
            Users.Add(user);
            Available = false;
        }

        public Room(int id, string number, float square, float price)
        {
            Id = id;
            Number = number;
            Square = square;
            Price = price;
            Available = true;
        }
    }
}
