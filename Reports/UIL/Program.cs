using System;
using BLL;
using DAL;

namespace UIL
{
    internal static class Program
    {
        private static void Main()
        {
            IDatabase database = Database.Create();
            var reportService = new ReportService(database);
            
            var teamLead = reportService.CreateTeamLead("Max");
            var director = teamLead.ApplyDirector("Andrew");
            var employee = director.ApplyEmployee("Alex");
            var employee2 = director.ApplyEmployee("Henry");
            var employee3 = director.ApplyEmployee("Platon");

            var task1 = reportService.CreateTask("task1", "first task", employee);
            var task2 = reportService.CreateTask("task2", "second task", employee2);
            var task3 = reportService.CreateTask("task3", "third task", employee3);

            director.AddTaskToEmployee(employee, task1);
            director.AddTaskToEmployee(employee2, task2);
            director.AddTaskToEmployee(employee3, task3);
            
            employee.AddReport(1, "Hello");
            employee.AddReport(1, "My name is Max");
            employee.AddReport(2, "2 day");
            
            employee2.AddReport(1, "hi");
            employee3.AddReport(1, "bonjour");

            var rep = teamLead.MakeSprintReport();
            Console.WriteLine(rep.GetInfo());

            var tasks = reportService.GetAllTasks();
            foreach(var t in tasks)
                Console.WriteLine($"{t.taskName} {t.taskDescription} {t.status}");

            var task = reportService.GetTaskById(3);
            Console.WriteLine(task.taskName);
            
            reportService.GetEmployeesTasksByDirector("Andrew");

            var activeTasks = reportService.GetActiveTasks();
            foreach(var t in activeTasks)
                Console.WriteLine($"{t.taskName} {t.taskDescription} {t.status}");
        }
    }
}