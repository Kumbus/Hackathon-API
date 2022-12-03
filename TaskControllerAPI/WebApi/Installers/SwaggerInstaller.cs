using Microsoft.OpenApi.Models;

namespace WebApi.Installers
{
    public class SwaggerInstaller : IInstaller
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen();
            builder.Services.AddEndpointsApiExplorer();
        }
    }
}
