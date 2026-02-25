namespace WebApiRedarBor.Tests.Application.CQRS.Query
{
    using AutoMapper;
    using Domain.Entity;
    using Domain.Exceptions;
    using Domain.IRepository;
    using global::Application.CQRS.Query;
    using global::Application.Dto;
    using global::Application.Exceptions;
    using Infrastructure.Exceptions;
    using Moq;
    using System.Threading;
    using Xunit;

    public class GetEmployeesByIdHandlerTests
    {
        private readonly Mock<IEmployeeGetRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly GetEmployeesByIdHandler _handler;

        public GetEmployeesByIdHandlerTests()
        {
            _repositoryMock = new Mock<IEmployeeGetRepository>();
            _mapperMock = new Mock<IMapper>();

            _handler = new GetEmployeesByIdHandler(
                _repositoryMock.Object,
                _mapperMock.Object
            );
        }

        //ID válido retorna EmployeeDto
        [Fact]
        public async Task Handle_ValidId_ReturnsEmployeeDto()
        {
            var query = new GetEmployeesByIdQuery(1L);

            var employeeEntity = new EmployeEntity(
                companyId: 1, portalId: 1, roleId: 1, statusId: 1,
                username: "testuser", email: "test@example.com",
                password: "testPassword", name: "Test User"
            );
            employeeEntity.SetId(1L);

            var expectedDto = new EmployeeDto(
                Username: "testuser",
                Name: "Test User",
                Fax: null,
                Telephone: null,
                Email: "test@example.com"
            );

            
            _repositoryMock.Setup(r => r.GetByIdAsync(query.Id)).ReturnsAsync(employeeEntity);
            _mapperMock.Setup(m => m.Map<EmployeeDto>(employeeEntity)).Returns(expectedDto);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(expectedDto.Username, result.Username);
            Assert.Equal(expectedDto.Email, result.Email);
            Assert.Equal(expectedDto.Name, result.Name);

            _repositoryMock.Verify(r => r.GetByIdAsync(query.Id), Times.Once);
            _mapperMock.Verify(m => m.Map<EmployeeDto>(employeeEntity), Times.Once);
        }

        //ID no encontrado retorna null
        [Fact]
        public async Task Handle_IdNotFound_ReturnsNull()
        {
            var query = new GetEmployeesByIdQuery(999L);

            _repositoryMock.Setup(r => r.GetByIdAsync(query.Id)).ReturnsAsync((EmployeEntity?)null);

            var result = await _handler.Handle(query, CancellationToken.None); 
            
            Assert.Null(result);

            _repositoryMock.Verify(r => r.GetByIdAsync(query.Id), Times.Once);
            _mapperMock.Verify(m => m.Map<EmployeeDto>(It.IsAny<EmployeEntity>()), Times.Never);
        }

        //DomainException
        [Fact]
        public async Task Handle_DomainException_ThrowsDomainException()
        {
            var query = new GetEmployeesByIdQuery(1L);

            _repositoryMock.Setup(r => r.GetByIdAsync(query.Id)).ThrowsAsync(new DomainException("Entidad inválida"));

            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(query, CancellationToken.None));
            _mapperMock.Verify(m => m.Map<EmployeeDto>(It.IsAny<EmployeEntity>()), Times.Never);
        }

        //InfrastructureException
        [Fact]
        public async Task Handle_InfrastructureException_ThrowsInfrastructureException()
        {
            var query = new GetEmployeesByIdQuery(1L);

            _repositoryMock.Setup(r => r.GetByIdAsync(query.Id)).ThrowsAsync(new InfrastructureException("Error de conexión a BD"));

            await Assert.ThrowsAsync<InfrastructureException>(() => _handler.Handle(query, CancellationToken.None));
        }

        //AppException
        [Fact]
        public async Task Handle_GenericException_ThrowsAppException()
        {
            var query = new GetEmployeesByIdQuery(1L);

            _repositoryMock.Setup(r => r.GetByIdAsync(query.Id)).ThrowsAsync(new Exception("Error inesperado"));

            var exception = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(query, CancellationToken.None));
            Assert.Equal("Error cargando registros.", exception.Message);
        }

        //Mapper recibe la entidad correcta
        [Fact]
        public async Task Handle_MapperCalledWithCorrectEntity()
        {
            var query = new GetEmployeesByIdQuery(42L);
            var employeeEntity = new EmployeEntity(1, 1, 1, 1, "prueba", "prueba42@test.com", "password", "User 42");
            employeeEntity.SetId(42L);

            _repositoryMock.Setup(r => r.GetByIdAsync(query.Id)).ReturnsAsync(employeeEntity);
            _mapperMock.Setup(m => m.Map<EmployeeDto>(It.IsAny<EmployeEntity>())).Returns(new EmployeeDto("prueba", "User 42", null, null, "prueba42@test.com"));

            
            await _handler.Handle(query, CancellationToken.None);
            _mapperMock.Verify(m => m.Map<EmployeeDto>(It.Is<EmployeEntity>(e => e.Id == 42L)), Times.Once);
        }

        //Repository llamado con el ID exacto
        [Fact]
        public async Task Handle_RepositoryCalledWithCorrectId()
        {
            // Arrange
            var expectedId = 123L;
            var query = new GetEmployeesByIdQuery(expectedId);

            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<long>())).ReturnsAsync((EmployeEntity?)null);

            await _handler.Handle(query, CancellationToken.None);

            _repositoryMock.Verify(r => r.GetByIdAsync(expectedId), Times.Once);
            _repositoryMock.Verify(r => r.GetByIdAsync(It.Is<long>(id => id != expectedId)), Times.Never);
        }
    }
}