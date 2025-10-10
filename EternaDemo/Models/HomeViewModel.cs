using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EternaDemo.Models
{
    public class HomeViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> AllProducts { get; set; }
        public List<Product> FeaturedProducts { get; set; }
        public List<Product> NewArrival { get; set; }
    }
}