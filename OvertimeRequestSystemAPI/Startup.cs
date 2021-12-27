using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OvertimeRequestSystemAPI.Context;
using OvertimeRequestSystemAPI.JSON;
using OvertimeRequestSystemAPI.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeRequestSystemAPI
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
            services.AddScoped<ParameterRepository>();
            services.AddScoped<AccountRepository>();
            services.AddScoped<AccountRoleRepository>();
            services.AddScoped<EmployeeRepository>();
            services.AddScoped<OvertimeRepository>();
            services.AddScoped<OvertimeDetailRepository>();
            services.AddScoped<ResponseRepository>();
            services.AddScoped<RoleRepository>();
            services.AddDbContext<MyContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("APIContext")));

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero

                };
            });
            
   /*         services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter());
                options.JsonSerializerOptions.Converters.Add(new OvertimeDetaillToStringConverter());

            });*/
            services.AddControllers()
     .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
         options.JsonSerializerOptions.PropertyNamingPolicy = null;
         options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter());
     });

            services.AddRazorPages();
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero

                };
            });
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
            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    await context.Response.WriteAsync("Request Access Denied, Please Input Your Token");
                }
                else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    await context.Response.WriteAsync("Token Validation Has Failed. Request Access Denied");
                }
            });
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
