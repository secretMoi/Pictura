using Pictura.ClientAndroid.Services;
using Xamarin.Forms;

namespace Pictura.ClientAndroid
{
	public partial class App
	{
		public App()
		{
			InitializeComponent();

			DependencyService.Register<MockDataStore>();
			
			MainPage = new AppShell();
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
