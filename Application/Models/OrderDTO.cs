using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class OrderDTO
    {
        public int? OrderId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        [Required]
        public bool Paid { get; set; }
        public CustomerDTO? Customer { get; set; }
    }
}
