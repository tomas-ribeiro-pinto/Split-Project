using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Split.Pages;
using Xamarin.Forms;

namespace Split
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            tripView.ItemsSource = await App.SplitDatabase.GetTripListAsync();
        }


        async void OnSee(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var currentSelection = (Trip)button.BindingContext;

            Page TripPage = new TripPage
            {
                Title = $"Trip to {currentSelection.Title}"
            };

            TripPage.BindingContext = currentSelection;
            await Navigation.PushAsync(TripPage);

        }

        async void OnAddTripClicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Add a new Trip", "Enter trip title (e.g. Brazil)");
            if (!String.IsNullOrEmpty(result))
            {
                Trip trip = new Trip();
                trip.Title = result;

                await App.SplitDatabase.SaveTripAsync(trip);
                OnAppearing();
            }
        }

        async void SeePeople(object sender, EventArgs e)
        {
            Page PeoplePage = new PeoplePage
            {
                Title = "People"
            };
            await Navigation.PushAsync(PeoplePage);
        }
    }
}

