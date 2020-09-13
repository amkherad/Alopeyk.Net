using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace Alopeyk.Net
{
    public class RetryHandler : IRetryHandler
    {
        public static readonly Lazy<RetryHandler> NoRetry = new Lazy<RetryHandler>(() => new NoRetryHandler());


        private readonly int _retryCount;
        private TimeSpan? _delay;

        public bool ThrowOnExceptionWhenRetryIsNotPossible { get; set; }


        public RetryHandler(
            int retryCount
        )
        {
            _retryCount = retryCount;
        }

        public RetryHandler(
            int retryCount,
            TimeSpan delay
        )
        {
            _retryCount = retryCount;
            _delay = delay;
        }

        private RetryHandler()
        {
        }


        public virtual async Task<object> BeginTry(
            CancellationToken cancellationToken
        )
        {
            return new RetryHandlerContext
            {
                BeginTimeSnapshot = DateTime.Now
            };
        }

        public virtual async Task EndTry(
            object retryContext,
            CancellationToken cancellationToken
        )
        {
            if (retryContext is null) return;

            if (!(retryContext is RetryHandlerContext ctx))
                throw new AlopeykException("Retry context must be acquired by BeginTry()");

            ctx.Retries = 0;
        }

        public virtual async Task<bool> CatchException(
            object retryContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            if (retryContext is null) throw new ArgumentNullException(nameof(retryContext));
            if (exception is null) throw new ArgumentNullException(nameof(exception));

            if (!(retryContext is RetryHandlerContext ctx))
                throw new AlopeykException("Retry context must be acquired by BeginTry()");

            if (ctx.Retries >= _retryCount)
            {
                if (ThrowOnExceptionWhenRetryIsNotPossible)
                {
                    var exceptionInfo = ExceptionDispatchInfo.Capture(exception);

                    exceptionInfo.Throw();

                    throw new InvalidOperationException();
                }

                return false;
            }

            ctx.Retries++;

            if (!(_delay is null))
            {
                await Task.Delay(_delay.Value, cancellationToken);
            }

            return true;
        }

        private class RetryHandlerContext
        {
            public DateTime BeginTimeSnapshot { get; set; }

            public int Retries { get; set; }
        }

        private class NoRetryHandler : RetryHandler
        {
            public override Task<object> BeginTry(
                CancellationToken cancellationToken
            )
            {
                return Task.FromResult((object) null);
            }

            public override Task EndTry(
                object retryContext,
                CancellationToken cancellationToken
            )
            {
                return Task.CompletedTask;
            }

            public override Task<bool> CatchException(
                object retryContext,
                Exception exception,
                CancellationToken cancellationToken
            )
            {
                if (exception is null) throw new ArgumentNullException(nameof(exception));

                var exceptionInfo = ExceptionDispatchInfo.Capture(exception);

                exceptionInfo.Throw();

                throw new InvalidOperationException();
            }
        }
    }
}