namespace WebApi.Installers
{
    public interface IInstaller
    {
        void InstallServices(WebApplicationBuilder builder);
    }
}
