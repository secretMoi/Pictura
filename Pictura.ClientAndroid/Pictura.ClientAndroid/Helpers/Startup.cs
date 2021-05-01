using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pictura.ClientAndroid.Helpers.Navigation;
using Pictura.ClientAndroid.Helpers.Routes;
using Pictura.ClientAndroid.Services.Files;
using Pictura.ClientAndroid.Services.ServerConnection;
using Pictura.ClientAndroid.Services.ServerConnection.Networks;
using Pictura.ClientAndroid.ViewModels.Gallery;
using Xamarin.Essentials;

namespace Pictura.ClientAndroid.Helpers
{
	public static class Startup
	{
		public static IServiceProvider ServiceProvider { get; private set; }
		
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
			
			ServiceProvider.GetService<IServerConnection>()?.InitializeClient();
			ServiceProvider.GetService<IPictureNetwork>()?.GetFilesFromDiskAsync();
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
			services.AddSingleton<IServerConnection, ServerConnection>();
			services.AddSingleton<IFileService, FileService>();
			services.AddSingleton<IPictureNetwork, PictureNetwork>();

			services.AddSingleton<GalleryViewModel>();
			services.AddSingleton<PictureFullScreenViewModel>();
			services.AddSingleton<AppShell>();

			//Another thing we can do is access variables from that json file
			var world = context.Configuration["Hello"];
		}
	}
}