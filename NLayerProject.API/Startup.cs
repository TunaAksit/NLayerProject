using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLayerProject.API.DTOs;
using NLayerProject.API.Filters;
using NLayerProject.Core.Repositories;
using NLayerProject.Core.Services;
using NLayerProject.Core.UnitOfWorks;
using NLayerProject.Data;
using NLayerProject.Data.Repositories;
using NLayerProject.Data.UnitOfWorks;
using NLayerProject.Service.Services;

namespace NLayerProject.API
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

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ProductNotFoundFilter>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(Service.Services.Service<>));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //data katmanýnda dbcontex içinde tanýmladýðýmýz optionsu burada vereceðiz.

            services.AddDbContext<AppDbContext>(optios =>
            {
                //AppDbContext Data katmanýnda olduðu için MigrationsAssembly parametresini UseSqlServer optionsta veriyoruz veriyoruz
                optios.UseSqlServer(Configuration["ConnectionsStings:SqlConStr"].ToString(), o =>
                {
                    o.MigrationsAssembly("NLayerProject.Data");
                    //Migrations iþlemi için Microsoft.EntityFrameworkCore.Design paketini projeye ekledik
                });
            });


            //AddScoped bir request sýrasýnda bir clasýn constunda IUnitOfWork ile karþýlasýrsa gidecek UnitOfWork ten birtane nesne örneði alýcak
            //bir request sýrasýnda birden fasla IUnitOfWork ile karþýlaþýrsa ayný nesne örneði üzerinden devam edecek
            //performans için çok faydalý
            

            services.AddControllers(o=>
            {
                o.Filters.Add(new ValidationFilter());
            });
            //filterlara karýþma ben yöneteceðim.
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //hatalarý global olarak tutmak
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var error = context.Features.Get<IExceptionHandlerFeature>();
                if(error!=null)
                    {
                        var ex = error.Error;

                        ErrorDto errorDto = new ErrorDto();
                        errorDto.Status = 500;
                        errorDto.Errors.Add(ex.Message);
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorDto));

                    }
                
                
                });
            });

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
