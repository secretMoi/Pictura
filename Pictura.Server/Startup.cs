using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pictura.Server.Helpers.Pictures;
using Pictura.Server.Services.Data;
using Pictura.Server.Services.Data.Picture;

namespace Pictura.Server
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
			services.AddDbContext<Context>(opt => opt.UseLazyLoadingProxies()
				.UseSqlServer(
					Configuration.GetConnectionString("WallonsConnection"), 
					x => x.UseNetTopologySuite()
				));
			
			services.AddScoped<IPictureRepo, PictureRepo>();
			
			services.AddControllers();

			services.AddSingleton<IPictureHelper, PictureHelper>();
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
	}
}
