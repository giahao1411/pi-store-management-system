using System;

namespace DTO
{
    public class EmployeeDTO
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address {  get; set; }
        public double salary { get; set; }
        public DateTime hireDate { get; set; }
    }
}
