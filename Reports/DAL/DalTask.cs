using System.Collections.Generic;
namespace DAL
{
    public enum TaskStatus
    {
        Open = 1,
        Active = 2,
        Resolved = 3
    }
    public class Task
    {
        public int Id;
        public readonly string TaskName;
        public readonly string TaskDescription;
        public readonly int CreationDate;
        public int LastChangesDate;
        public readonly List<Report> Reports = new List<Report>();
        public string EmployeeName;
        public TaskStatus Status = TaskStatus.Open;

        public Task(string taskName, string taskDescription, int todayDate)
        {
            this.TaskName = taskName;
            this.TaskDescription = taskDescription;
            this.CreationDate = todayDate;
        }

        public void SetEmployeeName(string name)
        {
            EmployeeName = name;
        }
    }
}