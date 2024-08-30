using CRUDExa.Utilidades;
using System.Net;
using System.Text;

namespace CRUDExa.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        /// <summary>
        ///  Constructor que inicializa el middleware con los servicios inyectados.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public ErrorHandlingMiddleware(RequestDelegate next, IWebHostEnvironment env, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        /// <summary>
        /// Método Invoke que se llama en cada solicitud HTTP. Intenta procesar la solicitud y maneja cualquier excepción que ocurra.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _env, _logger);
            }
        }

        /// <summary>
        /// Método privado y estático para manejar las excepciones.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception, IWebHostEnvironment env, ILogger<ErrorHandlingMiddleware> logger)
        {
            logger.LogError(exception, exception.Message);

            var code = HttpStatusCode.InternalServerError;

            var errorDetails = new StringBuilder();

            if (env.IsDevelopment())
            {
                Exception innerException = exception;

                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                    errorDetails.AppendLine($"Inner Exception Type: {innerException.GetType().Name}");
                    errorDetails.AppendLine($"Inner Exception: {innerException.Message}");
                    errorDetails.AppendLine($"Inner StackTrace: {innerException.StackTrace}");
                    errorDetails.AppendLine();
                }

                errorDetails.AppendLine($"Exception Type: {exception.GetType().Name}");
                errorDetails.AppendLine($"Exception: {exception.Message}");
                errorDetails.AppendLine($"StackTrace: {exception.StackTrace}");
            }
            else
            {
                // En producción, proporciona un mensaje genérico.
                errorDetails.Append("¡Ups! Algo salió mal. Por favor, inténtalo de nuevo más tarde.");
            }

            var response = new ModelResponse
            {
                Code = 0,
                Comments = "¡Ups! Algo salió mal. Por favor, inténtalo de nuevo más tarde.",
                Response = errorDetails.ToString()
            };

            var result = JsonUtils.SerializeObjectToLowerCamelCase(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
