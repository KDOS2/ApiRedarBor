namespace WebApiRedarBor.Tests.Application.CQRS.Command
{
    using AutoMapper;
    using Domain.Entity;
    using Domain.Exceptions;
    using Domain.IRepository;
    using global::Application.CQRS.Command;
    using global::Application.Dto;
    using global::Application.Exceptions;
    using Infrastructure.Exceptions;
    using Moq;
    using Xunit;

    public class CreateEmployeeHandlerTests
    {
        //Mocks
        private readonly Mock<IEmployeeSetRepository> _setRepositoryMock;
        private readonly Mock<IEmployeeGetRepository> _getRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        //Handler
        private readonly CreateEmployeeHandler _handler;

        public CreateEmployeeHandlerTests()
        {
            _setRepositoryMock = new Mock<IEmployeeSetRepository>();
            _getRepositoryMock = new Mock<IEmployeeGetRepository>();
            _mapperMock = new Mock<IMapper>();

            _handler = new CreateEmployeeHandler(
                _setRepositoryMock.Object,
                _getRepositoryMock.Object,
                _mapperMock.Object
            );
        }

        ///Creación exitosa de empleado
        [Fact]
        public async Task Handle_ValidCommand_ReturnsEmployeeDto()
        {
            var command = new CreateEmployeeCommand(
                CompanyId: 1,
                PortalId: 1,
                RoleId: 1,
                StatusId: 1,
                Username: "testuser",
                Email: "test@example.com",
                Password: "TestPass123",
                Name: "Test User",
                Fax: "1234567",
                Telephone: "7654321"
            );

            var existingEmployee = (EmployeEntity?)null;

            var createdEmployee = new EmployeEntity(
                command.CompanyId, command.PortalId, command.RoleId, command.StatusId,
                command.Username ?? string.Empty, command.Email ?? string.Empty,
                command.Password ?? string.Empty, command.Name,
                command.Fax, command.Telephone
            );
            createdEmployee.SetId(1L);

            var expectedDto = new EmployeeDto(
                Username: "testuser",
                Name: "Test User",
                Fax: "123456",
                Telephone: "654321",
                Email: "test@example.com"
            );

            _getRepositoryMock.Setup(r => r.GetByUserNameAsync(It.IsAny<string>(),It.IsAny<long?>(),It.IsAny<bool>())).ReturnsAsync(existingEmployee);
            _getRepositoryMock.Setup(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<long?>(), It.IsAny<bool>())).ReturnsAsync(existingEmployee);
            _setRepositoryMock.Setup(r => r.AddAsync(It.IsAny<EmployeEntity>())).ReturnsAsync(createdEmployee);
            _mapperMock.Setup(m => m.Map<EmployeeDto>(It.IsAny<EmployeEntity>())).Returns(expectedDto);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(expectedDto.Username, result.Username);
            Assert.Equal(expectedDto.Email, result.Email);
            Assert.Equal(expectedDto.Name, result.Name);
            Assert.Equal(expectedDto.Fax, result.Fax);
            Assert.Equal(expectedDto.Telephone, result.Telephone);

            _getRepositoryMock.Verify(r => r.GetByUserNameAsync(command.Username ?? string.Empty,It.IsAny<long?>(),It.IsAny<bool>()), Times.Once);
            _getRepositoryMock.Verify(r => r.GetByEmailAsync(command.Email ?? string.Empty, It.IsAny<long?>(), It.IsAny<bool>()), Times.Once);
            
            _setRepositoryMock.Verify(r => r.AddAsync(It.IsAny<EmployeEntity>()), Times.Once);
            _mapperMock.Verify(m => m.Map<EmployeeDto>(It.IsAny<EmployeEntity>()), Times.Once);
        }

        //Username ya existe
        [Fact]
        public async Task Handle_UsernameExists_ThrowsAppNotFoundException()
        {
            var command = new CreateEmployeeCommand(
                CompanyId: 1,
                PortalId: 1,
                RoleId: 1,
                StatusId: 1,
                Username: "existinguser",
                Email: "new@example.com",
                Password: "TestPass123"
            );

            var existingEmployee = new EmployeEntity(1, 1, 1, 1, "existinguser", "old@example.com", "hash", "Old User");

            _getRepositoryMock.Setup(r => r.GetByUserNameAsync(It.IsAny<string>(), It.IsAny<long?>(), It.IsAny<bool>())).ReturnsAsync(existingEmployee);
            var exception = await Assert.ThrowsAsync<AppNotFoundException>(() => _handler.Handle(command, CancellationToken.None));

            Assert.Equal("Ya se encuentra registrado este userName.", exception.Message);
            _setRepositoryMock.Verify(r => r.AddAsync(It.IsAny<EmployeEntity>()), Times.Never);
        }

        //Email ya existe
        [Fact]
        public async Task Handle_EmailExists_ThrowsAppNotFoundException()
        {
            var command = new CreateEmployeeCommand(
                CompanyId: 1,
                PortalId: 1,
                RoleId: 1,
                StatusId: 1,
                Username: "newuser",
                Email: "existing@example.com",
                Password: "TestPass123"
            );

            _getRepositoryMock.Setup(r => r.GetByUserNameAsync(It.IsAny<string>(), It.IsAny<long?>(), It.IsAny<bool>())).ReturnsAsync((EmployeEntity?)null);
            var existingEmployee = new EmployeEntity(1, 1, 1, 1, "otheruser", "existing@example.com", "hash", "Other User");
            _getRepositoryMock.Setup(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<long?>(), It.IsAny<bool>())).ReturnsAsync(existingEmployee);
            var exception = await Assert.ThrowsAsync<AppNotFoundException>(() => _handler.Handle(command, CancellationToken.None));

            Assert.Equal("Ya se encuentra registrado este correo electronico.", exception.Message);
            _setRepositoryMock.Verify(r => r.AddAsync(It.IsAny<EmployeEntity>()), Times.Never);
        }

        //DomainException
        [Fact]
        public async Task Handle_DomainException_ThrowsDomainException()
        {
            var command = new CreateEmployeeCommand(
                CompanyId: 1,
                PortalId: 1,
                RoleId: 1,
                StatusId: 1,
                Username: "testuser",
                Email: "test@example.com",
                Password: "TestPass123"
            );

            _getRepositoryMock.Setup(r => r.GetByUserNameAsync(It.IsAny<string>(), It.IsAny<long?>(), It.IsAny<bool>())).ReturnsAsync((EmployeEntity?)null);
            _getRepositoryMock.Setup(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<long?>(), It.IsAny<bool>())).ReturnsAsync((EmployeEntity?)null);

            _setRepositoryMock.Setup(r => r.AddAsync(It.IsAny<EmployeEntity>())).ThrowsAsync(new DomainException("Datos inválidos"));            
            await Assert.ThrowsAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));
        }

        //InfrastructureException
        [Fact]
        public async Task Handle_InfrastructureException_ThrowsInfrastructureException()
        {
            var command = new CreateEmployeeCommand(
                CompanyId: 1,
                PortalId: 1,
                RoleId: 1,
                StatusId: 1,
                Username: "testuser",
                Email: "test@example.com",
                Password: "TestPass123"
            );

            _getRepositoryMock.Setup(r => r.GetByUserNameAsync(It.IsAny<string>(), It.IsAny<long?>(), It.IsAny<bool>())).ReturnsAsync((EmployeEntity?)null);
            _getRepositoryMock.Setup(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<long?>(), It.IsAny<bool>())).ReturnsAsync((EmployeEntity?)null);

            _setRepositoryMock.Setup(r => r.AddAsync(It.IsAny<EmployeEntity>())).ThrowsAsync(new InfrastructureException("Error de base de datos"));
            await Assert.ThrowsAsync<InfrastructureException>(() => _handler.Handle(command, CancellationToken.None));
        }

        //AppException
        [Fact]
        public async Task Handle_GenericException_ThrowsAppException()
        {
            var command = new CreateEmployeeCommand(
                CompanyId: 1,
                PortalId: 1,
                RoleId: 1,
                StatusId: 1,
                Username: "testuser",
                Email: "test@example.com",
                Password: "TestPass123"
            );

            _getRepositoryMock.Setup(r => r.GetByUserNameAsync(It.IsAny<string>(), It.IsAny<long?>(), It.IsAny<bool>())).ReturnsAsync((EmployeEntity?)null);
            _getRepositoryMock.Setup(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<long?>(), It.IsAny<bool>())).ReturnsAsync((EmployeEntity?)null);

            _setRepositoryMock.Setup(r => r.AddAsync(It.IsAny<EmployeEntity>())).ThrowsAsync(new Exception("Error inesperado"));
            var exception = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("Error registrando nuevo empleado", exception.Message);
        }

        //Valores opcionales (Name, Fax, Telephone) pueden ser null
        [Fact]
        public async Task Handle_OptionalFieldsNull_CreatesEmployeeSuccessfully()
        {
            var command = new CreateEmployeeCommand(
                CompanyId: 1,
                PortalId: 1,
                RoleId: 1,
                StatusId: 1,
                Username: "testuser",
                Email: "test@example.com",
                Password: "TestPass123"            
            );

            _getRepositoryMock.Setup(r => r.GetByUserNameAsync(It.IsAny<string>(), It.IsAny<long?>(), It.IsAny<bool>())).ReturnsAsync((EmployeEntity?)null);
            _getRepositoryMock.Setup(r => r.GetByEmailAsync(It.IsAny<string>(), It.IsAny<long?>(), It.IsAny<bool>())).ReturnsAsync((EmployeEntity?)null);

            var createdEmployee = new EmployeEntity(1, 1, 1, 1, "testuser", "test@example.com", "hash", null, null, null);
            createdEmployee.SetId(1L);

            var expectedDto = new EmployeeDto(
                Username: "testuser",
                Name: null,
                Fax: null,
                Telephone: null,
                Email: "test@example.com"
            );

            _setRepositoryMock.Setup(r => r.AddAsync(It.IsAny<EmployeEntity>())).ReturnsAsync(createdEmployee);

            _mapperMock.Setup(m => m.Map<EmployeeDto>(It.IsAny<EmployeEntity>()))
                       .Returns(expectedDto);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("testuser", result.Username);
            Assert.Equal("test@example.com", result.Email);
            Assert.Null(result.Name);
            Assert.Null(result.Fax);
            Assert.Null(result.Telephone);
        }       
    }
}