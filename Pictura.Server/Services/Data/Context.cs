using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Pictura.Server.Services.Data.Picture;

namespace Pictura.Server.Services.Data
{
	public class Context : DbContext
	{
		public Context(DbContextOptions<Context> options) : base(options)
		{
			
		}
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
		
		public DbSet<Models.Picture> Pictures { get; set; } // map entity framework avec nos modèles
	}
}