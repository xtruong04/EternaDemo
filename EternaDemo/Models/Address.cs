using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EternaDemo.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required] public string Street { get; set; }
        [Required] public string City { get; set; }
        [Required] public string ZipCode { get; set; }
        [Required] public string Country { get; set; }
        public string Phone { get; set; }
        [ForeignKey("User")]
        [Required] public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}