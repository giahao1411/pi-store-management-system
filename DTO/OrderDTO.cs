using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderDTO
    {
        public string id { get; set; }
        public string clientID { get; set; }
        public string employeeID { get; set; }
        public string orderDate { get; set; }
        public double totalPrice { get; set; }
    }
}
