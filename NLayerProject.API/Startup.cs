using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(Service.Services.Service<>));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //data katman�nda dbcontex i�inde tan�mlad���m�z optionsu burada verece�iz.

            services.AddDbContext<AppDbContext>(optios =>
            {
                //AppDbContext Data katman�nda oldu�u i�in MigrationsAssembly parametresini UseSqlServer optionsta veriyoruz veriyoruz
                optios.UseSqlServer(Configuration["ConnectionsStings:SqlConStr"].ToString(), o =>
                {
                    o.MigrationsAssembly("NLayerProject.Data");
                    //Migrations i�lemi i�in Microsoft.EntityFrameworkCore.Design paketini projeye ekledik
                });
            });


            //AddScoped bir request s�ras�nda bir clas�n constunda IUnitOfWork ile kar��las�rsa gidecek UnitOfWork ten birtane nesne �rne�i al�cak
            //bir request s�ras�nda birden fasla IUnitOfWork ile kar��la��rsa ayn� nesne �rne�i �zerinden devam edecek
            //performans i�in �ok faydal�
            

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
