namespace Infrastructure.Mapping
{
    using Domain;
    using Domain.Entity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MappingEmployee : IEntityTypeConfiguration<EmployeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeEntity> builder)
        {
            builder.ToTable("Employee", "dbo");
            builder.HasKey(e => e.Id).HasName("PK_Employ");
                   

            builder.Property(e => e.CompanyId)
                   .HasConversion(v => v.Value, v => new CompanyId(v))
                   .HasColumnName("CompanyId")
                   .IsRequired();


            builder.Property(e => e.PortalId)
                   .HasConversion(v => v.Value, v => new PortalId(v))
                   .HasColumnName("PortalId")
                   .IsRequired();

            builder.Property(e => e.RoleId)
                   .HasConversion(v => v.Value, v => new RoleId(v))
                   .HasColumnName("RoleId")
                   .IsRequired();

            builder.Property(e => e.StatusId)
                   .HasConversion(v => v.Value, v => new StatusId(v))
                   .HasColumnName("StatusId")
                   .IsRequired();

            builder.Property(e => e.Username)
                   .HasConversion(v => v.Value, v => new Username(v))
                   .HasColumnName("Username")
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(e => e.Password)
                   .HasConversion(v => v.Value, v => new Password(v))
                   .HasColumnName("Password")
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(e => e.Email)
                   .HasConversion(v => v.Value, v => new Email(v))
                   .HasColumnName("Email")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(e => e.Name)
                   .HasConversion(v => v.Value, v => new Name(v))
                   .HasColumnName("Name")
                   .HasMaxLength(250)
                   .IsRequired(false);

            builder.Property(e => e.Fax)
                   .HasConversion(v => v.Value, v => new Fax(v))
                   .HasColumnName("Fax")
                   .HasMaxLength(11)
                   .IsRequired(false);

            builder.Property(e => e.Telephone)
                   .HasConversion(v => v.Value, v => new Telephone(v))
                   .HasColumnName("Telephone")
                   .HasMaxLength(11)
                   .IsRequired(false);

            builder.Property(e => e.CreatedOn);
            builder.Property(e => e.UpdatedOn).IsRequired(false);
            builder.Property(e => e.DeletedOn).IsRequired(false);
            builder.Property(e => e.LastLogin).IsRequired(false);

            
        }
    }
}
