namespace Application.CQRS.Command
{
    using MediatR;

    public sealed record UpdateEmployeeCommand(
        long Id,
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
    ) : IRequest<Unit>;
}
