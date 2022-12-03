using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<TaskOrganiserContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TaskOrganiserCS")));

            builder.Services.AddIdentity<User, IdentityRole>( options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 0;
                options.User.RequireUniqueEmail = false;
                options.Lockout.AllowedForNewUsers = true;
            }).AddEntityFrameworkStores<TaskOrganiserContext>().AddDefaultTokenProviders();
        }
    }
}
