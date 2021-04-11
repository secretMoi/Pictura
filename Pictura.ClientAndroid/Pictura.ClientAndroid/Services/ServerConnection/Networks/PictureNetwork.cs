using System.Net.Http;
using System.Threading.Tasks;
using Pictura.Shared.Models;

namespace Pictura.ClientAndroid.Services.ServerConnection.Networks
{
	public class PictureNetwork : ServerNetwork<PictureTransferModel>, IPictureNetwork
	{
		public PictureNetwork(IServerConnection serverConnection) : base(serverConnection)
		{
			Url = "Picture";

			FillBaseMethods(
				BaseMethod.GetAll,
				BaseMethod.GetId,
				BaseMethod.Post,
				BaseMethod.Update,
				BaseMethod.Delete
			);
		}

		// public async Task<PictureTransferModel> ByMail(string mail)
		// {
		// 	string url = MakeUrl("ParMail");
		//
		// 	Client model = new Client
		// 	{
		// 		Mail = mail
		// 	};
		// 	StringContent dataJson = SerializeAsJson(model);
		//
		// 	// fais une req sur l'url et attend la réponse
		// 	using (HttpResponseMessage response = await Api.ApiClient.PostAsync(url, dataJson))
		// 	{
		// 		model = await response.Content.ReadAsAsync<Client>();
		// 	}
		//
		// 	return model;
		// }
	}
}