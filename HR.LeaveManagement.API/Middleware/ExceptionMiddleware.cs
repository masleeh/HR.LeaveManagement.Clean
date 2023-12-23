using HR.LeaveManagement.API.Model;
using HR.LeaveManagement.Application.Exceptions;
using SendGrid.Helpers.Errors.Model;
using System.Net;
using BadRequestException = HR.LeaveManagement.Application.Exceptions.BadRequestException;
using NotFoundException = HR.LeaveManagement.Application.Exceptions.NotFoundException;

namespace HR.LeaveManagement.API.Middleware {
    public class ExceptionMiddleware {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await _next(context);
            }
            catch (Exception ex) {
                await HandleExceptionAsync(context, ex);

            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex) {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomValidationProblemDetails problem = new();

            switch(ex) {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomValidationProblemDetails() {
                        Title = badRequestException.Message,
                        Status = (int)statusCode,
                        Detail = badRequestException.InnerException?.Message,
                        Type = nameof(BadRequestException),
                        Errors = badRequestException.ValidationErrors
                    };
                    break;
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    problem = new CustomValidationProblemDetails() {
                        Title = notFoundException.Message,
                        Status = (int)statusCode,
                        Detail = notFoundException?.InnerException?.Message,
                        Type = nameof(NotFoundException)
                    };
                    break;
                default:
                    problem = new CustomValidationProblemDetails() {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Type = nameof(HttpStatusCode.InternalServerError),
                        Detail = ex?.StackTrace
                    };
                    break;
            }

            httpContext.Response.StatusCode = (int)statusCode;
            await httpContext.Response.WriteAsJsonAsync(problem);

        }
    }
}
