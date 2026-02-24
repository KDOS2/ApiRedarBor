namespace Application.CQRS.Command
{
    using Application.Dto;
    using MediatR;

    public record LoginCommand(string Username, string Password): IRequest<AuthResponse>;
}
