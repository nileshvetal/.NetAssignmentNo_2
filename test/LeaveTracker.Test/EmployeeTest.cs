using System;
using Xunit;
using leavetracker;

namespace LeaveTracker.Test
{
    public class EmployeeTest
    {
        [Fact]
        public void ValidEmployeeTest()
        {
            var employee = new Employee()
            {
                Id = 102
            };

            var actual = employee.Validate();

            var expected = true;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InvalidEmployeeTest()
        {
            var employee = new Employee()
            {
                Id = 155
            };

            var actual = employee.Validate();
            var expected = false;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetByIdSucces()
        {
            var actual = Employee.GetById(102);
            var expected = new Employee()
            {
                Id = 102,
                Name = "Hazel Nutt",
                ManagerId = 100
            };

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.ManagerId, actual.ManagerId);
        }

        [Fact]
        public void GetByIdEmployeeNotFound()
        {
            var actual = Employee.GetById(155);
            var expected = new Employee();

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.ManagerId, actual.ManagerId);
        }

        [Fact]
        public void EmployeeIsManagerTest()
        {
            var employee = new Employee()
            {
                Id = 102
            };

            var actual = employee.isManager();

            var expected = true;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EmployeeIsNotManagerTest()
        {
            var employee = new Employee()
            {
                Id = 107
            };

            var actual = employee.isManager();

            var expected = false;
            Assert.Equal(expected, actual);
        }
    }
}
