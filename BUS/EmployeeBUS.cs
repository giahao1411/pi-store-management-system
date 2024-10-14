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

        public List<EmployeeDTO> getList()
        {
            return employeeDAL.getEmployyeeList();
        }

        public bool addEmployee(EmployeeDTO employeeDTO)
        {
            return employeeDAL.insert(employeeDTO);
        }

        public bool updateEmployee(EmployeeDTO employeeDTO)
        {
            return employeeDAL.update(employeeDTO);
        }

        public bool deleteEmployee(EmployeeDTO employeeDTO)
        {
            return employeeDAL.delete(employeeDTO);
        }
    }
}
