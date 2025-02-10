using System.ComponentModel.DataAnnotations;

namespace Trip_Planner.Models.Expenses
{
    public class CreateExpenseModel
    {
        [Required]
        public string ExpenseName { get; set; }

        [Required]
        public decimal ExpenseAmount { get; set; }
    }
}
