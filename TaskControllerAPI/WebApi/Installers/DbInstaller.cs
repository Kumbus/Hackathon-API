using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<TaskOrganiserContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TaskOrganiserCS")));
        }
    }
}
