using System;
using System.Diagnostics;

namespace Restaurants.API.Middlewares
{
    public class RequestTimeLoggingMiddleware : IMiddleware
    {
        const int ONE_SEC_IN_MILISECONDS = 1000;

        private ILogger<RequestTimeLoggingMiddleware> _logger;

        public RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopWatch = Stopwatch.StartNew();

            //_logger.LogInformation("START ---->>>>>>>>>>>>>>>>>");

            //Thread.Sleep(10000);

            await next.Invoke(context);

            stopWatch.Stop();

            if ((stopWatch.ElapsedMilliseconds / ONE_SEC_IN_MILISECONDS) > 4)
            {
                _logger.LogInformation("Request [{Verb}] at {Path} took {Time} ms",
                    context.Request.Method,
                    context.Request.Path,
                    stopWatch.ElapsedMilliseconds
                );
            }
        }
    }
}

