using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public record AuthResponse(
                                string AccessToken,
                                string TokenType,
                                int ExpiresIn
                               );
}
