using System;
using System.Collections.ObjectModel;

namespace Split.Data
{
    public class Payment
    {
        public Person Person { get; set; }
        // Difference from the total of everyone
        // dividing by number of people in party
        public double AmountDue { get; set; }
        // Total spent on this trip
        public double Total { get; set; }
    }
}

