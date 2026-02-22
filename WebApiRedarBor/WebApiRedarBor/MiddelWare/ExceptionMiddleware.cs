namespace WebApiRedarBor.MiddelWare
{
    using Application.Exceptions;
    using Domain.Exceptions;
    using Infrastructure.Exceptions;

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int statusCode = ex switch
            {
                DomainException => StatusCodes.Status400BadRequest,
                InfrastructureException => StatusCodes.Status500InternalServerError,
                AppException => StatusCodes.Status500InternalServerError,
                AppNotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            var response = new
            {
                error = ex.Message,
                status = statusCode,
                success = false
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
