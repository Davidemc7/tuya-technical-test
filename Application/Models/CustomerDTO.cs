namespace Application.Models
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string GivenName { get; set; } = null!;
        public string FamilyName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
