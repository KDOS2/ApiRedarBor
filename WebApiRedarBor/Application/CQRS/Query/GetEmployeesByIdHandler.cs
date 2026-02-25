namespace Application.CQRS.Query
{
    using Application.Dto;
    using Application.Exceptions;
    using AutoMapper;
    using Domain.Exceptions;
    using Domain.IRepository;
    using Infrastructure.Exceptions;
    using MediatR;

    public sealed class GetEmployeesByIdHandler : IRequestHandler<GetEmployeesByIdQuery, EmployeeDto?>
    {
        private readonly IEmployeeGetRepository _repository;
        private readonly IMapper _mapper;

        public GetEmployeesByIdHandler(IEmployeeGetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EmployeeDto?> Handle(GetEmployeesByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var employees = await _repository.GetByIdAsync(request.Id);
                var employeesResult = employees != null ? _mapper.Map<EmployeeDto>(employees) : null;
                return employeesResult;
            }
            catch (DomainException) { throw; }
            catch (InfrastructureException) { throw; }
            catch (Exception) { throw new AppException("Error cargando registros."); }
        }
    }
}
