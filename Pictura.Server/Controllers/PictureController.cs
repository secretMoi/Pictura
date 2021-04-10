using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pictura.Server.Helpers.Json;
using Pictura.Server.Helpers.Pictures;

namespace Pictura.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PictureController : ControllerBase
	{
		private readonly ILogger<WeatherForecastController> _logger;
		private readonly IPictureHelper _pictureHelper;
		private readonly IConfigurationSection _pictureSectionConfiguration;
		private readonly PictureConfigurationModel _pictureConfiguration;

		public PictureController(ILogger<WeatherForecastController> logger, IConfiguration configuration,
			IPictureHelper pictureHelper)
		{
			_logger = logger;
			_pictureHelper = pictureHelper;
			_pictureConfiguration = configuration
				.GetSection("PictureHelper")
				.Get<PictureConfigurationModel>();
		}

		[HttpGet]
		public async Task<IActionResult> SayHello()
		{
			var t = _pictureHelper.GetAllFiles();
			return Ok(await Task.Run(() => "hello"));
		}
	}
}