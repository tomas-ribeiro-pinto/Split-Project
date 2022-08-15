using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Split.Pages
{
    public partial class AddPeoplePage : ContentPage
    {
        public AddPeoplePage()
        {
            InitializeComponent();
        }

        async void AddPerson(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                People newPerson = new People
                {
                    Name = nameEntry.Text,
                };

                await App.PeopleDatabase.SavePeopleAsync(newPerson);
                nameEntry.Text = string.Empty;
                await Navigation.PopAsync(false);
            }
        }
    }
}
