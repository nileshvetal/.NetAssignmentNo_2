using System;
using System.Collections.Generic;

namespace leavetracker
{
    class Program
    {
        static void Main(string[] args)
        {
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
            throw new NotImplementedException();
        }
    }
}
