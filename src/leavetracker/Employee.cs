using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leavetracker
{
    public class Employee : UserBase
    {
        public int ManagerId { get; set; }

        public bool Validate()
        {
            var isValid = false;
            var employees = GetAll();
            employees.ForEach(employee =>
            {
                if (employee.Id == Id)
                {
                    Name = employee.Name;
                    ManagerId = employee.ManagerId;
                    isValid = true;
                }
            });

            return isValid;
        }

        public static List<Employee> GetAll()
        {
            return CsvFile.ReadEmployes();
        }

        public static Employee GetById(int id)
        {
            var employees = GetAll();
            var employee = new Employee();
            employees.ForEach(empl =>
            {
                if (empl.Id == id)
                {
                    employee = empl;
                }
            });
            return employee;
        }

        public List<Leave> GetMyLeaves()
        {
            var leaves = CsvFile.ReadLeaves();
            leaves = leaves.Where(lv => lv.Creator.Name.ToLower() == Name.ToLower()).ToList();
            return leaves;
        }
    }
}