using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leavetracker
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

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
                    isValid = true;
                }
            });

            return isValid;
        }

        public static List<Employee> GetAll()
        {
            return CsvFile.ReadEmployes();
        }

        public IEnumerable<Employee> GetById(int id)
        {
            var employees = GetAll();
            var employee = employees.Where(empl => empl.Id == id);

            return employee;
        }
    }
}