
namespace DMYTest.Data.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public int UserID { get; set; }
        public string EmployeeName { get; set; }
        public string Job { get; set; }
        public int Salary { get; set; }
        public virtual User User { get; set; }
    }
}
