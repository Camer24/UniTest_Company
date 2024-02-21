namespace Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? SuperiorId { get; set; }
        public Employee Superior { get; set; }

        public bool HistoryFlag { get; set; }
        public bool ApprovalsFlag { get; set; }
        public bool IsApprover { get; set; }

        public IEnumerable<Employee> Subordinates { get; set; }

        public int DepartamentId { get; set; }
        public Department Departament { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
