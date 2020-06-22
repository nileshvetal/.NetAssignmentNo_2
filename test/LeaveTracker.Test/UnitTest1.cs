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
    }
}
