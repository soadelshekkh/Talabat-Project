using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.Api.Errors;
using Talabat.Api.Extensions;
using Talabat.Api.Helpers;
using Talabat.Api.MiddleWares;
using Talabat.Core.Reposatiries;
using Talabat.Repositary;
using Talabat.Repositary.DbContexts;
using Talabat.Repositary.Identity;

namespace Talabat.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        //configuration this the AppSetting
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerService();
            services.AddDbContext<TalabatContext>(options =>
               {
                   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
               });
            services.AddDbContext<AppUserDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });
            services.ServicesAddExtention();
            services.AddIdentityService(Configuration);
            services.AddSingleton<IConnectionMultiplexer>(s => {
                var connection = ConfigurationOptions.Parse( Configuration.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(connection);   
            });
            services.AddCors(
                Options =>
                {
                    Options.AddPolicy("CorsPolicy", option =>
                    { option.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddelWare>();
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.AddSwaggerApp();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
