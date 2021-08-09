using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pictura.ClientAndroid.Models;
using Pictura.ClientAndroid.Services.Files;
using Pictura.ClientAndroid.Services.Navigation;
using Pictura.ClientAndroid.Services.Routes;
using Pictura.ClientAndroid.Services.ServerConnection;
using Pictura.ClientAndroid.Services.ServerConnection.Networks;
using Pictura.ClientAndroid.ViewModels.Gallery;
using Xamarin.Essentials;
using INavigation = Pictura.ClientAndroid.Services.Navigation.INavigation;

namespace Pictura.ClientAndroid.Helpers
{
	public static class Startup
	{
		private static Action<IServiceCollection> _addPlatformServices;
		
		public static IServiceProvider ServiceProvider { get; private set; }

		public static void Init(Action<IServiceCollection> addPlatformServices = null)
		{
			_addPlatformServices = addPlatformServices;
			var assembly = Assembly.GetExecutingAssembly();
			var host = new HostBuilder()
				.ConfigureHostConfiguration(c => 
				{
					var stream = assembly.GetManifestResourceStream("Pictura.ClientAndroid.appsettings.json");
					
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
			
			services.Configure<PathOptions>(context.Configuration.GetSection("Path"));
			services.Configure<ServerConfiguration>(context.Configuration.GetSection("Server"));
			
			// Add platform specific services
			_addPlatformServices?.Invoke(services);

			services.AddSingleton<IRoute>(new Route("Pictura.ClientAndroid.Views"));
			services.AddSingleton<IServerConnection, ServerConnection>();
			services.AddSingleton<IFileService, FileService>();
			services.AddSingleton<IPictureNetwork, PictureNetwork>();

			services.AddTransient<GalleryViewModel>();
			services.AddTransient<PictureFullScreenViewModel>();
			services.AddTransient<MetaDataInfoViewModel>();
			services.AddSingleton<AppShell>();

			//Another thing we can do is access variables from that json file
			//var world = context.Configuration["Hello"];
		}
	}
}