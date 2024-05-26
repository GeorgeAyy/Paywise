

namespace MyMvcApp.Models
{
    public class Report
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Expense> Expenses { get; set; }
    }
}
