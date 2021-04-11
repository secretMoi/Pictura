using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pictura.ClientAndroid.Services.ServerConnection.Networks
{
	public class ServerNetwork<T> : IServerNetwork<T>
	{
		private readonly HttpClient _serverConnection;
		protected string Url;
		protected readonly List<BaseMethod> BaseMethods;

		protected enum BaseMethod
		{
			GetAll,
			GetId,
			Update,
			Post,
			Delete
		}

		public ServerNetwork(IServerConnection serverConnection)
		{
			_serverConnection = serverConnection.ApiClient;
			BaseMethods = new List<BaseMethod>();
		}

		/**
		 * <summary>Permet d'autoriser les classes enfants à appeler les méthodes parentes pour les requêtes</summary>
		 * <param name="methods">Tableau des méthodes à autoriser</param>
		 */
		protected void FillBaseMethods(params BaseMethod[] methods)
		{
			foreach (var method in methods)
				BaseMethods.Add(method);
		}

		/**
		 * <summary>Sérialise un model dto en JSON</summary>
		 * <param name="dto">Le model à sérialiser</param>
		 * <returns>Une string contenant le code JSON</returns>
		 */
		protected StringContent SerializeAsJson(T dto)
		{
			var json = JsonConvert.SerializeObject(dto);
			return new StringContent(json, Encoding.UTF8, "application/json");
		}

		/**
		 * <summary>Génère l'url du serveur pour la requête</summary>
		 * <param name="suffix">Paramètres à ajouter à l'url</param>
		 * <returns>Une string étant l'url absolue</returns>
		 */
		protected string MakeUrl(params object[] suffix)
		{
			var url = new StringBuilder(Url);

			foreach (var element in suffix)
				url.Append("/" + element);

			return url.ToString();
		}

		/**
		 * <summary>Requête sélectionnant un élément par son ID</summary>
		 * <param name="id">Id de l'enregistrement à récupérer</param>
		 * <returns>Renvoie le model cherché</returns>
		 */
		public virtual async Task<T> GetById(int id)
		{
			if (!BaseMethods.Contains(BaseMethod.GetId)) return default;

			var url = MakeUrl("Afficher", id);

			// fais une req sur l'url et attend la réponse
			using var response = await _serverConnection.GetAsync(url);
			if (response.IsSuccessStatusCode)
			{
				// map le json lu dans la req http dans le model
				var data = await response.Content.ReadAsAsync<T>();

				return data;
			}
			
			throw new Exception(response.ReasonPhrase);
		}

		/**
		 * <summary>Requête sélectionnant tous les éléments</summary>
		 * <returns>Renvoie une liste des models trouvés</returns>
		 */
		public virtual async Task<IList<T>> GetAll()
		{
			if (!BaseMethods.Contains(BaseMethod.GetAll)) return default;

			var url = MakeUrl("Afficher");

			// fais une req sur l'url et attend la réponse
			using var response = await _serverConnection.GetAsync(url);
			if (response.IsSuccessStatusCode)
			{
				// map le json lu dans la req http dans le model
				var data = await response.Content.ReadAsAsync<IList<T>>();

				return data;
			}
				
			throw new Exception(response.ReasonPhrase);
		}

		/**
		 * <summary>Requête modifiant un enregistremen</summary>
		 * <param name="data">Model de l'enregistrement à envoyer</param>
		 */
		public virtual async Task Update(T data)
		{
			if (!BaseMethods.Contains(BaseMethod.Update)) return;

			var url = MakeUrl("Update");

			var dataJson = SerializeAsJson(data);

			// fais une req sur l'url et attend la réponse
			using var response = await _serverConnection.PutAsync(url, dataJson);
			await response.Content.ReadAsStringAsync();
		}

		/**
		 * <summary>Requête envoyant un nouvel enregistrement</summary>
		 * <param name="input">Model à envoyer et créer</param>
		 * <returns>Renvoie le model créé</returns>
		 */
		public virtual async Task Post(T input)
		{
			if (!BaseMethods.Contains(BaseMethod.Post)) return;

			var url = MakeUrl("Ajouter");

			var dataJson = SerializeAsJson(input);

			//string result = null;

			// fais une req sur l'url et attend la réponse
			await _serverConnection.PostAsync(url, dataJson);
		}

		/**
		 * <summary>Requête supprimant un élément par son ID</summary>
		 * <param name="id">Id de l'enregistrement à supprimer</param>
		 * <returns>Renvoie le model supprimé</returns>
		 */
		public virtual async Task Delete(int id)
		{
			if (!BaseMethods.Contains(BaseMethod.Delete)) return;

			var url = MakeUrl("Supprimer", id);

			await _serverConnection.DeleteAsync(url);
		}
	}
}