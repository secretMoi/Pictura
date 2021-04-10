﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Pictura.Server.Helpers.Json;
using Pictura.Server.Helpers.Pictures;
using Pictura.Server.Services.Data.Picture;
using Pictura.Shared.Models;

namespace Pictura.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PictureController : ControllerBase
	{
		private readonly ILogger<WeatherForecastController> _logger;
		private readonly IPictureHelper _pictureHelper;
		private readonly IPictureRepo _pictureRepo;
		private readonly IConfigurationSection _pictureSectionConfiguration;
		private readonly PictureConfigurationModel _pictureConfiguration;

		public PictureController(ILogger<WeatherForecastController> logger, IConfiguration configuration,
			IPictureHelper pictureHelper, IPictureRepo pictureRepo)
		{
			_logger = logger;
			_pictureHelper = pictureHelper;
			_pictureRepo = pictureRepo;

			_pictureConfiguration = configuration
				.GetSection("PictureHelper")
				.Get<PictureConfigurationModel>();
		}

		[HttpGet]
		public async Task<IActionResult> SayHello()
		{
			var filesOnDisk = await _pictureHelper.GetAllFilesAsync();

			var filesToReturn = new Collection<PictureTransferModel>();
			int id = 0;
			foreach (var file in filesOnDisk)
			{
				filesToReturn.Add(new PictureTransferModel
					{
						Id = id,
						Path = file
					}
				);
				id++;
			}
			return Ok(filesToReturn);
		}
		
		[HttpGet]
		public async Task<IActionResult> SayHello2()
		{
			var t = await _pictureRepo.GetAllAsync();
			
			return Ok();
		}
	}
}