using System.Collections.Generic;
using DAL;

namespace BLL
{
    public enum TaskStatus
    {
        Open = 1,
        Active = 2,
        Resolved = 3
    }
    
    public class Task
    {
        public int Id { get; set; }
        public string taskName;
        public string taskDescription;
        public int CreationDate;
        public int LastChangesDate;
        public TaskStatus status;
        public List<Report> _reports = new List<Report>();
        public IEmployee TaskEmployee;
        private Database db = Database.Create();

        public Task(string taskName, string taskDescription, int todayDate)
        {
            this.taskName = taskName;
            this.taskDescription = taskDescription;
            this.CreationDate = todayDate;
            this.status = TaskStatus.Open;
        }

        public void SetEmployee(IEmployee employee)
        {
            TaskEmployee = employee;
        }

        public void AddReport(int date, string changes)
        {
            var report = _reports.Find(r => r.Date == date);
            // если не было записи, создаем новую
            if (report == null)
            {
                status = TaskStatus.Active;
                var newReport = new Report(date, changes);
                _reports.Add(newReport);
            }
            // если была запись, дополяем её
            else
            {
                report.AddChanges(changes);
            }
            LastChangesDate = date;
            db.Update(BllToDal());
        }

        public Report MakeSprintReport()
        {
            var sprintReport = new Report();
            sprintReport.AddChanges($"{TaskEmployee.GetName()}`s sprint report:\n");
            sprintReport.AddChanges($"Status: {status}");
            var i = 1;
            foreach (var rep in _reports)
            {
                sprintReport.AddChanges($"{i} day report");
                sprintReport.AddChanges(rep.GetInfo());
                i++;
            }
            return sprintReport;
        }

        public void EndTask()
        {
            status = TaskStatus.Resolved;
            AddReport(LastChangesDate, "Task completed!");
        }

        private DAL.Task BllToDal()
        {
            var dalTask = new DAL.Task(taskName, taskDescription, CreationDate)
            {
                Id = Id,
                Status = (DAL.TaskStatus) status,
                EmployeeName = TaskEmployee.GetName(),
                LastChangesDate = LastChangesDate
            };

            foreach (var rep in _reports)
            {
                dalTask.Reports.Add(new DAL.Report {Changes = rep.Changes, Date = rep.Date});
            }

            return dalTask;
        }
    }
}