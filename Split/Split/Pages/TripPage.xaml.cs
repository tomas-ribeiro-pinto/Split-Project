using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Split.Pages;
using Xamarin.Forms;

namespace Split.Pages
{
    public partial class TripPage : ContentPage
    {
        public double CurrentTotal;
        public static Trip bindingTrip;

        public TripPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            expenseView.ItemsSource = await App.SplitDatabase.GetExpenseListAsync();

            Trip trip = (Trip)BindingContext;
            bindingTrip = trip;
            currentTrip.Text = trip.Title;
            GetTotal();
        }

        async void GetTotal()
        {
            List<Expense> expenses = await App.SplitDatabase.GetExpenseListAsync();

            double total = 0;
            totalAmount.Text = String.Empty;

            foreach (Expense expense in expenses)
            {
                total += expense.Amount;
            }

            totalAmount.Text += "Total: " + string.Format("{0:0.00}", total) + "€";
        }

        async void OnEdit(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var currentSelection = (Expense)button.BindingContext;

            Page EditExpensePage = new EditExpensePage
            {
                Title = "Edit Expense"
            };

            EditExpensePage.BindingContext = currentSelection;
            await Navigation.PushAsync(EditExpensePage);

        }

        async void OnDelete(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var currentSelection = (Expense)button.BindingContext;
            bool answer = await DisplayAlert("Delete?", "Would you like to delete this expense?", "Yes", "No");

            if (answer)
            {
                await App.SplitDatabase.DeleteExpenseAsync(currentSelection);
                GetTotal();
            }

            expenseView.ItemsSource = await App.SplitDatabase.GetExpenseListAsync();
        }

        async void OnDeleteAll(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Delete All?", "Would you like to delete all the expenses?", "Yes", "No");

            if (answer)
            {
                await App.SplitDatabase.DeleteAllExpenseAsync();
                GetTotal();
            }

            expenseView.ItemsSource = await App.SplitDatabase.GetExpenseListAsync();
        }

        async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            Page AddExpensePage = new AddExpensePage
            {
                Title = "Add Expense"
            };
            await Navigation.PushAsync(AddExpensePage);
        }
    }
}

