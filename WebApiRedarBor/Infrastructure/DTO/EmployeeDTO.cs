namespace Infrastructure.DTO
{
    public class EmployeeDTO
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public int PortalId { get; set; }
        public long RoleId { get; set; }
        public int StatusId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? Fax { get; set; }
        public string? Telephone { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? LastLogin { get; set; }

    }
}
