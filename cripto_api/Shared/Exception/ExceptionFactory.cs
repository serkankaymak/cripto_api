namespace Shared.ApiResponse
{
    public static class ExceptionFactory
    {
        // 400 - Bad Request
        public static Exception BadRequest(string message = "The request is invalid or malformed.")
            => new ArgumentException(message);

        // 401 - Unauthorized
        public static Exception Unauthorized(string message = "Authentication is required to access this resource.")
            => new UnauthorizedAccessException(message);

        // 403 - Forbidden
        public static Exception Forbidden(string message = "You do not have permission to access this resource.")
            => new UnauthorizedAccessException(message);

        // 404 - Not Found
        public static Exception NotFound(string message = "The requested resource was not found.")
            => new KeyNotFoundException(message);

        // 422 - Unprocessable Entity
        public static Exception UnprocessableEntity(string message = "The request data is semantically incorrect or cannot be processed.")
            => new InvalidOperationException(message);

        // 500 - Internal Server Error
        public static Exception InternalServerError(string message = "An unexpected internal server error has occurred.")
            => new Exception(message);

        // İş kuralı ihlalleri vb. özel durumlar
        public static Exception BusinessRuleViolation(string message = "A business rule has been violated.")
            => new ApplicationException(message);
    }
}
