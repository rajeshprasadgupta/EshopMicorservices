﻿using BuildingBlocks.Exceptions;
using Carter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
namespace Ordering.API
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddCarter();
			services.AddExceptionHandler<CustomExceptionHandler>();
			services.AddHealthChecks().AddSqlServer(configuration.GetConnectionString("Database")!);
			return services;
		}
		public static WebApplication UseApiServices(this WebApplication app)
		{
			app.MapCarter();
			app.UseExceptionHandler(option => { });
			app.UseHealthChecks("/health",
				new HealthCheckOptions
				{ 
					ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
				});
			return app;
		}
	}
}
