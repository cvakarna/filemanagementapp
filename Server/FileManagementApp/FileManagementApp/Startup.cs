using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManagementApp.AppExceptions.Middleware;
using FileManagementApp.Models.Configuration;
using FileManagementApp.Services;
using FileManagementApp.Services.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace FileManagementApp
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
            services.AddCors();//addding cross origin 
            services.AddControllers();

            //configure or binding  app settings model object from config file 
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            //configure blob settings
            var blobSettingsSection = Configuration.GetSection("AzureBlobSettings");
            services.Configure<AzureBlobSettings>(blobSettingsSection);


            //Jwt configuration
            var appSettings = appSettingsSection.Get<AppSettings>();
            var secretkey = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x=> {

                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretkey),
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });

            //DI Configuration for app services
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            services.AddScoped<IFileService, LocalFileSystemService>();
            //blob enable Azure blob if u have storage account and update key in appsettings.json
            //services.AddScoped<IFileService, AzureBlobFileSystemService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureCustomExceptionMiddleware(); //custom exception middleware

            app.UseRouting();
            
            //cors policy
            app.UseCors(x =>
            {
                x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
               
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
