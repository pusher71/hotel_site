using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using hotel_site.Repository;

namespace hotel_site
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<DataContext>(
                options => options.UseNpgsql(Configuration.GetConnectionString("DatabaseConnection")));
            services.AddScoped<HotelInfoDbRepository>();
            services.AddScoped<HotelBuildingDbRepository>();
            services.AddScoped<HotelPhotoDbRepository>();
            services.AddScoped<RoomDbRepository>();
            services.AddScoped<RoomPhotoDbRepository>();
            services.AddScoped<UserDbRepository>();
            services.AddScoped<CommentDbRepository>();
            services.AddScoped<MessageDbRepository>();
            services.AddScoped<ServiceDbRepository>();
            services.AddScoped<HistoryActionDbRepository>();
            services.AddScoped<HistoryRecordDbRepository>();

            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<UserManager<IdentityUser>>();
            services.AddHttpContextAccessor();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
