using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Split
{
    public partial class EditExpensePage : ContentPage
    {
        public EditExpensePage()
        {
            InitializeComponent();
        }

        async void OnDelete(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var currentSelection = (Expense)button.BindingContext;
            bool answer = await DisplayAlert("Delete?", "Would you like to delete this expense?", "Yes", "No");

            if (answer)
            {
                await App.ExpenseDatabase.DeleteExpenseEntry(currentSelection);
                await Navigation.PopToRootAsync(true);
            }
        }

        void OnEditExpense(object sender, EventArgs e)
        {
            var EditButton = (Button)sender;
            EditButton.IsVisible = false;
            SaveButton.IsVisible = true;
            titleEntry.IsEnabled = true;
            amountEntry.IsEnabled = true;
        }

        async void OnSaveExpense(object sender, EventArgs e)
        {
            var SaveButton = (Button)sender;
            var currentSelection = (Expense)SaveButton.BindingContext;

            bool answer = await DisplayAlert("Save?", "Would you like to edit and save new details to this expense?", "Yes", "No");

            if (answer)
            {
                currentSelection.Title = titleEntry.Text;
                currentSelection.Amount = double.Parse(amountEntry.Text);
                await App.ExpenseDatabase.UpdateExpenseAsync(currentSelection);
                await Navigation.PopToRootAsync(false);
            }

        }
    }
}
