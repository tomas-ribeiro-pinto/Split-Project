using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Split
{
    public partial class EditPeoplePage : ContentPage
    {

        public EditPeoplePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            People person = (People)BindingContext;
            List<ExpenseRecord> records = await App.RecordDatabase.GetRecordList_byPerson(person.ID);

            List<Expense> expenses = new List<Expense>();

            foreach (var item in records)
            {
                expenses.Add(App.ExpenseDatabase.GetItemAsync(item.SplitExpenseId).Result);
            }

            expenseView.ItemsSource = records;
            //expenseView.SetBinding(ItemsView.ItemsSourceProperty, "records");
        }

        async void OnEdit(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var record = (ExpenseRecord)button.BindingContext;
            Expense currentSelection = App.ExpenseDatabase.GetItemAsync(record.SplitExpenseId).Result;

            Page EditExpensePage = new EditExpensePage
            {
                Title = "Edit Expense"
            };

            EditExpensePage.BindingContext = currentSelection;
            await Navigation.PushAsync(EditExpensePage);

        }
    }
}
