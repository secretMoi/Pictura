using Xamarin.Forms;

namespace Pictura.ClientAndroid.Helpers.Routes
{
	public interface IRoute
	{
		void RegisterRoute<T>() where T : Page;
		string RouteName(string completeNamespace);
	}
}