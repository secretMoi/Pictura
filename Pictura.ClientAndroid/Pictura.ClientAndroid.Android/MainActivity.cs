using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Microsoft.Extensions.DependencyInjection;
using Pictura.ClientAndroid.PlatformInterfaces;

namespace Pictura.ClientAndroid.Droid
{
    [Activity(Label = "Pictura.ClientAndroid", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(AddServices));
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDg1NTA2QDMxMzkyZTMyMmUzMGsxWU9NN2VDZnFWTzlaK3U0aEpPdkxGK3hUT29NR3JFYkJYREg2OU1ycnc9");
        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        
        static void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IThumbnailService, ThumbnailService>();
        }
    }
}