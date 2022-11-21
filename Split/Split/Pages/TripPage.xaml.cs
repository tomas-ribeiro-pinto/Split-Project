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
        public static double Total;
        public static Trip bindingTrip;
        public List<Expense> expenses;

        public TripPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Trip trip = (Trip)BindingContext;
            bindingTrip = trip;
            currentTrip.Text = trip.Title;

            expenses = await App.SplitDatabase.GetExpenseList_byTrip(bindingTrip.ID);
            expenseView.ItemsSource = expenses;
            if (expenses.Count() == 0)
            {
                paymentsButton.IsEnabled = false;
                paymentsButton.BackgroundColor = Color.LightGray;
            }
            else
            {
                paymentsButton.IsEnabled = true;
                paymentsButton.BackgroundColor = Color.OrangeRed;
            }

            GetTotal();
        }

        void GetTotal()
        {
            Total = Expense.SumOfExpenses(expenses);

            totalAmount.Text = "Total: " + string.Format("{0:0.00}", Total) + "€";
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

        async void OnDeleteAll(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Delete All?", "Would you like to delete all the expenses?", "Yes", "No");

            if (answer)
            {
                App.SplitDatabase.DeleteAllExpenseAsync(bindingTrip.ID);
                OnAppearing();
            }
        }

        async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            Page AddExpensePage = new AddExpensePage
            {
                Title = "Add Expense"
            };
            await Navigation.PushAsync(AddExpensePage);
        }

        async void OnPaymentsClicked(object sender, EventArgs e)
        {
            Page PaymentsPage = new PaymentsPage
            {
                Title = $"Payments due on trip to {bindingTrip.Title}"
            };
            await Navigation.PushAsync(PaymentsPage);
        }
    }
}

