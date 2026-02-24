namespace IdentityServer
{
    using Duende.IdentityServer.Validation;
    using Duende.IdentityServer.Models;
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;
    using Domain;

    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly ContextRedarbor _context;

        public ResourceOwnerPasswordValidator(ContextRedarbor context)
        {
            _context = context;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _context.Employee.FirstOrDefaultAsync(e => e.Username == new Username(context.UserName) && e.Password == new Password(context.Password));

            if (user != null)
            {
                context.Result = new GrantValidationResult(
                    user.Id.ToString(),
                    "custom",
                    new[]
                    {
                        new Claim("name", user.Username.Value),
                        new Claim("email", user.Email.Value)
                    });
            }
            else
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    "Usuario o contraseña inválidos"
                );
            }


        }
    }
}
