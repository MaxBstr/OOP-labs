namespace BLL
{
    public class Report
    {
        public int Date;
        public string Changes;

        public Report(int todayDate, string changes)
        {
            this.Date = todayDate;
            this.Changes = changes + '\n';
        }
        
        public Report() {}

        public void AddChanges(string changes)
        {
            Changes += changes + '\n';
        }

        public string GetInfo()
        {
            return Changes;
        }
    }
}