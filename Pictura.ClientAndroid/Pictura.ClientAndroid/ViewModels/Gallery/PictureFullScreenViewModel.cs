using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using INavigation = Pictura.ClientAndroid.Helpers.Navigation.INavigation;

namespace Pictura.ClientAndroid.ViewModels.Gallery
{
	[QueryProperty(nameof(ImagePath), nameof(ImagePath))]
	public class PictureFullScreenViewModel : BaseViewModel
	{
		private readonly INavigation _navigation;

		public PictureFullScreenViewModel(INavigation navigation)
		{
			_navigation = navigation;
			SwipedDownCommand = new Command(async () => await OnPictureSwipedDown());
		}

		private async Task OnPictureSwipedDown()
		{
			await _navigation.GoBackAsync();
		}

		public ICommand SwipedDownCommand { get; set; }
		
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

		public void LoadPicture(string itemId)
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