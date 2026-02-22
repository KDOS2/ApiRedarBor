namespace Infrastructure
{
    using Domain.Entity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    public class ContextRedarbor : DbContext
    {

        public ContextRedarbor(DbContextOptions<ContextRedarbor> options) : base(options)
        { }

        #region "Datasets"
        public DbSet<EmployeEntity> Employee { get; set; }
        #endregion

        #region DbContext overrides

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextRedarbor).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        #endregion

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ContextRedarbor>
        {
            public ContextRedarbor CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("./appsettings.json")
                    .Build();

                var builder = new DbContextOptionsBuilder<ContextRedarbor>();
                var connectionString = configuration.GetConnectionString("CrudConnection");
                builder.UseSqlServer(connectionString);
                return new ContextRedarbor(builder.Options);
            }
        }

        public override int SaveChanges()
        {
            var changes = from e in this.ChangeTracker.Entries()
                          where e.State != EntityState.Unchanged
                          select e;
            var entris = this.ChangeTracker.Entries();

            foreach (var change in changes)
            {
                if (change.State == EntityState.Added)
                { }
                else if (change.State == EntityState.Modified)
                { }
                else if (change.State == EntityState.Deleted)
                { }
            }
            return base.SaveChanges();
        }
    }
}

