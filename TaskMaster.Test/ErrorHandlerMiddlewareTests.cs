using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using TaskMaster.API.Middleware;

namespace TaskMaster.Test
{
    public class ErrorHandlerMiddlewareTests
    {
        [Fact]
        public async Task HandleExceptionAsync_ShouldReturnNotFoundStatusCode()
        {
            // Arrange
            var next = new Mock<RequestDelegate>();
            next.Setup(nd => nd(It.IsAny<HttpContext>())).Throws(new KeyNotFoundException("Test exception"));
            var logger = new Mock<ILogger<ErrorHandlerMiddleware>>();
            var middleware = new ErrorHandlerMiddleware(next.Object, logger.Object);
            var context = new DefaultHttpContext();

            // Act
            await middleware.Invoke(context);

            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound, context.Response.StatusCode);
        }

        [Fact]
        public async Task HandleExceptionAsync_ShouldReturnUnauthorizedStatusCode()
        {
            // Arrange
            var next = new Mock<RequestDelegate>();
            next.Setup(nd => nd(It.IsAny<HttpContext>())).Throws(new UnauthorizedAccessException("Test exception"));
            var logger = new Mock<ILogger<ErrorHandlerMiddleware>>();
            var middleware = new ErrorHandlerMiddleware(next.Object, logger.Object);
            var context = new DefaultHttpContext();

            // Act
            await middleware.Invoke(context);

            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, context.Response.StatusCode);
        }

        [Fact]
        public async Task HandleExceptionAsync_ShouldReturnInternalServerErrorStatusCode()
        {
            // Arrange
            var next = new Mock<RequestDelegate>();
            next.Setup(nd => nd(It.IsAny<HttpContext>())).Throws(new Exception("Test exception"));
            var logger = new Mock<ILogger<ErrorHandlerMiddleware>>();
            var middleware = new ErrorHandlerMiddleware(next.Object, logger.Object);
            var context = new DefaultHttpContext();

            // Act
            await middleware.Invoke(context);

            // Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
        }
    }
}
