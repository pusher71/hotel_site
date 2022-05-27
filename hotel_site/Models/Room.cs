using System.Collections.Generic;

namespace hotel_site.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Floor { get; set; }
        public float Square { get; set; }
        public float Price { get; set; }
        public bool IsAvailable { get; set; }
        public int? HotelBuildingId { get; set; }
        public virtual HotelBuilding HotelBuilding { get; set; }
        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<RoomPhoto> RoomPhotos { get; set; } = new List<RoomPhoto>();

        public Room(int id, string number, string floor, float square, float price, bool isAvailable)
        {
            Id = id;
            Number = number;
            Floor = floor;
            Square = square;
            Price = price;
            IsAvailable = isAvailable;
        }

        public void SetHotel(HotelBuilding hotel)
        {
            if (HotelBuilding != null)
                throw new System.Exception("Ошибка. Комната уже присвоена отелю.");
            HotelBuilding = hotel;
            HotelBuildingId = hotel.Id;
        }

        public void Book(User user)
        {
            if (!IsAvailable || Users.Count > 0)
                throw new System.Exception("Ошибка. Комната занята.");
            Users.Add(user);
            IsAvailable = false;
        }
    }
}
