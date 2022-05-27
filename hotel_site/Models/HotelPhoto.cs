namespace hotel_site.Models
{
    public class HotelPhoto
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public int? HotelBuildingId { get; set; }
        public virtual HotelBuilding HotelBuilding { get; set; }

        public HotelPhoto(int id, byte[] image)
        {
            Id = id;
            Image = image;
        }

        public void SetHotel(HotelBuilding hotel)
        {
            if (HotelBuilding != null)
                throw new System.Exception("Ошибка. Фотография уже присвоена отелю.");
            HotelBuilding = hotel;
            HotelBuildingId = hotel.Id;
        }
    }
}
