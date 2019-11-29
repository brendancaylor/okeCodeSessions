using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecuringAngularApps.STS.Data;
using SecuringAngularApps.STS.Models;
using SecuringAngularApps.STS.Quickstart.Account;

namespace SecuringAngularApps.STS
{
    public class Startup
    {
        public IOtherConfirguration _otherConfirguration = new OtherConfirguration();
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Configuration.GetSection("OtherConfirguration").Bind(_otherConfirguration);

            services.Configure<IOtherConfirguration>(
                Configuration.GetSection("OtherConfirguration")
            );

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", corsBuilder =>
                {
                    corsBuilder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(origin => origin == "http://localhost:4200")
                    .AllowCredentials();
                });
            });

            services.AddScoped<IdentityServiceUserManager>();

            services.AddMvc();
            services.AddTransient<IProfileService, CustomProfileService>();


            var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    options.Authentication.CookieLifetime = TimeSpan.FromMinutes(15);
                })
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<CustomProfileService>();


            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCors("CorsPolicy");

            app.UseIdentityServer();

            EnsureDbInitialization(app);
            EnsureUsersFromConfiguration(app);
            app.UseMvcWithDefaultRoute();
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
            foreach (var user in _otherConfirguration.InitialUsers)
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
