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
            String cadena = 
                this.Configuration.GetConnectionString("mysqlawshospital");
            services.AddTransient<IRepositoryDepartamentos, RepositoryDepartamentos>();
            services.AddDbContextPool<DepartamentosContext>
                (options => options.UseMySql(cadena
                , ServerVersion.AutoDetect(cadena)));
           

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
                            Description = "Servicio Api con EC2 y RDS MySQL"
                        });
                });


        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Foo {groupName}",
                    Version = groupName,
                    Description = "Foo API",
                    Contact = new OpenApiContact
                    {
                        Name = "Foo Company",
                        Email = string.Empty,
                        Url = new Uri("https://foo.com/"),
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
