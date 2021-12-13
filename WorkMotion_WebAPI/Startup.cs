﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.AspNetCore.Swagger;
using WorkMotion_WebAPI.BaseModel;
using Microsoft.EntityFrameworkCore;

namespace WorkMotion_WebAPI
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
            services.AddControllers();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Main API v1.0", Version = "v1.0" });
            });

            services.AddDbContext<ASCCContext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("WorkMotionConnection"));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:3000");
                builder.AllowAnyHeader();
                builder.WithExposedHeaders("Token-Expired");
                builder.AllowAnyMethod();
                builder.AllowCredentials();
                builder.Build();
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            bool chkdeploy = string.IsNullOrEmpty(Configuration.GetValue<string>("deploy")) ? false : Convert.ToBoolean(Configuration.GetValue<string>("deploy"));
            if (!chkdeploy)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Versioned API v1.0");
                    c.DocumentTitle = "API Documentation";
                    c.DocExpansion(DocExpansion.List);

                });
            }
            else
            {
                //forUAT
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/API/swagger/v1.0/swagger.json", "Versioned API v1.0");
                    c.DocumentTitle = "API Documentation";
                    c.DocExpansion(DocExpansion.List);
                });

            }
        }
    }
}
