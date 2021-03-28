using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pictura.ClientAndroid.Helpers.Navigation;
using Pictura.ClientAndroid.Helpers.Routes;
using Pictura.ClientAndroid.ViewModels.Gallery;
using Pictura.ClientAndroid.Views;
using Xamarin.Essentials;

namespace Pictura.ClientAndroid.Helpers
{
	public static class Startup
	{
		public static IServiceProvider ServiceProvider { get; set; }
		
		public static void Init()
		{
			var assembly = Assembly.GetExecutingAssembly();
			using var stream = assembly.GetManifestResourceStream("Pictura.ClientAndroid.appsettings.json");
			
			var host = new HostBuilder()
				.ConfigureHostConfiguration(c =>
				{
					// Tell the host configuration where to find the file (this is required for Xamarin apps)
					c.AddCommandLine(new [] { $"ContentRoot={FileSystem.AppDataDirectory}" });
					
					c.AddJsonStream(stream);
				})
				.ConfigureServices(ConfigureServices)
				.Build();

			//Save our service provider so we can use it later.
			ServiceProvider = host.Services;
		}

		static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
		{
			// The HostingEnvironment comes from the appsetting.json and could be optionally used to configure the mock service
			if (context.HostingEnvironment.IsDevelopment())
			{
				services.AddSingleton<INavigation, ShellNavigation>();
			}
			else
			{
				services.AddSingleton<INavigation, ShellNavigation>();
			}

			services.AddSingleton<IRoute>(new Route("Pictura.ClientAndroid.Views"));
			
			services.AddSingleton<GalleryViewModel>();
			services.AddSingleton<PictureFullScreenViewModel>();
			services.AddSingleton<AppShell>();

			//Another thing we can do is access variables from that json file
			var world = context.Configuration["Hello"];
		}
	}
}