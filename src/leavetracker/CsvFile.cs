using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace leavetracker
{
    class CsvFile
    {
        public static List<Employee> ReadEmployes()
        {
            var employees = new List<Employee>();
            if (!Directory.Exists(Constants.EmployeeCsvPath))
            {
                Console.WriteLine("Please provide correct path");
                return employees;
            }

            var csvFiles = Directory.GetFiles(Constants.EmployeeCsvPath, "*.csv", SearchOption.TopDirectoryOnly);
            var line = "";
            try
            {
                using (var sr = new StreamReader(csvFiles[0]))
                {
                    var header = sr.ReadLine(); // To ignore headers in csv File.
                    while ((line = sr.ReadLine()) != null)
                    {
                        var coloms = line.Split(",");
                        var employee = new Employee()
                        {
                            Id = int.Parse(coloms[0]),
                            Name = coloms[1],
                            ManagerId = coloms.Length < 3 || string.IsNullOrWhiteSpace(coloms[2]) ? 0 : int.Parse(coloms[2])
                        };
                        employees.Add(employee);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return employees;
        }

        public static List<Leave> ReadLeaves()
        {
            var leaves = new List<Leave>();
            if (!Directory.Exists(Constants.LeaveCsvPath))
            {
                Console.WriteLine("Please provide correct path");
                return leaves;
            }

            var csvFiles = Directory.GetFiles(Constants.LeaveCsvPath, "*.csv", SearchOption.TopDirectoryOnly);
            if (csvFiles.Length < 1)
            {
                return leaves;
            }
            var line = "";
            try
            {
                using (var sr = new StreamReader(csvFiles[0]))
                {
                    var header = sr.ReadLine(); // To ignore headers in csv File.
                    while ((line = sr.ReadLine()) != null)
                    {
                        var coloms = line.Split(",");

                        var leave = new Leave()
                        {
                            Id = int.Parse(coloms[0]),
                            Creator = new UserBase { Name = coloms[1] },
                            Manager = new UserBase { Name = coloms[2] },
                            Title = coloms[3],
                            Description = coloms[4],
                            StartDate = coloms[5],
                            EndDate = coloms[6],
                            Status = EnumStringMap.ToEnum(coloms[7])
                        };
                        leaves.Add(leave);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return leaves;
        }

        internal static bool WriteLeave(List<Leave> leaves)
        {
            leaves = leaves.OrderBy(lv => lv.Id).ToList();

            DeleteLeaveFiles();
            try
            {
                using (var writer = File.AppendText(Constants.LeaveCsvFile))  // For colum Headers
                {
                    writer.WriteLine(string.Concat("Id", ",", "Creator", ",", "Manager", ",",
                                     "Title", ",", "Description", ",", "Start-Date", ",", "End-Date", ",", "Status"));
                }

                foreach (var lv in leaves)
                {
                    using (var writer = File.AppendText(Constants.LeaveCsvFile))
                    {
                        writer.WriteLine(lv.Id + "," + lv.Creator.Name + "," + lv.Manager.Name + ","
                                    + lv.Title + "," + lv.Description + "," +
                                    lv.StartDate + "," +
                                    lv.EndDate + "," +
                                    EnumStringMap.ToString(lv.Status));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        private static void DeleteLeaveFiles()
        {
            var csvFiles = Directory.GetFiles(Constants.LeaveCsvPath, "*.csv", SearchOption.TopDirectoryOnly);

            foreach (var file in csvFiles)
            {
                File.Delete(file);
            }
        }
    }
}