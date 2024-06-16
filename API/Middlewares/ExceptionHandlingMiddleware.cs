using Application;
using Application.Exceptions;
using Application.Logging;
using FluentValidation;

namespace API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private IErrorLogger _errorLogger;
        private IApplicationActor _actor = null;

        public ExceptionHandlingMiddleware(RequestDelegate next,
IErrorLogger errorLogger,
IApplicationActor actor)
        {
            _next = next;
            _errorLogger = errorLogger;
            _actor = actor;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException e)
            {
                context.Response.StatusCode = 422;

                if (e.Errors.Count() > 0)
                {
                    var errors = e.Errors.Select(x => new { Error = x.ErrorMessage, Property = x.PropertyName }).ToList();
                    await context.Response.WriteAsJsonAsync(errors);
                    return;
                }

                await context.Response.WriteAsJsonAsync(new { message = e.Message });

                //Console.WriteLine($"Validation exception {e.Message}");
            }
            catch (UnauthorizedUseCaseExecutionException e)
            {
                context.Response.StatusCode = 401;
            }
            catch (UnauthorizedAccessException e)
            {
                context.Response.StatusCode = 401;
                if (string.IsNullOrEmpty(e.Message))
                {
                    return;
                }
                await context.Response.WriteAsJsonAsync(new { message = e.Message });
            }
            catch (FileNotFoundException e)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new { message = "File that you want to access was not found" });
            }
            catch (EntityNotFoundException e)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new { message = e.Message });
            }
            catch (ConflictException e)
            {
                context.Response.StatusCode = 409;
                await context.Response.WriteAsJsonAsync(new { message = e.Message });
            }
            catch (FormatException e)
            {
                context.Response.StatusCode = 400;
            }
            catch (InvalidOperationException e)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { message = e.Message });
            }
            catch (Exception e)
            {
                // var errorId = Guid.NewGuid();
                // AppError err = new AppError { ErrorId = errorId, Exception = e, Username = "test" };

                Guid errorId = _errorLogger.Log(e, _actor);

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var responseBody = new
                {
                    message = $"There was an error, contact our support with error code: {errorId}"
                };

                await context.Response.WriteAsJsonAsync(responseBody);
            }
        }
    }
}
