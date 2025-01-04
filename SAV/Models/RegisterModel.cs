namespace SAV.Models
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; } // Add phone field if necessary

    }
}
