namespace Application.CQRS.Command
{
    using MediatR;

    public sealed record DeleteEmployeeCommand(long Id) : IRequest<Unit>;
}
