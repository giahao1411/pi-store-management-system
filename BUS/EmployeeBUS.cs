using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BUS
{
    public class EmployeeBUS
    {
        private EmployeeDAL employeeDAL = new EmployeeDAL();

        public int getNumOfEmployee()
        {
            return employeeDAL.countEmployee();
        }
    }
}
