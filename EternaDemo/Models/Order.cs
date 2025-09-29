using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EternaDemo.Models
{
    public class Order
    {
        #region Status
        public enum OrderStatus { Pending, Processing, Shipped, Delivered, CancelRequested, Cancelled, Refunded }
        public enum PaymentState { Unpaid, Paid, Refunded }
        #endregion
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("User")]
        public virtual ApplicationUser User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PaidAt { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public PaymentState PaymentStatus { get; set; } = PaymentState.Unpaid;

        [StringLength(50)]
        public string PaymentMethod { get; set; }
        [StringLength(100)]
        public string TransactionId { get; set; }
        [StringLength(50)]
        public string PaymentProvider { get; set; }

        public int? ShippingAddressId { get; set; }
        [ForeignKey("ShippingAddress")]
        public virtual Address ShippingAddress { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Subtotal { get; set; }

        [Column(TypeName = "decimal")]
        public decimal ShippingFee { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Tax { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Total { get; set; }

        [StringLength(500)]
        public string GiftMessage { get; set; }
        public bool GiftWrap { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; }
    }
}