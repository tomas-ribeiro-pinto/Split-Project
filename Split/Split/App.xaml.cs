using System;
using System.IO;
using Xamarin.Forms;

namespace Split
{
    public partial class App : Application
    {
        static ExpenseDatabase expenseDatabase;

        public static ExpenseDatabase ExpenseDatabase
        {
            get
            {
                if (expenseDatabase == null)
                {
                    expenseDatabase = new ExpenseDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "expense.db3"));
                }
                return expenseDatabase;
            }
        }

        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();

            MainPage = new NavigationPage(new MainPage());
            //MainPage.Title = "Welcome to Split";
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