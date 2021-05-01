using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pictura.Server.Helpers.Pictures;
using Pictura.Server.Models;
using Pictura.Server.Services.Data.Picture;
using Pictura.Server.Services.File;
using Pictura.Shared.Extensions;
using Pictura.Shared.Models;

namespace Pictura.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PictureController : ControllerBase
	{
		private readonly IPictureHelper _pictureHelper;
		private readonly IPictureRepo _pictureRepo;
		private readonly IFileService _fileService;
		private readonly PictureOptions _pictureConfiguration;

		public PictureController(IConfiguration configuration, IPictureHelper pictureHelper, IPictureRepo pictureRepo, 
			IFileService fileService)
		{
			_pictureHelper = pictureHelper;
			_pictureRepo = pictureRepo;
			_fileService = fileService;

			_pictureConfiguration = configuration.GetModelFromSection<PictureOptions>("Picture");
		}

		[HttpGet("FilesFromDisk", Name = "FilesFromDisk")]
		public async Task<IActionResult> GetAllPicturesFromDisk()
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
		
		[HttpGet("FilesFromBd", Name = "FilesFromBd")]
		public async Task<IActionResult> GetAllPicturesFromDatabase()
		{
			var t = await _pictureRepo.GetAllAsync();
			
			return Ok(t);
		}
		
		[HttpPost("Upload", Name = "Upload")]
		public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
		{
			Console.WriteLine("console");
			var size = files.Sum(file => file.Length);

			await _fileService.SaveFilesAsync(files);

			return Ok(new { count = files.Count, size });
		}
	}
}