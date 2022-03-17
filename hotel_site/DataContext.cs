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

        public DataContext()
        {
            _ = HotelInfo.ToList();
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            _ = HotelInfo.ToList();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=hotel_site;Username=postgres;Password=1234");
        }
    }
}
