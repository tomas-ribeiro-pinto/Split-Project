using System;
using System.IO;
using Xamarin.Forms;

namespace Split
{
    public partial class App : Application
    {
        static SplitDatabase database;

        public static SplitDatabase SplitDatabase
        {
            get
            {
                if (database == null)
                {
                    database = new SplitDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "expense_v2.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

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