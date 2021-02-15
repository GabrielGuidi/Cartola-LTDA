using Cartola.Domain.Services;
using Cartola.Domain.Services.IRepositories;
using Cartola.Domain.Services.IServices;
using Cartola.Domain.Services.Strategies;
using Cartola.Domain.Services.Strategies.Interfaces;
using Cartola.Infra;
using Cartola.Infra.Repositories;
using Cartola.Infra.Repositories.Base;
using Cartola.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.IO;

namespace Cartola.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICartolaRepository, CartolaRepository>();
            services.AddScoped<ICartolaService, CartolaService>();
            services.AddScoped<ICargaCartolaService, CargaCartolaService>();
            services.AddScoped<ICargaCartolaRepository, CargaCartolaRepository>();
            services.AddScoped<IApostasService, ApostasService>();
            services.AddScoped<IAnalyticsService, AnalyticsService>();

            var strategies = new List<IBestPlayerStrategy>()            
            {
                new ByMathMagicStrategy(),
                new ByPriceStrategy(),
                new ByPriceWithOnlyHomeStrategy(),
                new ByPriceWithNoOpponentsStrategy(),
                new ByPriceWithOnlyHomeAndNoOpponentsStrategy(),
                new ByMeanStrategy(),
                new ByMeanWithOnlyHomeStrategy(),
                new ByMeanWithNoOpponentsStrategy(),
                new ByMeanWithOnlyHomeAndNoOpponentsStrategy()
            };
            services.AddScoped<IList<IBestPlayerStrategy>>((x) => new List<IBestPlayerStrategy>(strategies));
            
            services.AddScoped<CartolaDBContext>();
            services.AddSingleton<IHttpClientCartolaApi, HttpClientCartolaApi>();

            services.AddDbContext<DapperContext>(options =>
                      options.UseSqlServer(
                          Configuration.GetConnectionString("DefaultConnection")));
            //Register dapper in scope    
            services.AddScoped<ICartolaDapperRepository, CartolaDapperRepository>();

            services.AddHttpClient();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cartola.Api", Version = "v1" });

                var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;
                var applicationName = PlatformServices.Default.Application.ApplicationName;
                var xmlDocumentPath = Path.Combine(applicationBasePath, $"{applicationName}.xml");

                if (File.Exists(xmlDocumentPath))
                {
                    c.IncludeXmlComments(xmlDocumentPath);
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cartola.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
