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
        public DbSet<Book> Book { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Service> Service { get; set; }

        public DataContext()
        {
            //_ = HotelInfo.ToList();
            //_ = HotelBuilding.ToList();
            //_ = HotelPhoto.ToList();
            //_ = Room.ToList();
            //_ = RoomPhoto.ToList();
            //_ = Comment.ToList();
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //_ = HotelInfo.ToList();
            //_ = HotelBuilding.ToList();
            //_ = HotelPhoto.ToList();
            //_ = Room.ToList();
            //_ = RoomPhoto.ToList();
            //_ = Comment.ToList();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=hotel_site;Username=postgres;Password=1234");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Room)
                .WithOne(i => i.Book)
                .HasForeignKey<Room>(b => b.BookForeignKey);
        }
    }
}
