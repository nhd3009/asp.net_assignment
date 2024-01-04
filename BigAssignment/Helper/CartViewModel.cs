using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigAssignment.Helper
{
    [Serializable] 
    
    public class CartViewModel
    {

        public int ProductID { get; set; }
        public String Name { get; set; }
        public String Slug { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public String Image { get; set; }
    }
}
