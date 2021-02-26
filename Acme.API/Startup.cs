using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.API.Data;
using Acme.API.Repositories.Interfaces;
using Acme.API.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Acme.API
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
            services.AddCors(c =>
            {
                c.AddPolicy("CorsPolicy", options => options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
            });
            services.AddControllers();
            services.Configure<DeviceDatabaseSettings>(Configuration.GetSection(nameof(DeviceDatabaseSettings)));
            services.AddSingleton<IDeviceDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DeviceDatabaseSettings>>().Value);
            services.AddTransient<IDeviceContext, DeviceContext>();
            services.AddTransient<IDeviceRepository, DeviceRepository>();
            services.AddAutoMapper(typeof(Startup));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
