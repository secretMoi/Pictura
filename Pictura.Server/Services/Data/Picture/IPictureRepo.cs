using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pictura.Server.Services.Data.Picture
{
	public interface IPictureRepo
	{
		Context Context { get; }
		Task<List<Models.Picture>> GetAllAsync();
	}
}