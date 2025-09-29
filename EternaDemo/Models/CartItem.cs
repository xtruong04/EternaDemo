using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EternaDemo.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        [Required] public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("Product")]
        [Required] public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string SelectedSize { get; set; }
        public string SelectedColor { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; }
    }
}