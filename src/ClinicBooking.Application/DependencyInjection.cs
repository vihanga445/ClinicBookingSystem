using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;

namespace ClinicBooking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

       
        services.AddMediatR(configuration => 
            configuration.RegisterServicesFromAssembly(assembly));

        
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}