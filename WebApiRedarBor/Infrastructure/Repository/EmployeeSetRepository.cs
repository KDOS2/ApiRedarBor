namespace Infrastructure.Repository
{
    using Domain.Entity;
    using Domain.IRepository;
    using Infrastructure.Exceptions;
    using Microsoft.EntityFrameworkCore;

    public class EmployeeSetRepository : IEmployeeSetRepository
    {
        private readonly ContextRedarbor _context;

        public EmployeeSetRepository(ContextRedarbor context)
        {
            this._context = context;
        }

        /// <summary>
        /// adds a new employee
        /// </summary>
        /// <param name="employee">employee data info</param>
        /// <returns>employee info</returns>
        public async Task<EmployeEntity> AddAsync(EmployeEntity employee)
        {
            try
            {
                await this._context.AddAsync(employee);
                await this._context.SaveChangesAsync();
                return employee;
            }
            catch (DbUpdateException)
            {
                throw new InfrastructureException("Error almacenando empleado en base de datos.");
            }
        }

        /// <summary>
        /// Deletes an employee record
        /// </summary>
        /// <param name="employee">employee data info</param>
        /// <returns>n/a</returns>
        public async Task DeleteAsync(EmployeEntity employee)
        {
            try
            {
                _context.Employee.Remove(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new InfrastructureException("Error actualizando empleado en base de datos.");
            }
        }

        /// <summary>
        /// Updates an employeee
        /// </summary>
        /// <param name="employee">employee data info</param>
        /// <returns>n/a</returns>
        public async Task UpdateAsync(EmployeEntity employee)
        {
            try
            {
                this._context.Employee.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new InfrastructureException("Error actualizando empleado en base de datos.");
            }
        }
    }
}
