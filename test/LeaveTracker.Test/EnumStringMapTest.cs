using System;
using Xunit;
using leavetracker;

namespace LeaveTracker.Test
{
    public class EnumStringMapTest
    {
        [Fact]
        public void PendingStatusStringToEnumTest()
        {
            var statusString = Constants.Pending;

            var actual = EnumStringMap.ToEnum(statusString);
            var expected = Status.Pending;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ApprovedStatusStringToEnumTest()
        {
            var statusString = Constants.Approved;

            var actual = EnumStringMap.ToEnum(statusString);
            var expected = Status.Approved;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RejectedStatusStringToEnumTest()
        {
            var statusString = Constants.Rejected;

            var actual = EnumStringMap.ToEnum(statusString);
            var expected = Status.Rejected;

            Assert.Equal(expected, actual);
        }

        

        [Fact]
        public void OtherStatusStringToEnumTest()
        {
            var statusString = "Anything else";

            var actual = EnumStringMap.ToEnum(statusString);
            var expected = Status.Other;

            Assert.Equal(expected, actual);
        }
    }
}