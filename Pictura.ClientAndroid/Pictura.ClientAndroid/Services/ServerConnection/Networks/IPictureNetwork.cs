using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Pictura.Shared.Models;

namespace Pictura.ClientAndroid.Services.ServerConnection.Networks
{
	public interface IPictureNetwork : IServerNetwork<PictureTransferModel>
	{
		Task<IList<PictureTransferModel>> GetFilesFromDiskAsync();
		Task PostStreamAsync(IEnumerable<FileStream> fileStreams);
	}
}