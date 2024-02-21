namespace Application.DTO.Request
{
    public class CompanyRequest
    {
        public required string Cuit { get; set; }
        public required string Name { get; set; }
        public required string Adress { get; set; }
        public required string Phone { get; set; }
    }
}
