using App1;
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

namespace TshirtApp
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

            var appitem = new OrderItem();

            BindingContext = appitem;
        }


        async void OnSaveClicked(object sender, EventArgs e)
        {
            var App1Item = (OrderItem)BindingContext;
           // await App.Database.SaveItemAsync(App1Item);

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                double Latitude = location.Latitude;
                double Longitude = location.Longitude;
                if (location != null)
                {
                    App1Item.Latitude = Latitude;
                    App1Item.Longitude = Longitude;
                    await App.Database.SaveItemAsync(App1Item);
                }

            }
            catch (FeatureNotSupportedException ex)
            {
                // Handle not supported on device exception
                await DisplayAlert("Alert", ex.Message, "OK");

            }
            catch (FeatureNotEnabledException ex)
            {
                // Handle not enabled on device exception
                await DisplayAlert("Alert", ex.Message, "OK");
            }
            catch (PermissionException ex)
            {
                // Handle permission exception
                await DisplayAlert("Alert", ex.Message, "OK");
            }
            catch (Exception ex)
            {
                // Unable to get location
                await DisplayAlert("Alert", ex.Message, "OK");
            }

            await Navigation.PopAsync();

            

        }


        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            
        }

    }
}
