namespace Application.CQRS.Command
{
    using MediatR;

    public sealed record SoftDeleteEmployeeCommand(long Id) : IRequest<Unit>;
}
