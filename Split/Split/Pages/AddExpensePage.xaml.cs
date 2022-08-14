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

        async void AddExpense(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(titleEntry.Text) && !string.IsNullOrWhiteSpace(amountEntry.Text))
            {
                Expense newExpense = new Expense
                {
                    Title = titleEntry.Text,
                    Amount = double.Parse(amountEntry.Text)
                };

                await App.ExpenseDatabase.SaveExpenseAsync(newExpense);
                titleEntry.Text = amountEntry.Text = string.Empty;
                await Navigation.PopToRootAsync(false);
            }
        }
    }
}
