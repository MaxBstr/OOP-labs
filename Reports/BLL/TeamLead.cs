using System;
using System.Collections.Generic;

namespace BLL
{
    public class TeamLead : IEmployee
    {
        private string Name;
        public List<EmployeeDirector> Employees = new List<EmployeeDirector>();

        public TeamLead(string name)
        {
            Name = name;
        }

        public EmployeeDirector ApplyDirector(string name)
        {
            var newDirector = new EmployeeDirector(name);
            Employees.Add(newDirector);
            return newDirector;
        }
        public Report MakeSprintReport()
        {
            var sprintReport = new Report();
            sprintReport.AddChanges($"TeamLead {Name} created final report:");
            
            foreach (var emp in Employees)
            {
                var empReport = emp.MakeSprintReport();
                sprintReport.AddChanges(empReport.GetInfo());
            }

            return sprintReport;
        }

        public string GetName()
        {
            return Name;
        }

        public EmployeeDirector SearchDirector(string name)
        {
            var director =
                (EmployeeDirector) Employees.Find(
                    em => em.GetType() == typeof(EmployeeDirector) && em.GetName() == name);
            if (director == null)
                throw new Exception($"Can't apply employee. Director {name} does not exist!");
            
            return director;
        }

        public Employee FindEmployee(string name)
        {
            foreach (var emp in Employees)
            {
                var employee = emp.FindEmployee(name);
                if (employee != null)
                    return employee;
            }
            throw new Exception($"Employee {name} does not exist");
        }

        public EmployeeDirector FindDirector(string name)
        {
            var director = Employees.Find(em => em.GetName() == name);
            if (director == null)
                throw new Exception($"Director {name} does not exist!");
            return director;
        }
    }
}