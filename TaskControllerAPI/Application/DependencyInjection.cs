using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton(AutoMapperConfig.Initialize());
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ISlotsService, SlotsService>();
            services.AddScoped<IPlannedTasksService, TasksService>();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
