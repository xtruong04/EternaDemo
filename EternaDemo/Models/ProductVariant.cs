using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EternaDemo.Models
{
    public class ProductVariant
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string VariantSKU { get; set; }
        [Column(TypeName = "decimal")]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        // Tham chiếu đến kim loại
        [ForeignKey("Metal")]
        public int MetalId { get; set; }
        public virtual Metal Metal { get; set; }
        [StringLength(20)]
        public string Size { get; set; }

        [StringLength(20)]
        public string Color { get; set; }

    }
}