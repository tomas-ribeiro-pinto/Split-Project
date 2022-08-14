using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Split
{
    public partial class MainPage : ContentPage
    {
        public double CurrentTotal;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            expenseView.ItemsSource = await App.ExpenseDatabase.GetExpenseAsync();

            currentTrip.Text = "Get to know your expenses!";
            GetTotal();
        }

        async void GetTotal()
        {
            List<Expense> expenses = await App.ExpenseDatabase.GetExpenseAsync();

            double total = 0;
            totalAmount.Text = String.Empty;

            foreach (Expense expense in expenses)
            {
                total += expense.Amount;
            }

            totalAmount.Text += "Total: " + string.Format("{0:0.00}", total) + "€";
        }

        /**
         * !Future improvement to save cost of running time: avoid count every expense's total from list
         * 
        async void UpdateTotal(Expense expense)
        {
            List<Expense> expenses = await App.ExpenseDatabase.GetExpenseAsync();

            double total = 0;
            var totalText = totalAmount.Text.Split(':')[1];
            var actualTotal = totalText.Split('€')[0];

            CurrentTotal += total;

            totalAmount.Text += "Total: " + string.Format("{0:0.00}", total) + "€";
        }
        **/

        async void OnDelete(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var currentSelection = (Expense)button.BindingContext;
            bool answer = await DisplayAlert("Delete?", "Would you like to delete this expense?", "Yes", "No");

            if (answer)
            {
                await App.ExpenseDatabase.DeleteExpenseEntry(currentSelection);
                GetTotal();
            }

            expenseView.ItemsSource = await App.ExpenseDatabase.GetExpenseAsync();
        }

        async void OnDeleteAll(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Delete All?", "Would you like to delete all the expenses?", "Yes", "No");

            if (answer)
            {
                await App.ExpenseDatabase.DeleteAllExpense();
                GetTotal();
            }

            expenseView.ItemsSource = await App.ExpenseDatabase.GetExpenseAsync();
        }

        async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            Page AddExpensePage = new AddExpensePage();
            AddExpensePage.Title = "Add Expense";
            //AddExpensePage.BackgroundColor = Color.Beige;
            await Navigation.PushAsync(AddExpensePage);
        }
    }
}

