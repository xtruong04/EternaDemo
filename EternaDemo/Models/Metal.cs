using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EternaDemo.Models
{
    public class Metal
    {
        [Key]
        public int Id { get; set; }         // Khóa chính
        public string Name { get; set; }         // Tên kim loại (Gold, Platinum, Silver, Rose Gold)
        public string Purity { get; set; }       // Độ tinh khiết (14K, 18K, 24K, 950...)
        public decimal Price { get; set; } // Giá thị trường / gram (tùy chọn)

        // Quan hệ 1-n với ProductVariant
        public virtual ICollection<ProductVariant> ProductVariants { get; set; }
    }
}
