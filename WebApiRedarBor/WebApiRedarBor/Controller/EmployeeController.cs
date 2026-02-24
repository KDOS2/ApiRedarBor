namespace WebApiRedarBor.Controller
{
    using Application.CQRS.Command;
    using Application.CQRS.Query;
    using Application.Dto;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;

    [ApiController]
    [Route("api/redarbor")]
    
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// It adds a new employee
        /// </summary>
        /// <param name="command">params with employee info</param>
        /// <returns>dto wiht employee recorded</returns>
        [HttpPost]
        [Tags("Commands")]
        [SwaggerOperation(Summary = "Agrega nuevo registro")]
        public async Task<ActionResult<EmployeeDto>> Create(CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new ApiResponse<EmployeeDto>{data = result});
        }

        /// <summary>
        /// It updates an employee
        /// </summary>
        /// <param name="command">params with employee info</param>
        /// <returns>dto wiht employee updated</returns>
        [Authorize]
        [HttpPut]
        [Tags("Commands")]
        [SwaggerOperation(Summary = "Actualiza registro")]
        public async Task<ActionResult> Update(UpdateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new ApiResponse<bool>{data = true});
        }

        /// <summary>
        /// It deletes an employee
        /// </summary>
        /// <param name="command">employee id</param>
        /// <returns>status-> true/false</returns>
        [Authorize]
        [HttpPatch("{id}")]
        [Tags("Commands")]
        [SwaggerOperation(Summary = "Eliminación logica")]
        public async Task<IActionResult> SoftDelete(long id)
        {
            await _mediator.Send(new SoftDeleteEmployeeCommand(id));
            return Ok(new ApiResponse<bool> { data = true });
        }

        /// <summary>
        /// It deletes an employee
        /// </summary>
        /// <param name="command">employee id</param>
        /// <returns>status-> true/false</returns>
        [Authorize]
        [HttpDelete("{id}")]
        [Tags("Commands")]
        [SwaggerOperation(Summary = "Eliminación de registro")]
        public async Task<IActionResult> Delete(long id)
        {
            await _mediator.Send(new DeleteEmployeeCommand(id));
            return Ok(new ApiResponse<bool>{data = true});
        }

        /// <summary>
        /// It shows all employee registers
        /// </summary>
        /// <returns>an employee list</returns>
        [Authorize]
        [HttpGet]
        [Tags("Queries")]
        [SwaggerOperation(Summary = "Trae todos los registros de empleados")]
        public async Task<IActionResult> GetEmplpyee()
        {
            var result = await _mediator.Send(new GetAllEmployeesQuery());
            return Ok(new ApiResponse<IEnumerable<EmployeeDto>> { data = result });
        }

        /// <summary>
        /// It shows an employee register
        /// </summary>
        /// <returns>an employee object</returns>
        [Authorize]
        [HttpGet("{id}")]
        [Tags("Queries")]
        [SwaggerOperation(Summary = "Trae un registro de empleado")]
        public async Task<IActionResult> GetEmplpyee(long id)
        {
            var result = await _mediator.Send(new GetEmployeesByIdQuery(id));
            return Ok(new ApiResponse<EmployeeDto> { data = result });
        }
    }
}
