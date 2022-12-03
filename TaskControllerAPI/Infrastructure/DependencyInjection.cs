using FluentValidation.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Domain.Entities;
using Domain.Validators;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ISlotsRepository, MSSMSlotsRepository>();
            services.AddScoped<IPlannedTasksRepository, MSSMTasksRepository>();
            services.AddScoped<IHexIdRepository, MSSMHexRepository>();
            services.AddScoped<IValidator<PlannedTask>, TaskValidator>();
            return services;
        }
    }
}
