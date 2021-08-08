using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Pictura.ClientAndroid.Droid
{
	public class Setup
	{
		public static Action<IConfigurationBuilder> Configuration => builder =>
		{
			builder.AddJsonFile(new EmbeddedFileProvider(typeof(Setup).Assembly, typeof(Setup).Namespace),
				"appsettings.json", false, false);
		};
		
		public static Action<IServiceCollection, IConfigurationRoot> DependencyInjection =>
			(serviceCollection, configurationRoot) =>
			{
				serviceCollection.AddSingleton<IThumbnailService, ThumbnailService>();
			};
	}
}