﻿namespace hotel_site.Models
{
    public class HotelInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public HotelInfo(int id, string name, string description, string address, string phoneNumber, string email)
        {
            Id = id;
            Name = name;
            Description = description;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}
