using EmailService;

namespace WebApi.Installers
{
    public class MailInstaller : IInstaller
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            builder.Services.AddSingleton(emailConfig);
            builder.Services.AddScoped<IEmailSender, EmailSender>();
        }
    }
}
