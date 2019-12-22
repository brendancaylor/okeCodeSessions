// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer
{
    public class Startup
    {
        public IIdentityApiConfirguration _identityApiConfirguration = new IdentityApiConfirguration();
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IpdConnection")));

            Configuration.GetSection("IdentityApiConfirguration").Bind(_identityApiConfirguration);
            services.Configure<IdentityApiConfirguration>(this.Configuration.GetSection("IdentityApiConfirguration"));

            services.Configure<IIdentityApiConfirguration>(
                Configuration.GetSection("IdentityApiConfirguration")
            );

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", corsBuilder =>
                {
                    corsBuilder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(origin => origin == _identityApiConfirguration.SpaSpellingClientBaseUrl)
                    .AllowCredentials();
                });
            });

            services.AddControllersWithViews();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.Authentication.CookieLifetime = TimeSpan.FromMinutes(15);
            })
            .AddInMemoryIdentityResources(Config.Ids)
            .AddInMemoryApiResources(Config.Apis)
            .AddInMemoryClients(Config.GetClients(_identityApiConfirguration.SpaSpellingClientBaseUrl))
            .AddAspNetIdentity<ApplicationUser>();

            services.AddTransient<IEmailSender, EmailSender>();

            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                builder.AddDeveloperSigningCredential();
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            EnsureDbInitialization(app);
            EnsureUsersFromConfiguration(app);
        }

        void EnsureDbInitialization(IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            MigrateApplicationDb(serviceScope);
        }

        ApplicationDbContext MigrateApplicationDb(IServiceScope scope)
        {
            var applicationDbScope = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            applicationDbScope.Database.Migrate();
            return applicationDbScope;
        }

        void EnsureUsersFromConfiguration(IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            EnsureUsersFromConfigurationAsync(serviceScope).Wait();
        }

        async Task EnsureUsersFromConfigurationAsync(IServiceScope scope)
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            foreach (var user in _identityApiConfirguration.InitialUsers)
            {
                await EnsureUserFromConfiguration(userManager, user);
            }
        }

        async Task EnsureUserFromConfiguration(UserManager<ApplicationUser> userManager, InitialUser user)
        {
            var existingUser = await userManager.FindByEmailAsync(user.EmailAddress);
            if (existingUser != null && !string.IsNullOrWhiteSpace(user.Password))
            {
                var passwordResetToken = await userManager.GeneratePasswordResetTokenAsync(existingUser);
                await userManager.ResetPasswordAsync(existingUser, passwordResetToken, user.Password);
                Console.WriteLine($"User {existingUser.Email} has been given a new password from configuration");
                return;
            }

            var applicationUser = new ApplicationUser
            {
                Id = user.Id.ToLower(),
                Email = user.EmailAddress,
                UserName = user.EmailAddress,
                NormalizedEmail = user.EmailAddress.Normalize(),
                NormalizedUserName = user.EmailAddress.Normalize()
            };

            await userManager.CreateAsync(applicationUser, user.Password);
            Console.WriteLine($"User {user.EmailAddress} has been added from configuration");
        }
    }
}