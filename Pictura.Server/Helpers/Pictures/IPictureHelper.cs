using System.Threading.Tasks;

namespace Pictura.Server.Helpers.Pictures
{
	public interface IPictureHelper
	{
		Task<string[]> GetAllFilesAsync();
	}
}