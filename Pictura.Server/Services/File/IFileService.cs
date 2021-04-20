using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Pictura.Server.Services.File
{
	public interface IFileService
	{
		Task SaveFileAsync(IFormFile file);
		Task SaveFilesAsync(IEnumerable<IFormFile> files);
	}
}