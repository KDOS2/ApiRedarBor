namespace Application.CQRS.Command
{
    using Application.Dto;
    using MediatR;

    public sealed record CreateEmployeeCommand(
        long CompanyId,
        int PortalId,
        int RoleId,
        int StatusId,
        string? Username,
        string? Email,
        string? Password,
        string? Name = null,
        string? Fax = null,
        string? Telephone = null
    ) : IRequest<EmployeeDto>;
}
