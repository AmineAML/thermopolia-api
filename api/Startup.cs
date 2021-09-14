using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

namespace api
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "api", Version = "v1" });
            });
            services.AddHttpClient<IRecipesService, RecipesService>(c =>
            {
                c.BaseAddress = new Uri($"https://api.edamam.com/api/recipes/");

                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            services.AddHttpClient<IDrinksService, DrinksService>(c =>
            {
                c.BaseAddress = new Uri($"https://api.edamam.com/api/recipes/");

                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            services.AddScoped<IDietService, DietService>();
            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DatabaseContext"))
                                                        .UseSnakeCaseNamingConvention()
                                                        .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                                                        .EnableSensitiveDataLogging()
            );
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddScoped<ICacheService, CacheService>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = $"{Configuration.GetValue<string>("Redis:Server")}:{Configuration.GetValue<int>("Redis:Port")}";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1"));
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
