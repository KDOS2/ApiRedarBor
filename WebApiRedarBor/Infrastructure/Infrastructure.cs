namespace Infrastructure
{
    using Domain.IRepository;
    using Infrastructure.Repository;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeGetRepository, EmployeeGetRepository>();
            services.AddScoped<IEmployeeSetRepository, EmployeeSetRepository>();            

            return services;
        }
    }
}