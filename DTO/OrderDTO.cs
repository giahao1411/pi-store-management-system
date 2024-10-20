using System;

namespace DTO
{
    public class OrderDTO
    {
        public string id { get; set; }
        public string clientID { get; set; }
        public string employeeID { get; set; }
        public DateTime orderDate { get; set; }
        public double totalPrice { get; set; }
    }
}
