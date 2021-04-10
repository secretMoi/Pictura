using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Pictura.Server.Services.Data.Picture
{
	public class PictureRepo : IPictureRepo
	{
		private readonly Context _context;
		public Context Context => _context;

		public PictureRepo(Context context)
		{
			_context = context;
		}
		
		public async Task<List<Models.Picture>> GetAllAsync()
		{
			return await _context.Pictures.ToListAsync(); // retourne la liste des commandes
		}
	}
}