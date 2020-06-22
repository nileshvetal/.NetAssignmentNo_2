using System;
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
            var leaves = Leave.GetAll();
            leaves = leaves.Where(lv => lv.Creator.Name.ToLower() == Name.ToLower()).ToList();
            return leaves;
        }

        public bool isManager()
        {
            var employees = GetAll();
            employees = employees.Where(empl => empl.ManagerId == Id).ToList();
            return employees.Count > 0 ? true : false;
        }

        public List<Leave> GetLeavesToUpdate()
        {
            var leaves = Leave.GetPendingLeaves();
            leaves = leaves.Where(lv => lv.Manager.Name.ToLower() == Name.ToLower()).ToList();
            return leaves;
        }

        public bool UpdateLeave(int leaveId, bool isApproved)
        {
            var leaves = GetLeavesToUpdate();
            Leave updatedLeave = null;
            leaves.ForEach(lv =>
            {
                if (lv.Id == leaveId)
                {
                    updatedLeave = lv;
                }
            });

            if (updatedLeave == null)
            {
                Console.WriteLine("Please enter valid Leave Id");
                return false;
            }

            updatedLeave.Status = isApproved ? Status.Approved : Status.Rejected;

            return Leave.Update(updatedLeave);
        }


    }
}