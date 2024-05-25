namespace MyMvcApp.ViewModels
{
    public class ExpenseViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string CategoryName { get; set; }
    }
}
