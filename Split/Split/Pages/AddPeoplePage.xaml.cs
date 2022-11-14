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
                Person newPerson = new Person
                {
                    Name = nameEntry.Text,
                };

                await App.SplitDatabase.SavePersonAsync(newPerson);
                nameEntry.Text = string.Empty;
                await Navigation.PopAsync(false);
            }
        }
    }
}
