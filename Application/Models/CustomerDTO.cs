using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class CustomerDTO
    {
        public int? CustomerId { get; set; }
        [Required]
        public string GivenName { get; set; } = null!;
        [Required]
        public string FamilyName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
