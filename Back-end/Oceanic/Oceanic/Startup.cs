using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Oceanic.Infrastructure;
using Oceanic.Infrastructure.Interfaces;
using Oceanic.Core;
using Oceanic.Infrastructure.Repository;
using Oceanic.Services.Interface;
using Oceanic.Services.Service;


namespace Oceanic
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
            services.AddDbContext<OceanicDataContext>(options => options.UseSqlServer(@"Data Source=PF0Y3EMA\SQLEXPRESS;Initial Catalog=OCEANIC;User ID=sa;Password=P@ssword1;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
            services.AddScoped<IRepositoryAsync<City>, Repository<City>>()
                .AddScoped<IUnitOfWorkAsync, UnitOfWork>()
                .AddScoped<IRepositoryAsync<GoodsType>, Repository<GoodsType>>()
                .AddScoped<IRepositoryAsync<ExtraFee>, Repository<ExtraFee>>()
                .AddScoped<IRepositoryAsync<Order>, Repository<Order>>()
                .AddScoped<IRepositoryAsync<Price>, Repository<Price>>()
                .AddScoped<IRepositoryAsync<Route>, Repository<Route>>()
                .AddScoped<IRepositoryAsync<Size>, Repository<Size>>()
                .AddScoped<IRepositoryAsync<TransportType>, Repository<TransportType>>()
                .AddScoped<IDataContextAsync, OceanicDataContext>();


            // services.AddAutoMapper();
            services.AddMvc().AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddMvc();

            services
                .AddTransient<ISearchService, SearchService>()
                .AddTransient<IAdminService, AdminService>(); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{id?}",
                    defaults: new { controller = "Search" });
            });
        }
    }
}
