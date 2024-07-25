using BuisnessLogicLayer.Repositary;
using BuisnessLogicLayer.RepositaryInterfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersentationLayer.Mapping_Profiles;
using System.Reflection;

namespace PersentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DataContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //builder.Services.AddAutoMapper(m => m.AddProfile(new EmployeeProfile()));
            //builder.Services.AddAutoMapper(m => m.AddProfile(new DepartmentProfile()));
            //builder.Services.AddAutoMapper(m => m.AddProfile(new UsersProfile()));
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            builder.Services.AddIdentity<AppUser , IdentityRole>().AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();
            
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}