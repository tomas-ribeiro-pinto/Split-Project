using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Split.Pages
{
    public partial class PeoplePage : ContentPage
    {
        public PeoplePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            peopleView.ItemsSource = await App.PeopleDatabase.GetPeopleAsync();
        }

        async void AddPeoplePage(object sender, EventArgs e)
        {
            Page AddPeoplePage = new AddPeoplePage
            {
                Title = "Add Person"
            };
            await Navigation.PushAsync(AddPeoplePage);
        }
    }
}
