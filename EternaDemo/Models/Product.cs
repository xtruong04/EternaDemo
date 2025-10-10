using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;


namespace EternaDemo.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }
        
        [StringLength(300)]
        public string Alias { get; set; }
        public string Description { get; set; }
        public bool IsFeatured { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Price { get; set; }

        [ForeignKey("Metal")]
        public int MetalId { get; set; }
        public virtual Metal Metal { get; set; }

        [ForeignKey("Category")]
        public int CateId { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("Gemstone")]
        public int GemstoneId { get; set; }
        public virtual Gemstone Gemstone { get; set; }
        [ForeignKey("Certificate")]
        public int CertificateId { get; set; }   // khóa ngoại
        public virtual Certificate Certificate { get; set; }

        // Quan hệ
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}