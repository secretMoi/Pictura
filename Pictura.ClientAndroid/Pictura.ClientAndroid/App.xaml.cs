using System;
using Microsoft.Extensions.DependencyInjection;
using Pictura.ClientAndroid.Helpers;
using Pictura.ClientAndroid.Services;
using Xamarin.Forms;

namespace Pictura.ClientAndroid
{
	public partial class App
	{
		public static IServiceProvider DiServices => Startup.ServiceProvider;
		
		public App(Action<IServiceCollection> addPlatformServices = null)
		{
			InitializeComponent();

			DependencyService.Register<MockDataStore>();
			Startup.Init(addPlatformServices);
			
			MainPage = Startup.ServiceProvider.GetService<AppShell>();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
