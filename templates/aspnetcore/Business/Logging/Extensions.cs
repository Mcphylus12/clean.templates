using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

// Adapted from https://github.com/nblumhardt/serilog-timings But using microsofts ILogger
namespace Business.Logging
{
    public static class Extensions
    {
        public static Operation<T> TimeOperation<T>(this ILogger<T> logger, string log, params object[] items)
        {
            return new Operation<T>(logger, log, items);
        }
    }

    public class Operation<T> : IDisposable
    {
        private ILogger<T> logger;
        private string log;
        private object[] items;
        private readonly Stopwatch timer;
        private readonly IDisposable logScope;

        public Operation(ILogger<T> logger, string log, object[] items)
        {
            var operationId = Guid.NewGuid();
            this.logger = logger;
            this.log = log;
            this.items = items;
            this.timer = new Stopwatch();

            this.logScope = this.logger.BeginScope(new Dictionary<string, object>() 
            {
                { "operationId", operationId }
            });

            this.timer.Start();
        }

        public void Dispose()
        {
            this.timer.Stop();
            var ms = timer.ElapsedMilliseconds;

            this.logger.LogInformation(this.log + " completed in {ms}ms", this.items, ms);
            logScope.Dispose();
        }
    }
}
