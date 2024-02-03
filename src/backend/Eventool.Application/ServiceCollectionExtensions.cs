using Eventool.Application.Utility;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Eventool.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
        => services
            .AddMediatr()
            .AddValidation()
            .AddUtility();

    private static IServiceCollection AddMediatr(this IServiceCollection services)
        => services
            .AddMediatR(c => c
                .RegisterServicesFromAssemblyContaining(typeof(ServiceCollectionExtensions)));

    private static IServiceCollection AddValidation(this IServiceCollection services)
        => services.AddValidatorsFromAssemblyContaining(typeof(ServiceCollectionExtensions));

    private static IServiceCollection AddUtility(this IServiceCollection services)
        => services.AddTransient<IHasher, Sha512Hasher>();
}