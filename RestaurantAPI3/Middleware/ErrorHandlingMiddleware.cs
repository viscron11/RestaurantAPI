using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RestaurantAPI3.Exceptions;
using System.Net.Http;
using System.Diagnostics;
using System.Threading;
namespace RestaurantAPI3.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        Stopwatch stopwatch = new Stopwatch();
        static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            
            try
            {
                stopwatch.Start();
                Task monitorStopwatchTask = MonitorStopwatchAsync(stopwatch, cancellationTokenSource.Token);
                await next.Invoke(context);
                
                stopwatch.Stop();
                cancellationTokenSource.Cancel();
                await monitorStopwatchTask;
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }
            catch(NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
            
        }
        async Task MonitorStopwatchAsync(Stopwatch sw, CancellationToken cancellationToken)
        {
            bool informed = false;
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000); // Check elapsed time every second

                if (!cancellationToken.IsCancellationRequested && sw.ElapsedMilliseconds > 4000 && !informed) // Check if it's been more than 4 seconds
                {
                    Console.WriteLine("Warning: Operation taking longer than 4 seconds.");
                    LogMessage("Operation takes longer than 4 seconds");
                    informed = true;
                }
            }
        }

        void LogMessage(string message)
        {
            _logger.LogWarning(message);
        }
    }
}
