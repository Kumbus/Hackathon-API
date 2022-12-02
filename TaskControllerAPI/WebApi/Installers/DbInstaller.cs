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

            }).AddEntityFrameworkStores<TaskOrganiserContext>().AddDefaultTokenProviders();
        }
    }
}
