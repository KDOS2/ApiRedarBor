namespace Application.CQRS.Command
{
    using Application.Dto;
    using Domain.IRepository;
    using MediatR;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using System.Net;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    public class LoginHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ILogger<LoginHandler> _logger;
        private readonly IEmployeeGetRepository _getRepository;
        private readonly IEmployeeSetRepository _setRepository;

        public LoginHandler(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            ILogger<LoginHandler> logger,
            IEmployeeGetRepository getRepository,
            IEmployeeSetRepository setRepository)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClient = httpClientFactory.CreateClient("IdentityServer");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
            _getRepository = getRepository;
            _setRepository = setRepository;
        }

        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request?.Username) || string.IsNullOrWhiteSpace(request?.Password))
                throw new ApplicationException("Username y Password son requeridos");

            var identitySettings = _configuration.GetSection("IdentityServerSettings");
            var discoveryEndpoint = identitySettings["EntityServerEndpoint"]?.TrimEnd('/');
            var clientId = identitySettings["ClientId"];
            var clientSecret = identitySettings["ClientSecret"];

            if (string.IsNullOrEmpty(discoveryEndpoint) || string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
                throw new ApplicationException("Error de configuración del servidor de identidad");            

            var tokenEndpoint = $"{discoveryEndpoint}/connect/token";

            var content = new FormUrlEncodedContent(new Dictionary<string, string>{
                { "grant_type", "password" },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "username", request.Username },
                { "password", request.Password },
                { "scope", "redarbor.api" }
            });

            try
            {
                var response = await _httpClient.PostAsync(tokenEndpoint, content, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode is HttpStatusCode.BadRequest or HttpStatusCode.Unauthorized)
                        throw new ApplicationException("Usuario o contraseña inválidos");
                }

                var json = await response.Content.ReadAsStringAsync(cancellationToken);
                var tokenResponse = JsonSerializer.Deserialize<TokenResponseDto>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});

                if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.AccessToken))
                    throw new ApplicationException("Respuesta inválida del servidor de autenticación");

                var employee = await _getRepository.GetByUserPasswordAsync(request?.Username??String.Empty, request?.Password??String.Empty);
                employee?.SetLoginDate();
                await _setRepository.UpdateAsync(employee);

                return new AuthResponse(
                    AccessToken: tokenResponse.AccessToken,
                    TokenType: tokenResponse.TokenType ?? "Bearer",
                    ExpiresIn: tokenResponse.ExpiresIn
                );
            }
            catch (HttpRequestException)
            {
                throw new ApplicationException("No se pudo conectar con el servidor de autenticación");
            }
        }
    }
}