namespace Domain
{
    using Domain.Exceptions;
    using System.Text.RegularExpressions;

    public record CompanyId
    {
        public long? Value { get; init; }

        public CompanyId(long? value)
        {
            if (!value.HasValue || value.Value <= 0)
                throw new DomainException("CompanyId debe tener un valor permitido");

            Value = value;
        }
    }

    public record PortalId
    {
        public long? Value { get; init; }

        public PortalId(long? value)
        {
            if (!value.HasValue || value.Value <= 0)
                throw new DomainException("PortalId debe tener un valor permitido");

            Value = value;
        }
    }

    public record RoleId { 

        public long? Value { get; init; }

        public RoleId(long? value)
        {
            if (!value.HasValue || value.Value <= 0)
                throw new DomainException("RoleId debe tener un valor permitido");

            Value = value;
        }
    }

    public record StatusId {

        public int? Value { get; init; }

        public StatusId(int? value)
        {
            if (!value.HasValue || (value > 1 || value < 0))
                throw new DomainException("StatusId debe tener un valor permitido");

            Value = value;
        }
    }

    public record Username
    {
        public string Value { get; init; }

        public Username(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("El username es obligatorio.");

            if (value.Length > 250)
                throw new DomainException("El username excede la longitud premitida.");

            Value = value;
        }
    }

    public record Email
    {
        public string Value { get; init; }
        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
                throw new DomainException("Email inválido.");
            if (value.Length > 200)
                throw new DomainException("Email excede maxima logitud permitida.");

            Value = value;
        }
    }

    public record Password
    {
        public string Value { get; init; }

        public Password(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("La contraseña es obligatoria.");

            if (value.Length < 4)
                throw new DomainException("La contraseña debe tener al menos 4 caracteres.");

            if (value.Length > 250)
                throw new DomainException("La contraseña excede logitud permitida.");

            Value = value;
        }
    }

    public record Name
    {
        public string Value { get; init; }

        public Name(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (value.Length > 250)
                    throw new DomainException("El nombre excede la longitud premitida.");
            }

            Value = value;
        }
    }

    public record Fax
    {
        public string Value { get; init; }

        public Fax(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                if(!Regex.IsMatch(value, @"^(?:\d{3}-?\d{7}|\d{7})$"))
                    throw new DomainException("Fax inválido.");                
            }

            Value = value;
        }
    }

    public record Telephone
    {
        public string Value { get; init; }

        public Telephone(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (!Regex.IsMatch(value, @"^(?:3\d{9}|\d{7}|\d{3}-?\d{7})$"))
                    throw new DomainException("Telefono inválido.");
            }

            Value = value;
        }
    }
}
