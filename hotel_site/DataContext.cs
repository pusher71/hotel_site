using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public DbSet<User> User { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<HistoryAction> HistoryAction { get; set; }
        public DbSet<HistoryRecord> HistoryRecord { get; set; }

        public DataContext()
        {
            //_ = Hotel.ToList();
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //_ = Hotel.ToList();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=hotel_site;Username=postgres;Password=1234");
        }
    }
}
