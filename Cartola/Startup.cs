using Cartola.Data;
using Cartola.Domain.Services;
using Cartola.Domain.Services.IRepositories;
using Cartola.Domain.Services.IServices;
using Cartola.Infra;
using Cartola.Infra.Repositories;
using Cartola.Infra.Repositories.Base;
using Cartola.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddScoped<IApostasService, ApostasService>();
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
