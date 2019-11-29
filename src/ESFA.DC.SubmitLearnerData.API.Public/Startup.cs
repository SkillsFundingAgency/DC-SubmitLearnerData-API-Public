using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Reflection;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Modules;
using ESFA.DC.SubmitLearnerData.API.Public.Config;

namespace ESFA.DC.SubmitLearnerData.API.Public
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMemoryCache();

            services.AddApiVersioning(options =>
            {
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(c =>
            c.SwaggerDoc("v1", new Info
            {
                Title = "FIS API Swagger",
                Version = "v1",
                Description = "An ASP.NET Core Web API to check for FIS updates",
                Contact = new Contact
                {
                    Name = "ESFA"
                }
            }));
        
            var containerBuilder = BuildAutoFacContainer(services);

            AutofacContainer = containerBuilder.Build();

            return new AutofacServiceProvider(AutofacContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(
               options =>
               {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                   {
                       options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                   }
               });
        }

        private ContainerBuilder BuildAutoFacContainer(IServiceCollection services)
        {
            var config = Configuration.GetSection("ApplicationConfig").Get<APIConfiguration>();

            var containerBuilder = new ContainerBuilder();

            containerBuilder.Populate(services);
            containerBuilder.RegisterControllers(Assembly.GetExecutingAssembly()); //Register MVC Controllers
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly()); //Register WebApi Controllers

            containerBuilder.RegisterInstance<APIConfiguration>(config).As<IAPIConfiguration>();
            containerBuilder.RegisterModule<APIModule>();
            containerBuilder.RegisterModule(new LoggerModule(config));

            return containerBuilder;
        }
    }
}
