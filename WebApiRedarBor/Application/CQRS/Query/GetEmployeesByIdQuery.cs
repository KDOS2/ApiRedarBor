namespace Application.CQRS.Query
{
    using Application.Dto;
    using MediatR;

    public sealed class GetEmployeesByIdQuery : IRequest<EmployeeDto?>
    {
        public long Id { get; }

        public GetEmployeesByIdQuery(long id)
        {
            Id = id;
        }
    }
}
