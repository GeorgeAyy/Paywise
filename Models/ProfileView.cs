

namespace MyMvcApp.Models
{
    public class ProfileView
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Expense> Expenses { get; set; }
    }
}
