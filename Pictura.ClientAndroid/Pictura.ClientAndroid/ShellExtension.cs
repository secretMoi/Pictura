using System;
using Microsoft.Extensions.DependencyInjection;
using Pictura.ClientAndroid.ViewModels.Gallery;
using Xamarin.Forms;

namespace Pictura.ClientAndroid
{
	public static class ShellExtension
	{
		public static IServiceProvider ServiceProvider(this Shell shell)
		{
			return (shell as AppShell)?.ServiceProvider();
		}
		
		public static IServiceCollection AddViewModels(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<GalleryViewModel>();
			serviceCollection.AddSingleton<PictureFullScreenViewModel>();

			return serviceCollection;
		}
		
		private static IServiceCollection AddView<TView, TViewModel>(this IServiceCollection serviceCollection) where TView : Page
		{
			return serviceCollection.AddTransient(serviceProvider =>
			{
				var view = ActivatorUtilities.CreateInstance<TView>(serviceProvider);

				// Automatically bind the view model
				view.BindingContext = serviceProvider.GetRequiredService<TViewModel>();

				// You could also forward Appearing and Disappearing page events to your view model (again inspired by Prism)
				// view.Appearing += (sender, _) => (((BindableObject)sender).BindingContext as IPageLifeCycleAware)?.OnAppearing();
				// view.Disappearing += (sender, _) => (((BindableObject)sender).BindingContext as IPageLifeCycleAware)?.OnDisappearing();

				return view;
			});
		}
	}
}