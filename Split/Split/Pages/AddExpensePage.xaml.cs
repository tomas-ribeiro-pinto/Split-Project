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
            peopleEntry.ItemsSource = await App.SplitDatabase.GetPersonListAsync();
            splitPeopleEntry.ItemsSource = await App.SplitDatabase.GetPersonListAsync();
            try
            {
                expensePerson.Text = splitPeopleEntry.SelectedItem.ToString();
                expensePersonAmount.Text = amountEntry.Text;
            }
            catch
            {
                expensePersonAmount.Text = "0.00";
            }
            dateEntry.MaximumDate = DateTime.Now;
        }

        async void AddExpense(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(titleEntry.Text) && !string.IsNullOrWhiteSpace(amountEntry.Text)
                && peopleEntry.SelectedItem != null)
            {
                // create DateTime() from selected date on picker
                Person person = (Person)peopleEntry.SelectedItem;

                Expense newExpense = new Expense()
                {
                    TripID = Pages.TripPage.bindingTrip.ID,
                    Title = titleEntry.Text,
                    Amount = double.Parse(amountEntry.Text),
                    DateExpense = dateEntry.Date,
                    DateCreated = DateTime.Now,
                    PersonID = person.ID
                };

                await App.SplitDatabase.SaveExpenseAsync(newExpense);

                ExpenseRecord expenseRecord = new ExpenseRecord()
                {
                    TripID = newExpense.TripID,
                    PersonID = person.ID,
                    ExpenseID = newExpense.ID,
                    SplitAmount = double.Parse(amountEntry.Text),
                    Title = titleEntry.Text
                };

                await App.SplitDatabase.SaveRecordAsync(expenseRecord);

                titleEntry.Text = amountEntry.Text = string.Empty;
                await Navigation.PopAsync(false);
                
            }
        }
    }
}
