﻿using System;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Expense expense = (Expense)BindingContext;
            try
            {
                expensePerson.Text += expense.GetPersonName();
            }
            catch
            {
                expensePerson.Text += "undefined";
            }
        }


        async void OnDelete(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var currentSelection = (Expense)button.BindingContext;
            bool answer = await DisplayAlert("Delete?", "Would you like to delete this expense?", "Yes", "No");

            if (answer)
            {
                await App.SplitDatabase.DeleteExpenseAsync(currentSelection);
                await Navigation.PopAsync(true);
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
                await App.SplitDatabase.UpdateExpenseAsync(currentSelection);
                foreach (ExpenseRecord record in App.SplitDatabase.GetRecordList_byExpense(currentSelection.ID).Result)
                {
                    record.SplitAmount = double.Parse(amountEntry.Text);
                    record.Title = titleEntry.Text;
                    await App.SplitDatabase.UpdateRecordAsync(record);
                }
                await Navigation.PopAsync(false);
            }
        }
    }
}
