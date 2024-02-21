namespace Application.DTO.Request
{
    public class PositionRequest
    {
        public string Description { get; set; }
        public int Hierarchy { get; set; }
        public int MaxAmount { get; set; }
        public int CompanyId { get; set; }
    }
}
