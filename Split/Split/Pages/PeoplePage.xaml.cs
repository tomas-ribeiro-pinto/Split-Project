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

        async void OnEdit(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var person = (People)button.BindingContext;

            Page EditPeoplePage = new EditPeoplePage
            {
                Title = $"Expenses of {person.Name}"
            };

            EditPeoplePage.BindingContext = person;
            await Navigation.PushAsync(EditPeoplePage);

        }
    }
}
