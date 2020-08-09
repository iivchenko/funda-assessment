using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using SaleStatistics.Application.Repositories.SaleStatistics;
using SaleStatistics.Application.Services.Sales;
using SaleStatistics.Infrastructure.Repositories;
using SaleStatistics.Infrastructure.Services.Sales;
using SaleStatistics.Web.HostedServices;
using System;

namespace SaleStatistics.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<FundaSettings>(Configuration.GetSection(FundaSettings.Section));

            services.AddHttpClient();

            services.AddScoped<ISaleStatisticRepository, RedisSaleStatisticRepository>(x => new RedisSaleStatisticRepository(Configuration.GetConnectionString("Redis")));

            services.AddSingleton<IFundaClientFactory, FundaClientFactory>();
            services.AddScoped<ISaleService, FundaSaleService>();

            services.AddHostedService<ScheduledStatisticUpdateHostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=SaleStatistics}/{action=Index}/{id?}");
            });
        }
    }
}
