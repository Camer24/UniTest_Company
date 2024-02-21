namespace Application.DTO.Response
{
    public class EmployeeResponse
    {
        public int Id { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public int PositionId { get; set; }
        public int? SuperiorId { get; set; }
        public int DepartmentId { get; set; }
        public int CompanyId { get; set; }
        public bool IsApprover { get; set; }
    }
}
