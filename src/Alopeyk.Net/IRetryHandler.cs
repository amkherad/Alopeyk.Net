using System;
using System.Threading;
using System.Threading.Tasks;

namespace Alopeyk.Net
{
    public interface IRetryHandler
    {
        Task<object> BeginTry(
            CancellationToken cancellationToken
        );

        Task EndTry(
            object retryContext,
            CancellationToken cancellationToken
        );

        Task<bool> CatchException(
            object retryContext,
            Exception exception,
            CancellationToken cancellationToken
        );
    }
}