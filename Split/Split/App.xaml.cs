using System;
using System.IO;
using Xamarin.Forms;

namespace Split
{
    public partial class App : Application
    {
        static ExpenseDatabase expenseDatabase;
        static PeopleDatabase peopleDatabase;
        static RecordDatabase recordDatabase;

        public static ExpenseDatabase ExpenseDatabase
        {
            get
            {
                if (expenseDatabase == null)
                {
                    expenseDatabase = new ExpenseDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "expense_v1.db3"));
                }
                return expenseDatabase;
            }
        }

        public static PeopleDatabase PeopleDatabase
        {
            get
            {
                if (peopleDatabase == null)
                {
                    peopleDatabase = new PeopleDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "people_v1.db3"));
                }
                return peopleDatabase;
            }
        }

        public static RecordDatabase RecordDatabase
        {
            get
            {
                if (recordDatabase == null)
                {
                    recordDatabase = new RecordDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "record_v1.db3"));
                }
                return recordDatabase;
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