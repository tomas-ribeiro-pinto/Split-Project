using SQLite;

namespace Split
{
    public class Expense
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; }
        public double Amount { get; set; }
        public int SplitPercentage { get; set; }
    }
}
