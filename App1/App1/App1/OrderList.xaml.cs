using App1;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Diagnostics;
using System.Net.Http;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1
{
    public partial class OrderList : ContentPage
    {
        public OrderList()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Reset the 'resume' id, since we just want to re-start here
            ((App)App.Current).ResumeAtApp1Id = -1;
            listView.ItemsSource = await App.Database.GetItemsAsync(); 
        }

        async void OnItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage
            {
                BindingContext = new App1Item()
            });
            
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
      
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new MainPage
                {
                    BindingContext = e.SelectedItem as App1Item
                });
                
            }
            var address = "ShippingAddres";
            Map.OpenAsync(address, new MapLaunchOptions
            {
                Name = EditorPlaceholder.Text,
                NavigationMode = NavigationMode.None
            });
        }

        async void OnPost(object sender, EventArgs e)
        {
            var App1Item = (App1Item)BindingContext;
            var url = "http://10.0.2.2/App1";
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(App1Item);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            if (Id is false)
            {
                try
                {
                    var response = await client.PostAsync(url, content);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Exception", ex.Message, "OK");
                }
            }

        }

    }
}

