using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("order")]
    public partial class Order
    {
        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }
        [Column("customer_id")]
        public int CustomerId { get; set; }
        [Column("order_date", TypeName = "datetime")]
        public DateTime OrderDate { get; set; }
        [Column("total_amount", TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; }
        [Column("paid")]
        public bool Paid { get; set; }
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

        [ForeignKey("CustomerId")]
        [InverseProperty("Orders")]
        public virtual Customer Customer { get; set; } = null!;
    }
}
