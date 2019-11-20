using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
            await Navigation.PopAsync();

            var url = "http://10.0.2.2/App1";
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(App1Item);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(url, content);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Exception", ex.Message, "OK");
            }

        }


        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            
        }

    }
}
