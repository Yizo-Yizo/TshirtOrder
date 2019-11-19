using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
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
            WebClient client = new WebClient();
            Uri uri = new Uri("http://10.0.2.2/App1Controller");


            var App1Item = (App1Item)BindingContext;
            await App.Database.SaveItemAsync(App1Item);
            await Navigation.PopAsync();
            
        }


        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            
        }

    }
}
