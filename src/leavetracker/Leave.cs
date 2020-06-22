using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        public override string ToString()
        {
            return ("ID: " + Id + "    Creator: " + Creator.Name +
                "   Manager: " + Manager.Name + "    Title:" +
                Title + "     Description:" + Description +
                "   Start-Date: " + StartDate + "   End-Date: " + EndDate +
                "   Status: " + Status);
        }

    }

}