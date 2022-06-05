using System.Linq;
using Microsoft.EntityFrameworkCore;
using hotel_site.Models;

namespace hotel_site
{
    public class DataContext : DbContext
    {
        public DbSet<HotelInfo> HotelInfo { get; set; }
        public DbSet<HotelBuilding> HotelBuilding { get; set; }
        public DbSet<HotelPhoto> HotelPhoto { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<RoomPhoto> RoomPhoto { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<ServiceOrder> ServiceOrder { get; set; }

        public DataContext()
        {
            _ = HotelInfo.ToList();
            _ = HotelBuilding.ToList();
            _ = HotelPhoto.ToList();
            _ = Room.ToList();
            _ = RoomPhoto.ToList();
            _ = Book.ToList();
            _ = Comment.ToList();
            _ = Message.ToList();
            _ = Service.ToList();
            _ = ServiceOrder.ToList();
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            _ = HotelInfo.ToList();
            _ = HotelBuilding.ToList();
            _ = HotelPhoto.ToList();
            _ = Room.ToList();
            _ = RoomPhoto.ToList();
            _ = Book.ToList();
            _ = Comment.ToList();
            _ = Message.ToList();
            _ = Service.ToList();
            _ = ServiceOrder.ToList();
        }
    }
}
