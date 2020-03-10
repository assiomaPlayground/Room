using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RoomService.Services;
using RoomService.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Utils
{
    public class ConfigUtils
    {
        private IServiceCollection ServiceCollection { get; set; }
        private IConfiguration Configuration { get; set; }
        public ConfigUtils(IServiceCollection services, IConfiguration configuration)
        {
            ServiceCollection = services;
            Configuration = configuration; 
        }
        public void ConfigureApp()
        {
            ConfigureSettings();
            GenerateSingletons();
            ConfigureJwt(Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>().Secret);
        }
        public void ConfigureSettings()
        {
            //Setup config file class wrapper
            //Mongo cfg
            ServiceCollection.Configure<RoomServiceMongoSettings>(
                Configuration.GetSection(nameof(RoomServiceMongoSettings)));
            //App settings cfg
            ServiceCollection.Configure<AppSettings>(
                Configuration.GetSection(nameof(AppSettings)));
            //Mongo service
            ServiceCollection.AddSingleton<IRoomServiceMongoSettings>(sp =>
                sp.GetRequiredService<IOptions<RoomServiceMongoSettings>>().Value);
            //App Settings
            ServiceCollection.AddSingleton<IAppSettings>(sp =>
                sp.GetRequiredService<IOptions<AppSettings>>().Value);
        }
        /// <summary>
        /// Jwt configuration
        /// </summary>
        public void ConfigureJwt(string secret)
        {
            var key = Encoding.ASCII.GetBytes(secret);
            ServiceCollection.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer   = false,
                    ValidateAudience = false
                };
            });
        }
        public void GenerateSingletons()
        {
            //Services as singleton
            ServiceCollection.AddSingleton<UserService>();
            ServiceCollection.AddSingleton<BuildingService>();
            ServiceCollection.AddSingleton<ReservationService>();
            ServiceCollection.AddSingleton<WorkSpaceService>();
            ServiceCollection.AddSingleton<QRMapService>();
            ServiceCollection.AddSingleton<FavouritesService>();
        }
    }
}
