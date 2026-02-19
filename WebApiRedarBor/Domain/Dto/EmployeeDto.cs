namespace Domain.Dto
{
    public class EmployeeDto
    {
        public int id { get; set; }
        public long companyId { get; set; }
        public int portalId { get; set; }
        public int roleId { get; set; }
        public int statusId { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string? name { get; set; } = null;
        public string? fax { get; set; } = null;
        public string? telephone { get; set; } = null;        
    }
}
