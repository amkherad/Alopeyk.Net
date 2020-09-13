using System;

namespace Alopeyk.Net.AspNet
{
    public class RetryBuilder
    {
        private int _retryCount = 1;
        private TimeSpan? _delay;

        public RetryBuilder AddDelay(
            TimeSpan timeSpan
        )
        {
            _delay = timeSpan;

            return this;
        }

        public RetryBuilder SetRetryCount(
            int count
        )
        {
            _retryCount = count;

            return this;
        }

        public IRetryHandler CreateHandler()
        {
            if (_delay is null)
            {
                return new RetryHandler(_retryCount);
            }

            return new RetryHandler(_retryCount, _delay.Value);
        }
    }
}