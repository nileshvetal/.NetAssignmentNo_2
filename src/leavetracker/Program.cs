using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace leavetracker
{
    class Program
    {
        static void Main(string[] args)
        {
            // var employee = Employee.GetById(102);
            // var manager = Employee.GetById(employee.ManagerId);
            
            LeaveTrackerApllication();
        }

        private static void LeaveTrackerApllication()
        {
            int option;
            Console.WriteLine("Leave Tracker Application.");
            do
            {
                Console.WriteLine();
                Console.WriteLine("Select option");
                Console.WriteLine("1. Sign in");
                Console.WriteLine("2. Exit");
                int.TryParse(Console.ReadLine(), out option);

                if (option == 1)
                {
                    var employeeId = 0;
                    Console.WriteLine();
                    Console.WriteLine("Enter your Employee Id:");
                    int.TryParse(Console.ReadLine(), out employeeId);

                    var employee = new Employee() { Id = employeeId };

                    if (employee.Validate())
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Welcome {employee.Name}!, You are Succesfully Signed in to Leave Tracker Application");
                        GetMenu(employee);
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please Enter valid Id");
                    }
                }
                else
                {
                    if (option != 2)
                    {
                        Console.WriteLine("Enter valid option");
                    }
                }
            } while (option != 2);
        }

        private static void GetMenu(Employee employee)
        {
            int choice;
            do
            {
                Console.WriteLine("Enter your choice");
                Console.WriteLine();
                Console.WriteLine("1. Create Leave");
                Console.WriteLine("2. List my Leaves");
                Console.WriteLine("3. Update leaves");
                Console.WriteLine("4. Search Leave");
                Console.WriteLine("5. Sign Out");
                Console.WriteLine();

                int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        CreateLeave(employee);
                        break;
                    case 2:
                        ListofMyLeave();
                        break;
                    case 3:
                        UpdateLeaves();
                        break;
                    case 4:
                        SearchLeave();
                        break;
                    default:
                        break;
                }
            } while (choice != 5);

        }

        private static void SearchLeave()
        {
            throw new NotImplementedException();
        }

        private static void UpdateLeaves()
        {
            throw new NotImplementedException();
        }

        private static void ListofMyLeave()
        {
            throw new NotImplementedException();
        }

        private static void CreateLeave(Employee employee)
        {
            var leave = new Leave();
            var regex = new Regex(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");

            Console.WriteLine();
            Console.WriteLine("Enter Title*: ");
            do
            {
                leave.Title = Console.ReadLine();
            } while (string.IsNullOrEmpty(leave.Title.Trim()));

            Console.WriteLine();
            Console.WriteLine("Enter Description*:");
            do
            {
                leave.Description = Console.ReadLine();
            } while (string.IsNullOrEmpty(leave.Description.Trim()));

            Console.WriteLine();
            Console.WriteLine("Enter start date(DD/MM/YYY)");
            var datestring = "";
            do
            {
                datestring = Console.ReadLine();
            } while (!regex.IsMatch(datestring.Trim()));
            leave.StartDate = datestring;

            Console.WriteLine();
            Console.WriteLine("Enter end date(DD/MM/YYY)");
            do
            {
                datestring = Console.ReadLine();
            } while (!regex.IsMatch(datestring.Trim()));
            leave.EndDate = datestring;

            var manager = Employee.GetById(employee.ManagerId);
            leave.Creator = newUserBase(employee);
            leave.Manager = newUserBase(manager);

            if (leave.Create(leave))
            {
                Console.WriteLine("Succesfully created leave");
            }
            else
            {
                Console.WriteLine("Please try again");
            }
        }

        private static UserBase newUserBase(Employee employee)
        {
            var usrBase = new UserBase()
            {
                Id = employee.Id,
                Name = employee.Name
            };

            return usrBase;
        }
    }
}
