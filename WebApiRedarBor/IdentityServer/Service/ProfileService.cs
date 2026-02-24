
namespace IdentityServer.Service
{
    using Duende.IdentityServer.Models;
    using Duende.IdentityServer.Services;
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;

    namespace IdentityServer.Services
    {
        public class ProfileService : IProfileService
        {
            private readonly ContextRedarbor _db;

            public ProfileService(ContextRedarbor db)
            {
                _db = db;
            }

            public async Task GetProfileDataAsync(ProfileDataRequestContext context)
            {
                var userId = context.Subject?.FindFirst("sub")?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    var user = await _db.Employee.FirstOrDefaultAsync(u => u.Id.ToString() == userId);

                    if (user != null)
                    {
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username.Value),
                        new Claim(ClaimTypes.Email, user.Email.Value),
                        new Claim("userId", user.Id.ToString()),
                        new Claim("Name", user.Name?.Value ?? user.Username.Value)                        
                    };

                        context.IssuedClaims = claims;
                    }
                }
            }

            public async Task IsActiveAsync(IsActiveContext context)
            {
                var userId = context.Subject?.FindFirst("sub")?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    var user = await _db.Employee.FirstOrDefaultAsync(
                        u => u.Id.ToString() == userId
                    );

                    context.IsActive = user != null && (user.StatusId?.Value == 1 ? true : false);
                }
            }
        }
    }
}