namespace Application.CQRS.Command
{
    using Application.Exceptions;
    using AutoMapper;
    using Domain.Exceptions;
    using Domain.IRepository;
    using Infrastructure.Exceptions;
    using MediatR;

    public sealed class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
    {
        private readonly IEmployeeSetRepository _setRepository;
        private readonly IEmployeeGetRepository _getRepository;

        public UpdateEmployeeHandler(IEmployeeSetRepository setRepository, IEmployeeGetRepository getRepository, IMapper mapper)
        {
            _setRepository = setRepository;
            _getRepository = getRepository;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getEmployee = await _getRepository.GetByIdAsync(command.Id);
                if (getEmployee is null)
                    throw new AppNotFoundException("No se encontro registro.");

                var validateEmployee = await _getRepository.GetByUserNameAsync(command.Username ?? string.Empty, command.Id, true);
                if (validateEmployee != null)
                    throw new AppNotFoundException("Ya se encuentra registrado este userName.");

                validateEmployee = await _getRepository.GetByEmailAsync(command.Email ?? string.Empty, command.Id, true);
                if (validateEmployee != null)
                    throw new AppNotFoundException("Ya se encuentra registrado este correo electronico.");

                getEmployee.UpdateProfileData(command.Id, command.Username??string.Empty, command.Password??string.Empty, command.Email??string.Empty, command.RoleId, command.StatusId, command.Name, command.Fax, command.Telephone);
                await _setRepository.UpdateAsync(getEmployee);
                return Unit.Value;
            }
            catch (DomainException) { throw; }
            catch (InfrastructureException) { throw; }
            catch (AppNotFoundException) { throw; }            
            catch (Exception) { throw new AppException("Error actualizando empleado"); }
        }
    }
}