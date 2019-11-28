using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1
{
   
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var appitem = new App1Item();

            BindingContext = appitem;
        }


        async void OnSaveClicked(object sender, EventArgs e)
        {
            var App1Item = (App1Item)BindingContext;
            await App.Database.SaveItemAsync(App1Item);

          /*  var la = (la)BindingContext;
            await App.Database.SaveItemAsync(la);

            var lon = (lon)BindingContext;
            await App.Database.SaveItemAsync(la); */
            
            await Navigation.PopAsync();

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                /*double lon = location.Longitude;
                double la = location.Latitude;*/



            }
            catch (FeatureNotSupportedException ex)
            {
                // Handle not supported on device exception
                await DisplayAlert("Alert", "Device not supported", "OK");

            }
            catch (FeatureNotEnabledException ex)
            {
                // Handle not enabled on device exception
                await DisplayAlert("Alert", "Not enabled on the device", "OK");
            }
            catch (PermissionException ex)
            {
                // Handle permission exception
                await DisplayAlert("Alert", "No Permission", "OK");
            }
            catch (Exception ex)
            {
                // Unable to get location
                await DisplayAlert("Alert", "Unable to get location", "OK");
            }

        }


        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            
        }

    }
}
