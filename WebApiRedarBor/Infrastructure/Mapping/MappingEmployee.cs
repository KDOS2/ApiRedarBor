namespace Infrastructure.Mapping
{
    using Domain.Entity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MappingEmployee : IEntityTypeConfiguration<EmployeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeEntity> builder)
        {
            builder.ToTable("MappingEmployee", "dbo");
            builder.HasKey(e => e.id).HasName("PK_Employ");
            builder.Property(e => e.id).HasColumnName("Id");

            builder.Property(e => e.companyId).HasColumnName("CompanyId");
            builder.Property(e => e.companyId).IsRequired();

            builder.Property(e => e.portalId).HasColumnName("PortalId");
            builder.Property(e => e.portalId).IsRequired();

            builder.Property(e => e.roleId).HasColumnName("RoleId");
            builder.Property(e => e.roleId).IsRequired();

            builder.Property(e => e.statusId).HasColumnName("StatusId");
            builder.Property(e => e.statusId).IsRequired();

            builder.Property(e => e.username).HasColumnName("Username");
            builder.Property(e => e.username).HasMaxLength(250);
            builder.Property(e => e.username).IsRequired();

            builder.Property(e => e.password).HasColumnName("Password");
            builder.Property(e => e.password).HasMaxLength(250);
            builder.Property(e => e.password).IsRequired();

            builder.Property(e => e.email).HasColumnName("Email");
            builder.Property(e => e.email).HasMaxLength(200);
            builder.Property(e => e.email).IsRequired();

            builder.Property(e => e.name).HasColumnName("Name");
            builder.Property(e => e.name).HasMaxLength(250);
            builder.Property(e => e.name).IsRequired(false);

            builder.Property(e => e.fax).HasColumnName("Fax");
            builder.Property(e => e.fax).HasMaxLength(11);
            builder.Property(e => e.fax).IsRequired(false);

            builder.Property(e => e.telephone).HasColumnName("Telephone");
            builder.Property(e => e.telephone).HasMaxLength(11);
            builder.Property(e => e.telephone).IsRequired(false);

            builder.Property(e => e.createdOn).HasColumnName("CreatedOn");
            builder.Property(e => e.telephone).HasDefaultValueSql("GetDate()");

            builder.Property(e => e.updatedOn).HasColumnName("UpdatedOn");
            builder.Property(e => e.updatedOn).IsRequired(false);            

            builder.Property(e => e.lastLogin).HasColumnName("LastLogin");
            builder.Property(e => e.lastLogin).IsRequired(false);
        }
    }
}
