namespace Domain.Entity
{
    using System;

    public class EmployeEntity
    {
        public long Id { get; private set; }
        public CompanyId CompanyId { get; private set; }
        public PortalId PortalId { get; private set; }
        public RoleId RoleId { get; private set; }
        public StatusId StatusId { get; private set; }
        public Username Username { get; private set; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public Name Name { get; private set; }
        public Fax Fax { get; private set; }
        public Telephone Telephone { get; private set; }
        public bool IsDelete { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime? UpdatedOn { get; private set; }
        public DateTime? DeletedOn { get; private set; }        
        public DateTime? LastLogin { get; private set; }

        private EmployeEntity()
        {
            CompanyId = default!;
            PortalId = default!;
            RoleId = default!;
            StatusId = default!;
            Username = default!;
            Email = default!;
            Password = default!;
            Name = default!;
            Fax = default!;
            Telephone = default!;
            CreatedOn = default;
            IsDelete = false;
        }//EF

        public EmployeEntity(
            long companyId,
            long portalId,
            long roleId,
            int statusId,
            string username,
            string email,
            string password,
            string? name = null,
            string? fax = null,
            string? telephone = null,
            DateTime? createdOn = null,
            DateTime? updatedOn = null,
            DateTime? lastLogin = null)
        {
            CompanyId = new CompanyId(companyId);
            PortalId = new PortalId(portalId);
            RoleId = new RoleId(roleId);
            StatusId = new StatusId(statusId);
            Username = new Username(username);
            Email = new Email(email);
            Password = new Password(password);
            Name = new Name(name ?? string.Empty);
            Fax = new Fax(fax ?? string.Empty);
            Telephone = new Telephone(telephone ?? string.Empty);
            CreatedOn = createdOn ?? DateTime.Now;
            UpdatedOn = updatedOn;
            LastLogin = lastLogin;
            IsDelete = false;
        }        

        public void UpdateProfileData(long id, string userName, string password, string email, long rolId, int statusId, string? name = null, string? fax = null, string? telephone = null)
        {
            Id = id;
            Username = new Username(userName);
            Password = new Password(password);
            Email = new Email(email);
            Name = new Name(name??string.Empty);
            Fax = new Fax(fax??string.Empty);
            Telephone = new Telephone(telephone??string.Empty);
            RoleId = new RoleId(rolId);
            StatusId = new StatusId(statusId);
            UpdatedOn = DateTime.Now;
        }

        public void SetId(long id) {
            Id = id;
        }

        public void SoftDelete() {
            IsDelete = true;
            DeletedOn = DateTime.Now;
        }
        
    }
}