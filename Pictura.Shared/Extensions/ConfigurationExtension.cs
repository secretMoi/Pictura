using Microsoft.Extensions.Configuration;

namespace Pictura.Shared.Extensions
{
	public static class ConfigurationExtension
	{
		public static T GetModelFromSection<T>(this IConfiguration configuration, string sectionName)
		{
			return configuration
				.GetSection(sectionName)
				.Get<T>();
		}
	}
}