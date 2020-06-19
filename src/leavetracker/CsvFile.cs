using System;
using System.Collections.Generic;
using System.IO;

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
                        var empId = int.Parse(coloms[0]);
                        var empName = coloms[1];
                        var empMngID = coloms.Length < 3 || string.IsNullOrWhiteSpace(coloms[2]) ? 0 :int.Parse(coloms[2]);
                        var employee = new Employee()
                        {
                            Id = int.Parse(coloms[0]),
                            Name = coloms[1],
                            ManagerId = coloms.Length < 3 || string.IsNullOrWhiteSpace(coloms[2]) ? 0 :int.Parse(coloms[2])
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
    }
}