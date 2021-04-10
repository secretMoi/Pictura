#nullable enable
using System;

namespace Pictura.Server.Models
{
	public class Picture
	{
		public int Id { get; set; }
		public string? ImageName { get; set; }
		public string? Thumbnail { get; set; }
		public DateTime? AddDate { get; set; }
		public DateTime? LastUpdate { get; set; }
		public NetTopologySuite.Geometries.Point? GeographicalCoordinates { get; set; }
	}
}