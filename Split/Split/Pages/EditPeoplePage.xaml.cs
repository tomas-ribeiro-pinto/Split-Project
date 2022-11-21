using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Page = Xamarin.Forms.Page;

namespace Split.Pages
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

            Person person = (Person)BindingContext;
            List<ExpenseRecord> records = await App.SplitDatabase.GetRecordList_byPerson(person.ID);

            List<Expense> expenses = new List<Expense>();

            foreach (var item in records)
            {
                expenses.Add(App.SplitDatabase.GetExpenseAsync(item.ExpenseID).Result);
            }

            expenseView.ItemsSource = records;
        }

        async void OnEdit(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var record = (ExpenseRecord)button.BindingContext;
            Expense currentSelection = App.SplitDatabase.GetExpenseAsync(record.ExpenseID).Result;

            Page EditExpensePage = new EditExpensePage
            {
                Title = "Edit Expense"
            };

            EditExpensePage.BindingContext = currentSelection;
            await Navigation.PushAsync(EditExpensePage);

        }
    }
}
