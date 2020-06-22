using System;
using Xunit;
using leavetracker;

namespace LeaveTracker.Test
{
    public class EmployeeTest
    {
        [Fact]
        public void TestName()
        {
            var employee = new Employee()
            {
                Id = 102
            };

            var actual = employee.Validate();
            var expected = true;

            Assert.Equal(expected, actual);
        }
    }
}
