namespace Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdCompany { get; set; }
        public Company Company { get; set; }

        public IEnumerable<Employee> Employees { get; set;}
 
    }
}
