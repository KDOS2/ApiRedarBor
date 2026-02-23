namespace Application.CQRS.Query
{
    using Application.Dto;
    using MediatR;

    public sealed class GetAllEmployeesQuery : IRequest<IEnumerable<EmployeeDto>>{}
}
