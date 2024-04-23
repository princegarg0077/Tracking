using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MapboxNavigation.StringConstant;
using Xamarin.Essentials;

namespace MapboxNavigation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            string mapUrl = $"https://api.mapbox.com/styles/v1/mapbox/streets-v11.html?access_token={AppConstant.MapBoxKey}";
            webView.Source = mapUrl;
        }

        private async void OnShowLocationClicked(object sender, EventArgs e)
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                    if (status != PermissionStatus.Granted)
                    {
                        return;
                    }
                }
                var location = await Geolocation.GetLocationAsync();
                if (location != null)
                {
                    string mapboxToken = AppConstant.MapBoxKey;
                    string url = $"https://api.mapbox.com/styles/v1/mapbox/streets-v11/static/{location.Longitude},{location.Latitude},15/1000x1000?access_token={mapboxToken}&scrollwheel=false&touchZoom=false&dragPan=false";

                    webView.Source = new UrlWebViewSource { Url = url };

                }
                else
                {
                }
            }
            catch (Exception ex)
            {
             Console.WriteLine(ex.Message);
            }
        }
    }
}

