using App1;
using System;
using System.Diagnostics;
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
        }
    }
}
