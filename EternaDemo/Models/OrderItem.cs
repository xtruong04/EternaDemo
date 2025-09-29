using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EternaDemo.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Order")]
        [Required]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey("ProductVariant")]
        [Required]
        public int ProductVariantId { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal")]
        public decimal UnitPrice { get; set; }   // Giá tại thời điểm mua (đã fix, tránh bị thay đổi sau này)

        [Column(TypeName = "decimal")]
        public decimal Subtotal { get; set; }    // = UnitPrice * Quantity
    }
}