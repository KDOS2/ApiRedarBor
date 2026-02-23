namespace Application.CQRS.Query
{
    using Application.Dto;
    using Application.Exceptions;
    using AutoMapper;
    using Domain.Exceptions;
    using Domain.IRepository;
    using Infrastructure.Exceptions;
    using MediatR;

    public sealed class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeeDto>>
    {
        private readonly IEmployeeGetRepository _repository;
        private readonly IMapper _mapper;

        public GetAllEmployeesHandler(IEmployeeGetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> Handle(GetAllEmployeesQuery request,CancellationToken cancellationToken)
        {
            try
            {
                var employees = await _repository.GetAllAsync();
                var employeesResult = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
                return employeesResult;
            }            
            catch (DomainException) {throw;}
            catch (InfrastructureException){throw;}            
            catch (Exception) { throw new AppException("Error cargando registros."); }
        }
    }
}
