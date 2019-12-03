using App1;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Diagnostics;
using System.Net.Http;
using TshirtApp;
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
                BindingContext = new OrderItem()
            });
            
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            /*  if (e.SelectedItem != null)
              {
                  await Navigation.PushAsync(new MainPage
                  {
                      BindingContext = e.SelectedItem as App1Item
                  });

              }*/

            var item = e.SelectedItem as OrderItem;

            var placemark = new Placemark
            {
                Thoroughfare = item.ShippingAddress
            };
            var options = new MapLaunchOptions { Name = item.ShippingAddress };

            await Map.OpenAsync(placemark, options);
        }

        async void OnPost(object sender, EventArgs e)
        {

            // var App1Item = (App1Item)BindingContext;

            var unPostedItems = await App.Database.GetUnPostedAppItems();
            

            var url = "http://10.0.2.2:57886/App1";
            var client = new HttpClient();


            try
            {
                foreach (OrderItem unPosted in unPostedItems)
                {

                    var json = JsonConvert.SerializeObject(unPosted);
                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);

                    unPosted.Posted = true;
                    
                    await App.Database.SaveItemAsync(unPosted);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Exception", ex.Message, "OK");


            }
        }

    }
}

