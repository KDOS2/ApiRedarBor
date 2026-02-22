namespace Application.CQRS.Command
{
    using Application.Exceptions;
    using Domain.Exceptions;
    using Domain.IRepository;
    using Infrastructure.Exceptions;
    using MediatR;

    public sealed class SoftDeleteEmployeeHandler : IRequestHandler<SoftDeleteEmployeeCommand, Unit>
    {
        private readonly IEmployeeGetRepository _getRepository;
        private readonly IEmployeeSetRepository _setRepository;

        public SoftDeleteEmployeeHandler(IEmployeeGetRepository getRepository, IEmployeeSetRepository setRepository)
        {
            _getRepository = getRepository;
            _setRepository = setRepository;
        }

        public async Task<Unit> Handle(SoftDeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _getRepository.GetByIdAsync(request.Id);

                if (employee is null)
                    throw new AppNotFoundException("Empleado no encontrado.");

                employee.SoftDelete();
                await _setRepository.UpdateAsync(employee);
                
                return Unit.Value;
            }
            catch (DomainException) { throw; }
            catch (InfrastructureException) { throw; }
            catch (AppNotFoundException) { throw; }
            catch (Exception) { throw new AppException("Error eliminando empleado"); }
        }
    }
}
