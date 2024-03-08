namespace EmployeeAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }
        public DateTime JoinedDate {  get; set; }
    }
}
