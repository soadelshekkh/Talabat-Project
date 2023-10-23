using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Talabat.Api.Extensions
{
    public static class SwaggerServicesExtension
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Talabat.Api", Version = "v1" });
            });
            return services;
        }
        public static IApplicationBuilder AddSwaggerApp (this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Talabat.Api v1"));
            return app;
        }
    }
}
