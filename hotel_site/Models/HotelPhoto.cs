namespace hotel_site.Models
{
    public class HotelPhoto
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public int? HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }

        public void SetHotel(Hotel hotel)
        {
            if (hotel != null)
                throw new System.Exception("Ошибка. Фотография уже присвоена отелю.");
            Hotel = hotel;
            HotelId = hotel.Id;
        }

        public HotelPhoto(int id, byte[] image)
        {
            Id = id;
            Image = image;
        }
    }
}
