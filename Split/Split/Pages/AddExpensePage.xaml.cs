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
            dateEntry.MaximumDate = DateTime.Now;
        }

        async void AddExpense(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(titleEntry.Text) && !string.IsNullOrWhiteSpace(amountEntry.Text)
                && peopleEntry.SelectedItem != null)
            {
                // create DateTime() from selected date on picker
                People person = (People)peopleEntry.SelectedItem;

                Expense newExpense = new Expense()
                {
                    Title = titleEntry.Text,
                    Amount = double.Parse(amountEntry.Text),
                    DateExpense = dateEntry.Date,
                    DateCreated = DateTime.Now,
                    ExpensePersonId = person.ID
                };

                await App.ExpenseDatabase.SaveExpenseAsync(newExpense);

                ExpenseRecord expenseRecord = new ExpenseRecord()
                {
                    SplitPersonId = person.ID,
                    SplitExpenseId = newExpense.ID,
                    SplitAmount = double.Parse(amountEntry.Text)
                };

                await App.RecordDatabase.SaveRecordAsync(expenseRecord);

                titleEntry.Text = amountEntry.Text = string.Empty;
                await Navigation.PopToRootAsync(false);
            }
        }
    }
}
