using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Middlewares;

namespace WebApi.Installers
{
    public class MiddlewaresInstaller : IInstaller
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ExceptionHandlingMiddleware>();
            builder.Services.AddCors();
        }
    }
}
