using System.Windows.Input;
using Pictura.ClientAndroid.Views;
using Xamarin.Forms;
using INavigation = Pictura.ClientAndroid.Services.Navigation.INavigation;

namespace Pictura.ClientAndroid.ViewModels.Gallery
{
	[QueryProperty(nameof(ImagePath), nameof(ImagePath))]
	public class PictureFullScreenViewModel : BaseViewModel
	{
		private readonly INavigation _navigation;

		public PictureFullScreenViewModel(INavigation navigation)
		{
			_navigation = navigation;
			SwipedDownCommand = new Command(OnPictureSwipedDownAsync);
			DisplayMetadataCommand = new Command(NavigateToDisplayMetadataAsync);
		}

		private async void NavigateToDisplayMetadataAsync()
		{
			await _navigation.PushAsync<MetaDataInfoPage>(nameof(MetaDataInfoViewModel.ImagePath), ImagePath);
		}

		private async void OnPictureSwipedDownAsync()
		{
			await _navigation.GoBackAsync();
		}

		public ICommand SwipedDownCommand { get; set; }
		public ICommand DisplayMetadataCommand { get; set; }
		
		private string _imagePath;
		public string ImagePath
		{
			get => _imagePath;
			set => SetProperty(ref _imagePath, value);
		}
	}
}