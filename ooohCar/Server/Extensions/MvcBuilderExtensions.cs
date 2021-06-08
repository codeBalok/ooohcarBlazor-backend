using ooohCar.Application.Configurations;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace ooohCar.Server.Extensions
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddValidators(this IMvcBuilder builder)
        {
            builder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AppConfiguration>());
            return builder;
        }
    }
}