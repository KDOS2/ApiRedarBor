namespace Application.Dto
{
    /*public class EmployeeDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string? Name { get; set; } = null;
        public string? Fax { get; set; } = null;
        public string? Telephone { get; set; } = null;
    }*/

    public record EmployeeDto(
                                string Username,
                                string? Name,
                                string? Fax,
                                string? Telephone,
                                string Email
                            );
}
