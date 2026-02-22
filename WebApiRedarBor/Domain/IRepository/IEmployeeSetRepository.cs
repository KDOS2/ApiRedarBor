using Domain.Entity;

namespace Domain.IRepository
{
    public interface IEmployeeSetRepository
    {
        /// <summary>
        /// adds a new employee
        /// </summary>
        /// <param name="employee">employee data info</param>
        /// <returns>employee info</returns>
        Task<EmployeEntity> AddAsync(EmployeEntity employee);

        /// <summary>
        /// Updates an employeee
        /// </summary>
        /// <param name="employee">employee data info</param>
        /// <returns>n/a</returns>
        Task UpdateAsync(EmployeEntity employee);

        /// <summary>
        /// Deletes an employee record
        /// </summary>
        /// <param name="employee">employee data info</param>
        /// <returns>n/a</returns>
        Task DeleteAsync(EmployeEntity employee);
    
    }
}
