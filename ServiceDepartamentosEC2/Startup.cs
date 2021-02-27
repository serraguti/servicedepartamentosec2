using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ServiceDepartamentosEC2.Data;
using ServiceDepartamentosEC2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDepartamentosEC2
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
            String cadenapostgres =
                this.Configuration.GetConnectionString("postgresawshospital");
            String cadena = //postgresawshospital
                this.Configuration.GetConnectionString("mysqlawshospital");
            services.AddTransient<IRepositoryDepartamentos, RepositoryDepartamentos>();
            //services.AddDbContextPool<DepartamentosContext>
            //    (options => options.UseMySql(cadena
            //    , ServerVersion.AutoDetect(cadena)));
            services.AddDbContext<DepartamentosContext>(options =>
                options.UseNpgsql(cadenapostgres));

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });

            services.AddControllers();
            services.AddSwaggerGen(
                c =>
                {
                    //VERSION 2 Y VERSION 1
                    c.SwaggerDoc(
                        name: "v1", new OpenApiInfo
                        {
                            Title = "Api Departamentos EC2 MySQL",
                            Version = "v1",
                            Description = "Servicio Api con EC2 y RDS MySQL",
                            Contact = new OpenApiContact
                            {
                                Name = "Paco Garcia Serrano",
                                Email = "paco.garcia.serrano@tajamar365.com",
                                Url = new Uri("https://www.localhost.com/"),
                            }
                        });
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options => options.AllowAnyOrigin());
            app.UseHttpsRedirection();
            app.UseSwagger();
            //UI INDICA DONDE VA A VISUALIZAR EL USUARIO LA DOCUMENTACION
            //GENERADA POR SWAGGER EN NUESTRO SERVIDOR
            app.UseSwaggerUI(
                c =>
                {
                    //DEBEMOS CONFIGURAR LA URL DEL SERVIDOR
                    //PARA LA DOCUMENTACION
                    c.SwaggerEndpoint(
                        url: "/swagger/v1/swagger.json"
                        , name: "Api v1");
                    c.RoutePrefix = "";
                });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
