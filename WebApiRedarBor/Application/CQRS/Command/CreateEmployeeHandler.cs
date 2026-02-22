namespace Application.CQRS.Command
{
    using Application.Dto;
    using Application.Exceptions;
    using AutoMapper;
    using Domain.Entity;
    using Domain.Exceptions;
    using Domain.IRepository;
    using Infrastructure.Exceptions;
    using MediatR;

    public sealed class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
    {
        private readonly IEmployeeSetRepository _repository;
        private readonly IEmployeeGetRepository _getRepository;
        private readonly IMapper _mapper;

        public CreateEmployeeHandler(IEmployeeSetRepository repository, IEmployeeGetRepository getRepository, IMapper mapper)
        {
            _repository = repository;
            _getRepository = getRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getEmployee = await _getRepository.GetByUserNameAsync(command.Username??string.Empty);
                if (getEmployee != null)
                    throw new AppNotFoundException("Ya se encuentra registrado este userName.");

                getEmployee = await _getRepository.GetByEmailAsync(command.Email??string.Empty);
                if (getEmployee != null)
                    throw new AppNotFoundException("Ya se encuentra registrado este correo electronico.");

                var employee = new EmployeEntity(
                                                   command.CompanyId,
                                                   command.PortalId,
                                                   command.RoleId,
                                                   command.StatusId,
                                                   command.Username ?? string.Empty,
                                                   command.Email ?? string.Empty,
                                                   command.Password ?? string.Empty,
                                                   command.Name,
                                                   command.Fax,
                                                   command.Telephone
                                               );

                var createdEmployee = await _repository.AddAsync(employee);
                return _mapper.Map<EmployeeDto>(createdEmployee);
            }
            catch (DomainException) {throw;}
            catch (InfrastructureException){throw;}
            catch (AppNotFoundException) { throw; }
            catch (Exception) { throw new AppException("Error registrando nuevo empleado"); }
        }
    }
}
