﻿using Xamarin.Forms;

namespace Pictura.ClientAndroid.ViewModels.Gallery
{
	[QueryProperty(nameof(ImagePath), nameof(ImagePath))]
	public class PictureFullScreenViewModel : BaseViewModel
	{
		private string _imagePath;
		public string ImagePath
		{
			get => _imagePath;
			set
			{
				SetProperty(ref _imagePath, value);
				LoadPicture(value);
			}
		}

		public async void LoadPicture(string itemId)
		{
			/*try
			{
				var item = await DataStore.GetItemAsync(itemId);
				Id = item.Id;
				Text = item.Text;
				Description = item.Description;
			}
			catch (Exception)
			{
				Debug.WriteLine("Failed to Load Item");
			}*/
		}
	}
}