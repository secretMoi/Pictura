using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Pictura.ClientAndroid.Models;

namespace Pictura.ClientAndroid.Services.ServerConnection
{
	public class ServerConnection : IServerConnection
	{
		private readonly ServerConfiguration _serverConfiguration;

		public ServerConnection(IOptions<ServerConfiguration> serverConfiguration)
		{
			_serverConfiguration = serverConfiguration.Value;
		}

		private static HttpClient _apiClient;
		public HttpClient ApiClient => _apiClient;

		public void InitializeClient()
		{
			// trust any certificate
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			ServicePointManager.ServerCertificateValidationCallback +=
				(_, _, _, _) => true;

			_apiClient = new HttpClient(GetInsecureHandler());

			// set une addresse de base (ex : http://xkcd.com/ , qui permet de manipuler plusieurs liens api de ce site)
			_apiClient.BaseAddress = new Uri(_serverConfiguration.Address);

			_apiClient.DefaultRequestHeaders.Accept.Clear(); // nettoie les headers

			// crée un header qui demande du json
			_apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}
		
		public HttpClientHandler GetInsecureHandler()
		{
			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (_, _, _, _) => true
			};
			return handler;
		}
	}
}