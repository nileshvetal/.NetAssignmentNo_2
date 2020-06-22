using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace leavetracker
{
    class Program
    {
        static void Main(string[] args)
        {
            // var employee = Employee.GetById(100);
            // // ListofMyLeave(employee);


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
                Console.WriteLine();
                Console.WriteLine("1. Create Leave");
                Console.WriteLine("2. List my Leaves");
                Console.WriteLine("3. Update leaves");
                Console.WriteLine("4. Search Leave");
                Console.WriteLine("5. Sign Out");
                Console.WriteLine();
                Console.WriteLine("Enter your choice");

                int.TryParse(Console.ReadLine(), out choice);
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        CreateLeave(employee);
                        break;
                    case 2:
                        ListofMyLeave(employee);
                        break;
                    case 3:
                        UpdateLeaves(employee);
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
            int choice;
            Console.WriteLine();
            Console.WriteLine("Search leavey by:");
            Console.WriteLine("1. Title");
            Console.WriteLine("2. Satus(Pending/Approved/Rejected)");
            Console.WriteLine();
            Console.WriteLine("Enter your choice");
            int.TryParse(Console.ReadLine(), out choice);
            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    SearchByTitle();
                    break;

                case 2:
                    SearchByStatus();
                    break;

                default:
                    Console.WriteLine("Enter valid choice");
                    break;
            }

        }

        private static void SearchByStatus()
        {
            Console.WriteLine("Enter status to seacrh(Pending/Approved/Rejected)");
            var statusString = Console.ReadLine();
            var status = EnumStringMap.ToEnum(statusString);
            if (status == Status.Other)
            {
                Console.WriteLine("Enter valid status");
                return;
            }

            var leaves = Leave.GetByStatus(status);
            Console.WriteLine();
            if (leaves.Count < 1)
            {
                Console.WriteLine("Not Found any matching leave");
                return;
            }
            leaves.ForEach(leave =>
            {
                Console.WriteLine(leave);
            });
        }

        private static void SearchByTitle()
        {
            Console.WriteLine("Enter search string for title");
            var searchString = Console.ReadLine();
            var leaves = Leave.GetByTitle(searchString.Trim());
            Console.WriteLine();
            if (leaves.Count < 1)
            {
                Console.WriteLine("Not Found any matching leave");
                return;
            }
            leaves.ForEach(leave =>
            {
                Console.WriteLine(leave);
            });
        }

        private static void UpdateLeaves(Employee employee)
        {
            if (!employee.isManager())
            {
                Console.WriteLine("You do not have permission to update leaves.");
                return;
            }
            var leavesToUpdate = employee.GetLeavesToUpdate();
            if (leavesToUpdate.Count < 1)
            {
                Console.WriteLine("You do not have any pending leaves assing to you.");
                return;
            }
            Console.WriteLine();
            Console.WriteLine("List of pending leaves, Need to update status.");
            leavesToUpdate.ForEach(leave =>
            {
                Console.WriteLine(leave);
            });
            int leaveId;
            string status;
            Console.WriteLine();
            Console.WriteLine("Please enter Id of any leaves mentioned above to update status");
            int.TryParse(Console.ReadLine(), out leaveId);

            Console.WriteLine("Do you want to approve leave?(y/n)");
            do
            {
                status = Console.ReadLine();
            } while (string.IsNullOrEmpty(status.Trim()));

            if (!(status.ToLower().Equals("y") || status.ToLower().Equals("n")))
            {
                Console.WriteLine("Please type y to approve leave and n ro reject.");
                return;
            }

            var isApproved = status.ToLower().Equals("y") ? true : false;
            if (employee.UpdateLeave(leaveId, isApproved))
            {
                Console.WriteLine("Succesfully updated leave.");
            }
            else
            {
                Console.WriteLine("Please try again.");
            }

        }

        private static void ListofMyLeave(Employee employee)
        {
            var leaves = employee.GetMyLeaves();
            if (leaves.Count < 1)
            {
                Console.WriteLine("You did not created any leaves yet");
                return;
            }
            leaves.ForEach(leave =>
            {
                Console.WriteLine(leave);
            });
        }

        private static void CreateLeave(Employee employee)
        {
            var leave = new Leave();

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
            Console.WriteLine("Enter start date*(MM/DD/YYY)");
            var dateString = "";
            dateString = Console.ReadLine();
            var startDate = Date.GetFormatedDate(dateString);
            leave.StartDate = startDate;

            Console.WriteLine();
            Console.WriteLine("Enter end date*(MM/DD/YYY)");
            dateString = Console.ReadLine();
            var endDate = Date.GetFormatedDate(dateString);
            if (!Date.isValid(startDate, endDate))
            {
                Console.WriteLine("Enter valid end Date(Should be greater than or equal to start date)");
                CreateLeave(employee);
                return;
            }
            leave.EndDate = endDate;

            var manager = employee.ManagerId != 0 ? Employee.GetById(employee.ManagerId) : employee;
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
