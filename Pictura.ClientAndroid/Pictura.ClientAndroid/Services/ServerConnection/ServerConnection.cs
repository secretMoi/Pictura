using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Pictura.ClientAndroid.Models;
using Pictura.Shared.Extensions;

namespace Pictura.ClientAndroid.Services.ServerConnection
{
	public class ServerConnection : IServerConnection
	{
		private readonly ServerConfiguration _serverConfiguration;

		public ServerConnection(IConfiguration configuration)
		{
			_serverConfiguration = configuration.GetModelFromSection<ServerConfiguration>("Server");
		}

		private static HttpClient _apiClient;
		public HttpClient ApiClient => _apiClient;

		public void InitializeClient()
		{
			// trust any certificate
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			ServicePointManager.ServerCertificateValidationCallback +=
				(sender, cert, chain, sslPolicyErrors) => true;

			_apiClient = new HttpClient();

			// set une addresse de base (ex : http://xkcd.com/ , qui permet de manipuler plusieurs liens api de ce site)
			_apiClient.BaseAddress = new Uri(_serverConfiguration.Address);

			_apiClient.DefaultRequestHeaders.Accept.Clear(); // nettoie les headers

			// crée un header qui demande du json
			_apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}
	}
}