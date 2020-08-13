using AutoMapper;
using BookMyTicket.API.Filters;
using BookMyTicket.BLL;
using BookMyTicket.Core;
using BookMyTicket.Core.AutoMapperProfile;
using BookMyTicket.Core.ClientContext;
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
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            ConfigureSettings(services);
            ConfigureDBSettings(services);
            ConfigureIOC(services);
            ConfigureAuth(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Book My Ticket API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.  
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();
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
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book My Ticket API V1");
            });
        }

        public void ConfigureSettings(IServiceCollection services)
        {
            services.Configure<DBSettings>(Configuration.GetSection("DBSettings"));
            services.AddSingleton(r => r.GetRequiredService<IOptions<DBSettings>>().Value);

            services.Configure<JwtIssuerOptions>(Configuration.GetSection(nameof(JwtIssuerOptions)));
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

            services.AddScoped<IClientContext, ClientContext>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ICinemaRepository, CinemasRepository>();
            services.AddScoped<ICinemaSeatingRepository, CinemaSeatingRepository>();
            services.AddScoped<ICitiesRepository, CitiesRepository>();
            services.AddScoped<IMoviesRepository, MoviesRepository>();
            services.AddScoped<IReservationsRepository, ReservationsRepository>();
            services.AddScoped<IShowsRepository, ShowsRepository>();
            services.AddScoped<ITicketsRepository, TicketsRepository>();
        }

        public void ConfigureAuth(IServiceCollection services)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Configuration["JwtIssuerOptions:Issuer"],

                ValidateAudience = true,
                ValidAudience = Configuration["JwtIssuerOptions:Audience"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

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
                configureOptions.ClaimsIssuer = Configuration["JwtIssuerOptions:Issuer"];
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
