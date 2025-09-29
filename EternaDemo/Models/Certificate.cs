using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EternaDemo.Models
{
    public class Certificate
    {
        [Key]
        public int Id { get; set; }
        [Required] public string Type { get; set; } // GIA, AGS, Appraisal
        [Required] public string Number { get; set; }
        public DateTime IssueDate { get; set; }
        public string FileUrl { get; set; } // đường dẫn PDF ảnh scan
        [ForeignKey("Gemstone")]
        public int GemstoneId { get; set; }
        public virtual Gemstone Gemstone { get; set; }
    }
}
