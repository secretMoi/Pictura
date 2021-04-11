using System.Net.Http;

namespace Pictura.ClientAndroid.Services.ServerConnection
{
	public interface IServerConnection
	{
		public HttpClient ApiClient { get; }
		void InitializeClient();
	}
}