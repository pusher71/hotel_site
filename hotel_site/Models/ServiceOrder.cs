namespace hotel_site.Models
{
    public class ServiceOrder
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
        public float Price { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        public ServiceOrder(int id, float price)
        {
            Id = id;
            Price = price;
        }

        public void Link(Service service, User user, Room room)
        {
            Service = service;
            ServiceId = service.Id;
            UserId = user.Id;
            UserName = user.FirstName;
            Room = room;
            RoomId = room.Id;
        }
    }
}
