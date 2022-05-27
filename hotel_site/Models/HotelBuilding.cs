using System;
using System.Collections.Generic;
using System.Linq;
using hotel_site.Models.ViewModels;

namespace hotel_site.Models
{
    public class HotelBuilding
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public virtual ICollection<HotelPhoto> HotelPhotos { get; set; } = new List<HotelPhoto>();
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

        public HotelBuilding(int id, string name, string description, string address, string phoneNumber, string email)
        {
            Id = id;
            Name = name;
            Description = description;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public ICollection<HotelPhoto> GetAllHotelPhotos()
        {
            return HotelPhotos;
        }

        public HotelPhoto GetHotelPhotoById(int hotelPhotoId)
        {
            return HotelPhotos.FirstOrDefault(o => o.Id == hotelPhotoId);
        }

        public void AddHotelPhoto(HotelPhoto hotelPhoto)
        {
            if (HotelPhotos.Contains(hotelPhoto))
                throw new Exception("Ошибка. Данная фотография уже существует.");
            HotelPhotos.Add(hotelPhoto);
        }

        public void DeleteHotelPhotoById(int hotelPhotoId)
        {
            HotelPhotos.Remove(GetHotelPhotoById(hotelPhotoId));
        }
    }
}
