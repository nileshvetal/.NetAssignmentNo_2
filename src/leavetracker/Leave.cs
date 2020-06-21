using System;
using System.ComponentModel.DataAnnotations;

namespace leavetracker
{
    public class Leave
    {
        public int Id { get; set; }

        public UserBase Creator { get; set; }

        public UserBase Manager { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }

        public bool Create(Leave leave)
        {
            leave.Status = Status.Pending;
            return CsvFile.WriteLeave(leave);
        }
    }

}