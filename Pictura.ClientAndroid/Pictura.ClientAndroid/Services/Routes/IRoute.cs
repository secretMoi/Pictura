using Xamarin.Forms;

namespace Pictura.ClientAndroid.Services.Routes
{
	public interface IRoute
	{
		void RegisterRoute<T>() where T : Page;
		string RouteName(string completeNamespace);
	}
}