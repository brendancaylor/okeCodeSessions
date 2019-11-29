using System;
using System.Linq;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NSwag;
using NSwag.Generation.Processors.Security;
using SecuringAngularApps.API.Middleware;
using SecuringAngularApps.API.Model;

namespace SecuringAngularApps.API
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

            services.AddSwaggerDocument(document =>
            {
                document.SerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    }
                };
                document.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
                string[] scopes = { "Authorization" };
                document.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT Token", scopes,
                    new OpenApiSecurityScheme
                    {
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        Description = "Copy 'Bearer ' + valid JWT token into field",
                        In = OpenApiSecurityApiKeyLocation.Header
                    }));
            });

            services.AddDbContext<ProjectDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ProjectDbContext")));
            services.AddCors(options =>
            {
                options.AddPolicy("AllRequests", builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(origin => origin == "http://localhost:4200")
                    .AllowCredentials();
                });
            });

            //services.AddAuthentication("Bearer")
            //    .AddJwtBearer("Bearer", options =>
            //    {
            //        options.Authority = "http://localhost:4242";
            //        options.Audience = "projects-api";
            //        options.RequireHttpsMetadata = false;
            //    });
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                       .AddIdentityServerAuthentication(options =>
                       {
                           options.Authority = "http://localhost:4242";
                           options.ApiName = "projects-api";
                           options.RequireHttpsMetadata = false;
                       });
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseOpenApi();
                app.UseSwaggerUi3();

            }
            app.UseCors("AllRequests");
            app.UseAuthentication();
            app.UseIdentityBuilder();
            EnsureDbInitialization(app);
            app.UseMvc();
        }

        void EnsureDbInitialization(IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            MigrateApplicationDb(serviceScope);
        }

        ProjectDbContext MigrateApplicationDb(IServiceScope scope)
        {
            var applicationDbScope = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();
            applicationDbScope.Database.Migrate();
            return applicationDbScope;
        }
    }
}
