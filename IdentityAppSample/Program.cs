using IdentityAppSample.IdentityPolicy;
using IdentityAppSample.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityAppSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString"));
            });
            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
            //builder.Services.ConfigureApplicationCookie(opt =>
            //{
            //    opt.LoginPath = "/Authenticate/Login";
            //}
            //);
            builder.Services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "IdentityAppCookie";
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                opt.SlidingExpiration = true;
            });

            builder.Services.Configure<IdentityOptions>(opt =>
            {
                opt.User.RequireUniqueEmail= true;
                opt.User.AllowedUserNameCharacters = "abcdefghýijklmnoprstuvwxyz";
           
                opt.Password.RequiredLength = 8;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireDigit = true;
            });
            builder.Services.AddTransient<IPasswordValidator<AppUser>,
                CustomPasswordPolicy>();
            builder.Services.AddTransient<IUserValidator<AppUser>,CustomUserNameEmailPolicy>();
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}