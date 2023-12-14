using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("customer")]
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        [Column("customer_id")]
        public int CustomerId { get; set; }
        [Column("given_name")]
        [StringLength(255)]
        [Unicode(false)]
        public string GivenName { get; set; } = null!;
        [Column("family_name")]
        [StringLength(255)]
        [Unicode(false)]
        public string FamilyName { get; set; } = null!;
        [Column("email")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Email { get; set; }
        [Column("phone")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Phone { get; set; }
        [Column("address")]
        [StringLength(1024)]
        [Unicode(false)]
        public string? Address { get; set; }
        [Column("creator")]
        public int Creator { get; set; }
        [Column("date_created", TypeName = "datetime")]
        public DateTime DateCreated { get; set; }
        [Column("retired")]
        public bool Retired { get; set; }
        [Column("retired_by")]
        public int? RetiredBy { get; set; }
        [Column("date_retired", TypeName = "datetime")]
        public DateTime? DateRetired { get; set; }

        [InverseProperty("Customer")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
