using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderItemDTO
    {
        public string id { get; set; }
        public string orderID { get; set; } 
        public string productID { get; set; }
        public int quantity { get; set; }   
    }
}
