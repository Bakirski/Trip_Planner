namespace Trip_Planner.Models.Expenses
{
    public class Expense
    {
        public int Id { get; set; }
        public string expenseName { get; set; }
        public decimal expenseAmount { get; set; }
        public int TripId { get; set; }
    }
}
