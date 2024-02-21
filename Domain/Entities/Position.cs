
namespace Domain.Entities
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Hierarchy { get; set; }
        public decimal MaxAmount { get; set; }
        public int IdCompany { get; set; }
        public Company Company { get; set; }

        public IEnumerable<Employee> Employees { get; set; }
    }
}

