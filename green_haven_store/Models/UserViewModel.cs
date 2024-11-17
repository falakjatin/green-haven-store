namespace green_haven_store.Models
{
    public class UserViewModel
    {
        public int U_Id { get; set; }

        public required string U_Name { get; set; }

        public string? U_Email { get; set; }

        public DateTime U_Date { get; set; }
    }
}
