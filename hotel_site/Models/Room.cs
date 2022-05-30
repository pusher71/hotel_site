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
        public bool IsAvailable { get; set; }
        public int HotelBuildingId { get; set; }
        public virtual HotelBuilding HotelBuilding { get; set; }
        public int? BookForeignKey { get; set; }
        public virtual Book Book { get; set; }
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
                throw new Exception("Ошибка. Комната уже присвоена отелю.");
            HotelBuilding = hotel;
            HotelBuildingId = hotel.Id;
        }

        public void SetBook(Book book)
        {
            if (Book != null)
                throw new Exception("Ошибка. Бронь уже имеется.");
            if (!IsAvailable)
                throw new Exception("Ошибка. Комната недоступна.");
            Book = book;
            BookForeignKey = book.Id;
        }
    }
}
