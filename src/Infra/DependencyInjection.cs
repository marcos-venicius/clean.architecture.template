using System.Reflection;
using Application.Common.Behaviours;
using Application.Common.Interfaces;
using FluentValidation;
using Infra.Context;
using MediatR;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services)
    {
        services.AddMemoryCache();

        services.AddDbContext<EFContext>();

        services.AddScoped<IEFContext>(provider => provider.GetRequiredService<EFContext>());

        services.AddHttpContextAccessor();

        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);

        // configure fluent validator (validations)
        services.AddValidatorsFromAssembly(assembly);

        // configure mediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assembly);

            // [ATTENTION]: be careful with the configuration order. the first one added runs first, the last one runs last, and so on.

            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });

        // configuring api versioning
        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = new UrlSegmentApiVersionReader(); // allow versioning via url: /api/v1, /api/v2
        });

        // swagger versioning explorer 
        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        // health check
        services.AddHealthChecks();

        using var scope = services.BuildServiceProvider().CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<EFContext>();

        if (dbContext.Database.GetPendingMigrations().Any())
            dbContext.Database.Migrate();

        return services;
    }
}