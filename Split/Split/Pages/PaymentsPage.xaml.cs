using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Split.Data;
using Split.Pages;
using Xamarin.Forms;

namespace Split.Pages
{
    public partial class PaymentsPage : ContentPage
    {

        List<Person> people = new List<Person>();
        ObservableCollection<Payment> payments { get; set; }

        public PaymentsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            totalAmount.Text = "Total: " + string.Format("{0:0.00}", TripPage.Total) + "€";

            await GetPaymentListAsync();

            splitView.ItemsSource = payments;

        }

        private async Task GetPaymentListAsync()
        {
            payments = new ObservableCollection<Payment>();
            people = await App.SplitDatabase.GetPersonListAsync();
            double totalSplit = TripPage.Total / people.Count();
            foreach (Person person in people)
            {
                double totalDue = 0;
                List<ExpenseRecord> records = await App.SplitDatabase.GetRecordList_byPerson(person.ID, TripPage.bindingTrip.ID);
                double personTotal = Person.SumOfExpenses(records);
                totalDue = personTotal - totalSplit;

                payments.Add(new Payment { Person = person, AmountDue = totalDue, Total = personTotal });
            }

        }
    }
}

