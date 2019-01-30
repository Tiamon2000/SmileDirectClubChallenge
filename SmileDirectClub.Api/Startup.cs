using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmileDirectClub.Api.Extensions;
using SmileDirectClub.Business;
using SmileDirectClub.Services;
using SmileDirectClub.Shared.Contracts;
using SmileDirectClub.Shared.Settings;

namespace SmileDirectClub.Api
{
	public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			// framework services
			services.AddAutoMapper();
            services.AddMvc();

			// logging - logging will just be to the output console for this demo
			services.AddLogging(loggingBuilder =>
			{
				loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
				loggingBuilder.AddConsole();
				loggingBuilder.AddDebug();
			});

			// configuration
			services.AddOptions();
			services.Configure<SpaceXApiSettings>(Configuration.GetSection("SpaceXApiSettings"));

			// dependency injection
			services.AddSingleton<ILaunchPadManager, LaunchPadManager>();
			services.AddSingleton<ILaunchPadService, LaunchPadService>();

			/* NOTE REGARDING FUTURE PLANS TO REPLACE THE SPACEX REST API CLIENT WITH A DATABASE
			 * 
			 * ILaunchPadService is defined SmileDIrectClub.Shared
			 *		Its methods return domain models
			 * The current REST api implementation is defined in SmileDirectClub.Services
			 *		That project also defines the api related settings and the api data transfer object (dto) models
			 *		Internally converts from dto to domain models
			 * To replace the REST api with a database
			 *		Simply implement an ILaunchPadService that reads from a database
			 *		Remap the depedency injection line above to reference the new implementation
			 * If the REST api is going to be fully removed,
			 *		Remove the SmileDirectClub.Services project from the solution
			 *		Remove the SpaceXApiSettings section from appsettings.json
			 */
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.ConfigureExceptionHandling(loggerFactory.CreateLogger<Startup>());
			app.UseMvc();
        }
    }
}
