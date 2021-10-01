using System;
using api.Data;
using api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Interfaces;
using System.IO;
using Hangfire;
using Hangfire.PostgreSql;

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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Thermopolia API", Version = "V1" });
            });
            services.AddHttpClient<IFoodsService, FoodsService>(c =>
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
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddScoped<IMailService, MailService>();
            services.AddFluentEmail(Configuration.GetValue<string>("SMTP:User"), Configuration.GetValue<string>("SMTP:DisplayName"))
                .AddRazorRenderer(Directory.GetCurrentDirectory())
                .AddRazorRenderer(typeof(Startup))
                .AddSmtpSender(Configuration.GetValue<string>("SMTP:Server"), Configuration.GetValue<int>("SMTP:Port"), Configuration.GetValue<string>("SMTP:User"), Configuration.GetValue<string>("SMTP:Password"));
            services.AddHangfire(config =>
                config.UsePostgreSqlStorage(Configuration.GetConnectionString("DatabaseContext")));
            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Thermopolia API V1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // localhost:<port>/hangire
            app.UseHangfireDashboard();
        }
    }
}
