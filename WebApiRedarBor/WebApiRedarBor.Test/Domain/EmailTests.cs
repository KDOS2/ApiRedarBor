namespace WebApiRedarBor.Test.Domain
{
    using global::Domain;
    using global::Domain.Exceptions;
    using Xunit;

    public class EmailTests
    {
        //Email null lanza excepción
        [Fact]
        public void Constructor_NullEmail_ThrowsDomainException()
        {
            var exception = Assert.Throws<DomainException>(() => new Email(null!));
            Assert.Equal("Email inválido.", exception.Message);
        }

        //Email vacío lanza excepción
        [Fact]
        public void Constructor_EmptyEmail_ThrowsDomainException()
        {
            var exception = Assert.Throws<DomainException>(() => new Email(""));
            Assert.Equal("Email inválido.", exception.Message);
        }

        //Email solo espacios lanza excepción
        [Fact]
        public void Constructor_WhitespaceEmail_ThrowsDomainException()
        {
            var exception = Assert.Throws<DomainException>(() => new Email("   "));
            Assert.Equal("Email inválido.", exception.Message);
        }

        //Email sin @ lanza excepción
        [Theory]
        [InlineData("sin-arroba.com")]
        [InlineData("sinarroba.com")]
        public void Constructor_EmailWithoutAtSymbol_ThrowsDomainException(string invalidEmail)
        {
            var exception = Assert.Throws<DomainException>(() => new Email(invalidEmail));
            Assert.Equal("Email inválido.", exception.Message);
        }

        //Email sin dominio lanza excepción
        [Theory]
        [InlineData("usuario@")]
        [InlineData("usuario@.com")]
        [InlineData("usuario@domain.")]
        public void Constructor_EmailWithoutProperDomain_ThrowsDomainException(string invalidEmail)
        {
            var exception = Assert.Throws<DomainException>(() => new Email(invalidEmail));
            Assert.Equal("Email inválido.", exception.Message);
        }

        //Email sin parte local lanza excepción
        [Theory]
        [InlineData("@domain.com")]
        [InlineData(" @domain.com")]
        public void Constructor_EmailWithoutLocalPart_ThrowsDomainException(string invalidEmail)
        {
            var exception = Assert.Throws<DomainException>(() => new Email(invalidEmail));
            Assert.Equal("Email inválido.", exception.Message);
        }

        //Email excede longitud máxima (200 caracteres)
        [Fact]
        public void Constructor_EmailExceedsMaxLength_ThrowsDomainException()
        {
            var longEmail = new string('a', 190) + "@example.com";

            var exception = Assert.Throws<DomainException>(() => new Email(longEmail));
            Assert.Equal("Email excede maxima logitud permitida.", exception.Message);
        }

        
        //Email válido estándar no lanza excepción
        [Theory]
        [InlineData("usuario@example.com")]
        [InlineData("test@domain.com")]
        [InlineData("john.doe@company.org")]
        public void Constructor_ValidStandardEmail_DoesNotThrow(string validEmail)
        {
            var email = new Email(validEmail);
            Assert.Equal(validEmail, email.Value);
        }

        //Email con subdominio no lanza excepción
        [Theory]
        [InlineData("usuario@sub.domain.com")]
        [InlineData("test@mail.server.company.com")]
        public void Constructor_EmailWithSubdomain_DoesNotThrow(string validEmail)
        {
            var email = new Email(validEmail);
            Assert.Equal(validEmail, email.Value);
        }

        //Email con punto en parte local no lanza excepción
        [Theory]
        [InlineData("john.doe@example.com")]
        [InlineData("first.last@domain.com")]
        public void Constructor_EmailWithDotInLocalPart_DoesNotThrow(string validEmail)
        {
            var email = new Email(validEmail);
            Assert.Equal(validEmail, email.Value);
        }

        //Email con guión no lanza excepción
        [Theory]
        [InlineData("user-name@example.com")]
        [InlineData("first-last@domain.com")]
        public void Constructor_EmailWithHyphen_DoesNotThrow(string validEmail)
        {
            var email = new Email(validEmail);
            Assert.Equal(validEmail, email.Value);
        }

        //Email con número no lanza excepción
        [Theory]
        [InlineData("user123@example.com")]
        [InlineData("test99@domain.com")]
        public void Constructor_EmailWithNumbers_DoesNotThrow(string validEmail)
        {
            var email = new Email(validEmail);
            Assert.Equal(validEmail, email.Value);
        }
    }
}