using System;
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
        public int MaxPersonCount { get; set; }
        public bool IsAvailable { get; set; }
        public int HotelBuildingId { get; set; }
        public virtual HotelBuilding HotelBuilding { get; set; }
        public virtual ICollection<RoomPhoto> RoomPhotos { get; set; } = new List<RoomPhoto>();
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();

        public Room(int id, string number, string floor, float square, float price, int maxPersonCount, bool isAvailable)
        {
            Id = id;
            Number = number;
            Floor = floor;
            Square = square;
            Price = price;
            MaxPersonCount = maxPersonCount;
            IsAvailable = isAvailable;
        }

        public void SetHotel(HotelBuilding hotel)
        {
            if (HotelBuilding != null)
                throw new Exception("Ошибка. Комната уже присвоена отелю.");
            HotelBuilding = hotel;
            HotelBuildingId = hotel.Id;
        }
    }
}
