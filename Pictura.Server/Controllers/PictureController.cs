﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pictura.Server.Helpers.Pictures;
using Pictura.Server.Services.Data.Picture;
using Pictura.Server.Services.File;
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

		public PictureController(IPictureHelper pictureHelper, IPictureRepo pictureRepo, IFileService fileService)
		{
			_pictureHelper = pictureHelper;
			_pictureRepo = pictureRepo;
			_fileService = fileService;
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
			Console.WriteLine($"Réception de {files.Count} médias uploadés");
			var size = files.Sum(file => file.Length);

			try
			{
				await _fileService.SaveFilesAsync(files);
				Console.WriteLine($"Les {files.Count} médias ont été sauvés");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return BadRequest();
			}

			return Ok(new { count = files.Count, size });
		}
	}
}