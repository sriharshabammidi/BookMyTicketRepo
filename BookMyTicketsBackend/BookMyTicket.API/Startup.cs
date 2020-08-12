using AutoMapper;
using BookMyTicket.API.Filters;
using BookMyTicket.BLL;
using BookMyTicket.Core;
using BookMyTicket.Core.AutoMapperProfile;
using BookMyTicket.Core.Logger;
using BookMyTicket.DAL;
using BookMyTicket.DAL.Configurations;
using BookMyTicket.Entities;
using BookMyTicket.Interfaces.Repositories;
using BookMyTicket.Interfaces.Services;
using BookMyTicket.Models.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Text;

namespace BookMyTicket.API
{
    public class Startup
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure

        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(mvc =>
            {
                mvc.Filters.Add(typeof(ExceptionFilter));
            });
            services.AddControllers();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();
                    var result = new
                    {
                        Status = 400,

                        Error = new
                        {
                            Message = "Invalid Input",
                            Exception = string.Join(",", errors),
                        },
                    };

                    return new BadRequestObjectResult(result);
                };
            });

            //services.AddSession();
            services.AddHttpContextAccessor();

            services.AddScoped<EnsureUserLoggedIn>();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            this.ConfigureSettings(services);
            this.ConfigureDBSettings(services);
            this.ConfigureIOC(services);
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

        public void ConfigureSettings(IServiceCollection services)
        {
            services.Configure<DBSettings>(Configuration.GetSection("DBSettings"));
            services.AddSingleton(r => r.GetRequiredService<IOptions<DBSettings>>().Value);

            services.Configure<JwtIssuerOptions>(this.Configuration.GetSection(nameof(JwtIssuerOptions)));
            services.AddSingleton(r => r.GetRequiredService<IOptions<JwtIssuerOptions>>().Value);
        }

        public void ConfigureDBSettings(IServiceCollection services)
        {
            services.AddDbContext<BookMyTicketDBContext>(options => options.UseSqlServer(Configuration.GetSection("DBSettings:ConnectionString").Value));

            services.AddTransient<IBaseRepository<User>, BaseRepository<User>>();
        }

        public void ConfigureIOC(IServiceCollection services)
        {
            services.AddSingleton<ILogger, Logger>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ICinemaRepository, CinemasRepository>();
            services.AddScoped<ICitiesRepository, CitiesRepository>();
            services.AddScoped<IMoviesRepository, MoviesRepository>();
            services.AddScoped<IShowsRepository, ShowsRepository>();
        }

        public void ConfigureAuth(IServiceCollection services)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = this.Configuration["JwtIssuerOptions:Issuer"],

                ValidateAudience = true,
                ValidAudience = this.Configuration["JwtIssuerOptions:Audience"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = this._signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.RequireHttpsMetadata = false;
                configureOptions.ClaimsIssuer = this.Configuration["JwtIssuerOptions:Issuer"];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                options.HttpOnly = HttpOnlyPolicy.None;
            });
        }
    }
}
