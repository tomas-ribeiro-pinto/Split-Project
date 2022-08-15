using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Split
{
    public partial class AddExpensePage : ContentPage
    {
        public AddExpensePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            peopleEntry.ItemsSource = await App.PeopleDatabase.GetPeopleAsync();
        }

        async void AddExpense(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(titleEntry.Text) && !string.IsNullOrWhiteSpace(amountEntry.Text)
                && peopleEntry.SelectedItem != null)
            {
                // create DateTime() from selected date on picker
                People person = (People)peopleEntry.SelectedItem;

                Expense newExpense = new Expense
                {
                    Title = titleEntry.Text,
                    Amount = double.Parse(amountEntry.Text),
                    DateExpense = dateEntry.Date,
                    DateCreated = DateTime.Now,
                    ExpensePersonID = person.ID
                };

                await App.ExpenseDatabase.SaveExpenseAsync(newExpense);
                titleEntry.Text = amountEntry.Text = string.Empty;
                await Navigation.PopToRootAsync(false);
            }
        }
    }
}
