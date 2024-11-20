using Application.Errors;
using Microsoft.AspNetCore.Diagnostics;

namespace API.Middlewares
{
    public record Error(int Status, string Message, Object? Details = null);

    public class ErrorHandlingMiddleware : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
                HttpContext context,
                Exception exception,
                CancellationToken cancellationToken)
        {
            var error = exception switch
            {
                NotFoundException e => new Error(StatusCodes.Status404NotFound, e.Message),
                UpdateException e => new Error(StatusCodes.Status500InternalServerError, e.Message),
                OccupationOpException e => new Error(StatusCodes.Status400BadRequest, e.Message, e.Fail),
                _ => new Error(StatusCodes.Status500InternalServerError, "Unknown server error.")
            };

            context.Response.StatusCode = error.Status;
            await context.Response.WriteAsJsonAsync(error, cancellationToken);

            return true;
        }
    }
}