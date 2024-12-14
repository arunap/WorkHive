using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WorkHive.Api.Dtos;
using WorkHive.Domain.Exceptions;

namespace WorkHive.Api.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        private static readonly JsonSerializerOptions JSON_OPTIONS = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        private readonly RequestDelegate _next = next;

        private readonly ILogger<ExceptionMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Proceed to the next middleware in the pipeline
                await _next(context);
            }
            catch (FileUploadException ex)
            {
                await HandleFileUploadExceptionAsync(context, ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                await HandleConcurencyAsync(context, ex);
            }
            catch (Exception ex)
            {
                // Handle the exception
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleFileUploadExceptionAsync(HttpContext context, FileUploadException exception)
        {
            _logger.LogError(exception, "Error in image uploading.");

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            // Create a response object with error details
            var response = new ErrorDto
            {
                StatusCode = context.Response.StatusCode,
                Message = "Error in image uploading.",
                Errors = [exception.Message]
            };

            var responseJson = JsonSerializer.Serialize(response, JSON_OPTIONS);
            return context.Response.WriteAsync(responseJson);
        }

        private Task HandleConcurencyAsync(HttpContext context, DbUpdateConcurrencyException exception)
        {
            _logger.LogWarning(exception, "Concurrency exception occurred. Data may have been modified by another user.");

            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            context.Response.ContentType = "application/json";

            // Create a response object with error details
            var response = new ErrorDto
            {
                StatusCode = context.Response.StatusCode,
                Message = "Concurrency exception occurred. Data may have been modified by another user.",
                Errors = ["The data has been updated by another user. Please reload the data."]
            };

            var responseJson = JsonSerializer.Serialize(response, JSON_OPTIONS);
            return context.Response.WriteAsync(responseJson);
        }

        private  Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Log the exception
            _logger.LogError(exception, "Something went wrong.");

            // Set the response status code
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            // Create a response object with error details
            var response = new ErrorDto
            {
                StatusCode = context.Response.StatusCode,
                Message = "Error occured in the server!",
                Errors = [exception.Message]
            };

            // Serialize and return the response as JSON
            var responseJson = JsonSerializer.Serialize(response, JSON_OPTIONS);

            return context.Response.WriteAsync(responseJson);
        }
    }
}