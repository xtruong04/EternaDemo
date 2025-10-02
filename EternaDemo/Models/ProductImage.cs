using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EternaDemo.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        [Required] public string Url { get; set; }
        public string View360Url { get; set; } // link viewer 360
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}