using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    public partial class App : Application
    {
        static App1DataBase database;

        public App()
        {
            Resources = new ResourceDictionary();
            Resources.Add("primaryOrange", Color.FromHex("FFA9A9A9"));
            

            var nav = new NavigationPage(new OrderList());
            nav.BarBackgroundColor = (Color)App.Current.Resources["primaryOrange"];
            nav.BarTextColor = Color.White;

            MainPage = nav;
        }

        public static App1DataBase Database
        {
            get
            {
                if (database == null)
                {
                    database = new App1DataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "App1SQLite.db3"));
                }
                return database;
            }
        }

        public int ResumeAtApp1Id { get; set; }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
