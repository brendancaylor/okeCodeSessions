using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using College.Api.Middleware;
using IdentityServer4.AccessTokenValidation;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace College.Api
{
    public class Startup
    {
        public ICollegeApiConfirguration _collegeApiConfirguration = new CollegeApiConfirguration();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Configuration.GetSection("CollegeApiConfirguration").Bind(_collegeApiConfirguration);

            services.AddDbContext<CollegeContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("CollegeConnection")));

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

            services.AddCors(options =>
            {
                options.AddPolicy("AllRequests", builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins(
                        _collegeApiConfirguration.SpaSpellingClientBaseUrl,
                        "https://spell-it.co.uk",
                        "https://www.spell-it.co.uk"
                    )
                    .AllowCredentials();
                });
            });

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                                   .AddIdentityServerAuthentication(options =>
                                   {
                                       options.Authority = _collegeApiConfirguration.IdentityBaseUrl;
                                       options.ApiName = "spelling-api";
                                       options.RequireHttpsMetadata = true;
                                   });

            services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));

                //options.OutputFormatters
            });


            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped<ICollegeService, CollegeService>();
            services.AddScoped<ICollegeRepository, CollegeRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IHomeWorkRepository, HomeWorkRepository>();
            services.AddScoped<IStandardListRepository, StandardListRepository>();
            services.Configure<SendGridConfiguration>(this.Configuration.GetSection("SendGridConfiguration"));
            services.Configure<OxfordDictionaryConfiguration>(this.Configuration.GetSection("OxfordDictionaryConfiguration"));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddHttpClient("identityClient", client =>
                {
                    client.BaseAddress = new Uri(_collegeApiConfirguration.IdentityBaseUrl);
                });
            services.AddHttpClient("OxfordDictionaryService", client =>
            {
                var oxfordDictionaryConfiguration = new OxfordDictionaryConfiguration();
                Configuration.GetSection("OxfordDictionaryConfiguration").Bind(oxfordDictionaryConfiguration);
                client.BaseAddress = new Uri(oxfordDictionaryConfiguration.BaseUrl);
            });

            services.AddScoped<IOxfordDictionaryService, OxfordDictionaryService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            else
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseCors("AllRequests");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseIdentityBuilder();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
