using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Cartola.Data;
using Cartola.Infra.Repositories;
using Cartola.Domain.Services;
using Cartola.Domain.Services.IRepositories;
using Cartola.Domain.Services.IServices;
using Cartola.Infra.Repositories.Base;
using Cartola.Infra.Repositories.Interfaces;
using Cartola.Infra;

namespace Cartola
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<WeatherForecastService>();
            services.AddScoped<ICartolaRepository, CartolaRepository>();
            services.AddScoped<ICartolaService, CartolaService>();
            services.AddScoped<ICargaCartolaService, CargaCartolaService>();
            services.AddScoped<ICargaCartolaRepository, CargaCartolaRepository>();
            services.AddScoped<CartolaDBContext>();
            
            services.AddSingleton<IHttpClientCartolaApi, HttpClientCartolaApi>();
            
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
