namespace Domain.Entities
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string Cuit { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }

        public IEnumerable<Position> Positions { get; set; }
        public IEnumerable<Department> Departments { get; set; }
    }
}
