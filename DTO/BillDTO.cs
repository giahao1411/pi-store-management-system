using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BillDTO
    {
        public string id { get; set; }
        public string orderID { get; set; }
        public string clientID { get; set; }
        public string employeeID { get; set; }  
        public DateTime billDate { get; set; }
        public double totalPrice { get; set; }
    }
}
