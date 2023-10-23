using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Talabat.Api.Errors;
using Talabat.Api.Helpers;
using Talabat.Core.Reposatiries;
using Talabat.Core.services;
using Talabat.Repositary;
using Talabat_Service;
using Talabat_Service.Order;

namespace Talabat.Api.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection ServicesAddExtention(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepositary<>), typeof(GenericRepositary<>));
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();   
            services.AddScoped<Ipayment, PaymentService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();  
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var error = ActionContext.ModelState.Where(E => E.Value.Errors.Count() > 0)
                                             .SelectMany(E => E.Value.Errors).Select(E => E.ErrorMessage).ToArray();
                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = error
                    };
                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });
            services.AddScoped(typeof(IBasketRepositary), typeof(BsketRepositary));
            return services;    
        }
    }
}
