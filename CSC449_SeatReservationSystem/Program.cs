using CSC449_SeatReservationSystem.Entity;
using CSC449_SeatReservationSystem.Interfaces;
using CSC449_SeatReservationSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using static CSC449_SeatReservationSystem.Entity.Movie;
using static CSC449_SeatReservationSystem.Entity.Auditorium;
using static CSC449_SeatReservationSystem.Entity.MovieTheater;
using static CSC449_SeatReservationSystem.Entity.Seat;

namespace CSC449_SeatReservationSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("Default");
            
            builder.Services.AddDbContext<MovieMagicDbContext>(options =>
            options.UseSqlServer(connectionString));

            //Register Repos
            builder.Services.AddScoped<IMovieTheaterRepository, MovieTheaterRepository>();
            builder.Services.AddScoped<IRepository<Auditorium, AuditoriumModel>,AuditoriumRepository>();
            builder.Services.AddScoped<IRepository<Movie, MovieModel>, MovieRepository>();
            builder.Services.AddScoped<IRepository<Seat, SeatModel>,SeatRepository>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
