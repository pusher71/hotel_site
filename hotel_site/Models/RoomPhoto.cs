namespace hotel_site.Models
{
    public class RoomPhoto
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        public RoomPhoto(int id, byte[] image)
        {
            Id = id;
            Image = image;
        }

        public void SetRoom(Room room)
        {
            if (Room != null)
                throw new System.Exception("Ошибка. Фотография уже присвоена комнате.");
            Room = room;
            RoomId = room.Id;
        }
    }
}
