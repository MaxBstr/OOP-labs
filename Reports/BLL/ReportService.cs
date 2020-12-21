using System.Collections.Generic;
using DAL;

namespace BLL
{
    public class ReportService : IReportService
    {
        private int _serviceDate = 1;
        private TeamLead _teamLead;
        private IDatabase _database;
        public TeamLead CreateTeamLead(string name)
        {
            _teamLead = new TeamLead(name);
            return _teamLead;
        }

        public ReportService(IDatabase db)
        {
            _database = db;
        }

        public Task CreateTask(string taskName, string taskDescription, IEmployee employee)
        {
            var dalTask = new DAL.Task(taskName, taskDescription, _serviceDate);
            dalTask.SetEmployeeName(employee.GetName());
            _database.CreateTask(dalTask);
            var bllTask = DalToBll(dalTask);

            return bllTask;
        }

        public IEnumerable<Task> GetAllTasks()
        {
            var dalTasks = _database.GetAll();
            var bllTasks = new List<Task>();

            foreach (var dalTask in dalTasks)
            {
                var bllTask = DalToBll(dalTask);
                bllTasks.Add(bllTask);
            }

            return bllTasks;
        }

        public Task GetTaskById(int id)
        {
            var dalTask = _database.GetTaskById(id);
            var bllTask = DalToBll(dalTask);
            return bllTask;
        }

        public void GetEmployeesTasksByDirector(string directorName)
        {
            var director = _teamLead.FindDirector(directorName);
            director.GetEmployeesTasks();
        }

        public Task GetTaskByEmployee(Employee employee)
        {
            var dalTask = _database.GetTaskByEmployee(employee.GetName());
            var bllTask = DalToBll(dalTask);
            return bllTask;
        }

        public IEnumerable<Task> GetActiveTasks()
        {
            var dalTasks = _database.GetActiveTasks();
            var bllTasks = new List<Task>();
            foreach (var dalTask in dalTasks)
            {
                var bllTask = DalToBll(dalTask);
                bllTasks.Add(bllTask);
            }

            return bllTasks;
        }

        private Task DalToBll(DAL.Task dalTask)
        {
            var dllTask = new Task(dalTask.TaskName, dalTask.TaskDescription, dalTask.CreationDate)
            {
                Id = dalTask.Id,
                status = (TaskStatus) dalTask.Status,
                TaskEmployee = _teamLead.FindEmployee(dalTask.EmployeeName),
                LastChangesDate = dalTask.LastChangesDate
            };

            foreach (var dalReport in dalTask.Reports)
            {
                dllTask._reports.Add(new Report {Changes = dalReport.Changes, Date = dalReport.Date});
            }

            return dllTask;
        }
    }
}