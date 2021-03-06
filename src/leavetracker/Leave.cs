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

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }

        public override string ToString()
        {
            return ("ID: " + Id + "    Creator: " + Creator.Name +
                "   Manager: " + Manager.Name + "    Title:" +
                Title + "     Description:" + Description +
                "   Start-Date: " + StartDate.ToShortDateString() + "   End-Date: " + EndDate.ToShortDateString() +
                "   Status: " + Status);
        }

        public bool Create(Leave leave)
        {
            leave.Status = Status.Pending;
            var leaves = GetAll();
            leave.Id = leaves.Count + 1;
            leaves.Add(leave);
            return CsvFile.WriteLeave(leaves);
        }

        public static List<Leave> GetAll()
        {
            return CsvFile.ReadLeaves();
        }

        public static bool Update(Leave leave)
        {
            var leaves = GetAll();
            leaves.ForEach(lv =>
            {
                if (lv.Id == leave.Id)
                {
                    lv.Status = leave.Status;
                }
            });
            return CsvFile.WriteLeave(leaves);
        }

        public static List<Leave> GetPendingLeaves()
        {
            var leaves = GetAll();
            leaves = leaves.Where(lv => lv.Status == Status.Pending).ToList();

            return leaves;
        }

        public static List<Leave> GetByTitle(string searchString)
        {
            var leaves = GetAll();
            leaves = leaves.Where(leave => leave.Title.ToLower().Contains(searchString.ToLower().Trim())).ToList();
            return leaves;
        }

        public static List<Leave> GetByStatus(Status status)
        {
            var leaves = GetAll();
            leaves = leaves.Where(leave => leave.Status == status).ToList();
            return leaves;
        }
    }

}