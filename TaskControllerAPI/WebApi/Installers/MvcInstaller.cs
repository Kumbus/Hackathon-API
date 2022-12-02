using Application;
using Infrastructure;

namespace WebApi.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddInfrastructure();
            builder.Services.AddApplication();
        }
    }
}
