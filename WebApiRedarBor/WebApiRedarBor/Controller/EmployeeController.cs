namespace WebApiRedarBor.Controller
{
    using Application.CQRS.Command;
    using Application.Dto;
    using MediatR;
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
        [HttpPut]
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
        [HttpPatch("{id}")]
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
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Eliminación de registro")]
        public async Task<IActionResult> Delete(long id)
        {
            await _mediator.Send(new DeleteEmployeeCommand(id));
            return Ok(new ApiResponse<bool>{data = true});
        }
    }
}
