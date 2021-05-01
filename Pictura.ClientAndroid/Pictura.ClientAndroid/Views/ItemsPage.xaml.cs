using Pictura.ClientAndroid.ViewModels;

namespace Pictura.ClientAndroid.Views
{
	public partial class ItemsPage
	{
		private readonly ItemsViewModel _viewModel;

		public ItemsPage()
		{
			InitializeComponent();

			BindingContext = _viewModel = new ItemsViewModel();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			_viewModel.OnAppearing();
		}
	}
}