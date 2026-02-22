namespace Infrastructure.Repository
{
    using Dapper;
    using Domain.Entity;
    using Domain.IRepository;
    using Infrastructure.DTO;
    using Microsoft.Identity.Client;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public class EmployeeGetRepository : IEmployeeGetRepository
    {
        private readonly IDbConnection _connection;

        public EmployeeGetRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<EmployeEntity>> GetAllAsync()
        {
            var sql = "SELECT CompanyId, PortalId, RoleId, StatusId, Username, Email, Password, Name, Fax, Telephone FROM Employees WHERE IsDelete = 0";
            var employeeDb = await _connection.QueryAsync<EmployeeDTO>(sql);

            if (employeeDb == null) return new List<EmployeEntity>();

            return employeeDb.Select(e => new EmployeEntity(
                                                                e.CompanyId,
                                                                e.PortalId,
                                                                e.RoleId,
                                                                e.StatusId,
                                                                e.Username,
                                                                e.Email,
                                                                e.Password,
                                                                e.Name,
                                                                e.Fax,
                                                                e.Telephone
                                                            ));
        }

        public async Task<EmployeEntity?> GetByIdAsync(long id)
        {
            var sql = "SELECT Id, CompanyId, PortalId, RoleId, StatusId, Username, Email, Password, Name, Fax, Telephone, CreatedOn, UpdatedOn, LastLogin FROM Employee WHERE Id = @Id AND IsDelete = 0";
            var employeeDb = await _connection.QueryFirstOrDefaultAsync<EmployeeDTO>(sql, new { Id = id });

            if (employeeDb == null) return null;
            var employee = new EmployeEntity(
                                                employeeDb.CompanyId,
                                                employeeDb.PortalId,
                                                employeeDb.RoleId,
                                                employeeDb.StatusId,
                                                employeeDb.Username,
                                                employeeDb.Email,
                                                employeeDb.Password,
                                                employeeDb.Name,
                                                employeeDb.Fax,
                                                employeeDb.Telephone,
                                                employeeDb.CreatedOn,
                                                employeeDb.UpdatedOn,
                                                employeeDb.LastLogin
                                            );
            employee.SetId(employeeDb.Id);
            return employee;
        }

        public async Task<EmployeEntity?> GetByUserNameAsync(string userName, long? id = null, bool isUpdate = false)
        {
            
            var sql = !isUpdate ? "SELECT CompanyId, PortalId, RoleId, StatusId, Username, Email, Password, Name, Fax, Telephone FROM Employee WHERE Username = @userName AND IsDelete = 0" : "SELECT CompanyId, PortalId, RoleId, StatusId, Username, Email, Password, Name, Fax, Telephone FROM Employee WHERE Username = @userName and Id != @id AND IsDelete = 0";
            var employeeDb = !isUpdate ? await _connection.QueryFirstOrDefaultAsync<EmployeeDTO>(sql, new { Username = userName }) : await _connection.QueryFirstOrDefaultAsync<EmployeeDTO>(sql, new { Username = userName, Id = id });

            if (employeeDb == null) return null;

            return new EmployeEntity(
                employeeDb.CompanyId,
                employeeDb.PortalId,
                employeeDb.RoleId,
                employeeDb.StatusId,
                employeeDb.Username,
                employeeDb.Email,
                employeeDb.Password,
                employeeDb.Name,
                employeeDb.Fax,
                employeeDb.Telephone
            );
        }

        public async Task<EmployeEntity?> GetByEmailAsync(string email, long? id = null, bool isUpdate = false)
        {
            var sql = !isUpdate ? "SELECT CompanyId, PortalId, RoleId, StatusId, Username, Email, Password, Name, Fax, Telephone FROM Employee WHERE Email = @email AND IsDelete = 0" : "SELECT CompanyId, PortalId, RoleId, StatusId, Username, Email, Password, Name, Fax, Telephone FROM Employee WHERE Email = @email and Id != @id AND IsDelete = 0";
            var employeeDb = !isUpdate ?  await _connection.QueryFirstOrDefaultAsync<EmployeeDTO>(sql, new { Email = email }) : await _connection.QueryFirstOrDefaultAsync<EmployeeDTO>(sql, new { Email = email, Id = id });

            if (employeeDb == null) return null;

            return new EmployeEntity(
                employeeDb.CompanyId,
                employeeDb.PortalId,
                employeeDb.RoleId,
                employeeDb.StatusId,
                employeeDb.Username,
                employeeDb.Email,
                employeeDb.Password,
                employeeDb.Name,
                employeeDb.Fax,
                employeeDb.Telephone
            );
        }
    }
}
