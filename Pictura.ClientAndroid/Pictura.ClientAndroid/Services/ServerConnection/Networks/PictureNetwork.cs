using System;
using System.Collections.Generic;
using System.IO;
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

		public async Task PostStreamAsync(IEnumerable<FileStream> fileStreams)
		{
			var url = MakeUrl("Upload");

			try
			{
				var multipartContent = new MultipartFormDataContent();

				foreach (var fileStream in fileStreams)
				{
					multipartContent.Add(new StreamContent(fileStream), "files", Path.GetFileName(fileStream.Name));
				}
				using var response = await ServerConnection.PostAsync(url, multipartContent);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		
		public async Task<IList<PictureTransferModel>> GetFilesFromDiskAsync()
		{
			var url = MakeUrl("FilesFromDisk");

			try
			{
				// fais une req sur l'url et attend la réponse
				using var response = await ServerConnection.GetAsync(url);
				if (response.IsSuccessStatusCode)
				{
					// map le json lu dans la req http dans le model
					var data = await response.Content.ReadAsAsync<IList<PictureTransferModel>>();

					return data;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

			return null;
			//throw new Exception(response.ReasonPhrase);
		}
	}
}