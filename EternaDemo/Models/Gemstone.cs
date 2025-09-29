using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EternaDemo.Models
{
    public class Gemstone
    {
        [Key]
        public int Id { get; set; }
        [Required] public string Name { get; set; } // Diamond, Sapphire...
        public string Cut { get; set; }
        public string Clarity { get; set; }
        public string Color { get; set; }
        public decimal Carat { get; set; }
        public string Origin { get; set; }
        public string Treatment { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        // Một viên đá quý có thể có nhiều chứng nhận
        public virtual ICollection<Certificate> Certificates { get; set; }

    }
}
