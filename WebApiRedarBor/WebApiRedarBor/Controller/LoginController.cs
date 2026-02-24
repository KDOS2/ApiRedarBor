namespace WebApiRedarBor.Controller
{
    using Application.CQRS.Command;
    using Application.Dto;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;


    [ApiController]
    [Route("api/redarbor")]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// It allows a login
        /// </summary>
        /// <param name="command">params with employee longIn info</param>
        /// <returns>token</returns>
        [HttpPost("Login")]
        [Tags("LogIn")]
        [SwaggerOperation(Summary = "Agrega nuevo registro")]
        public async Task<ActionResult<AuthResponse>> Create(LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new ApiResponse<AuthResponse> { data = result });
        }
    }
}
