using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NittietFirstTest.Models;
using NittietFirstTest.Models.View;
using NittietFirstTest.Repositories;
using NittietFirstTest.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NittietFirstTest
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            // TODO Fix this configuration, add insights keys and stuff
            /*if (!_environment.IsEnvironment("LocalMac"))
            {
                services.AddApplicationInsightsTelemetry(Configuration);
            }*/

            // Include interfaces here as well, along with derived class?
            services.AddScoped<UserRepository>();
            services.AddScoped<RefreshTokenRepository>();
            services.AddScoped<EventRepository>();
            services.AddScoped<LocationRepository>();

            services.AddScoped<EncryptionService>();
            services.AddScoped<AccessTokenService>();

            services.AddDbContext<MainContext>();

            // Automatic migration on startup
            // ** Will not run when only published, applies after first request.
            // ** Trying to run it in the postPublish instead
            /*using (var context = new MainContext(_environment))
            {
                context.Database.Migrate();
            }*/

            // Replacement for UseJwtBearerAuthentication
            // Found at https://github.com/aspnet/Home/issues/2007#issuecomment-296252809
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            services.AddJwtBearerAuthentication(config =>
            {
                config.Audience = "https://nittietfirsttest.azurewebsites.net/";
                
            });

            services.AddMvc()
                .AddJsonOptions(config =>
                {
                    config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    config.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    config.SerializerSettings.DateFormatString = "yyyy-MM-dd'T'HH:mm:ss'Z'";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // TODO
            // This may be a bad idea, no clue how much unnecessary stuff this does
            // Would prefer to use dependency injection for this?
            Mapper.Initialize(config =>
            {
                config.CreateMap<User, SignUpUser>().ReverseMap();
                config.CreateMap<User, ViewUser>().ReverseMap();

                config.CreateMap<Event, CreateEvent>().ReverseMap();
                config.CreateMap<Event, ViewEvent>().ReverseMap();

                config.CreateMap<Location, CreateLocation>().ReverseMap();
                config.CreateMap<Location, ViewLocation>().ReverseMap();

                config.CreateMap<RefreshToken, ViewRefreshToken>().ReverseMap();
            });

            if (!env.IsEnvironment("LocalMac"))
            {
                // TODO Add insights
                //app.UseApplicationInsightsRequestTelemetry();
            }

            /*var secretKey = Environment.GetEnvironmentVariable("JWT_SIGNING_KEY");
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));*/

            /*var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = "https://eventappcore.azurewebsites.net/",

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = "https://eventappcore.azurewebsites.net/",

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };*/

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}