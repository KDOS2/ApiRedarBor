namespace Domain.IRepository
{
    using Domain.Entity;

    public interface IEmployeeGetRepository
    {
        Task<IEnumerable<EmployeEntity>> GetAllAsync();
        Task<EmployeEntity?> GetByIdAsync(long id);

        Task<EmployeEntity?> GetByUserNameAsync(string userName, long? id = null, bool isUpdate = false);

        Task<EmployeEntity?> GetByEmailAsync(string email, long? id = null, bool isUpdate = false);
        Task<EmployeEntity?> GetByUserPasswordAsync(string user, string psw);
    }
}
