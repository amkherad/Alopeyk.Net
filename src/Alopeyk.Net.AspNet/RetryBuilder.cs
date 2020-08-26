using System;

namespace Alopeyk.Net.AspNet
{
    public class RetryBuilder
    {
        public RetryBuilder AddDelay(
            TimeSpan timeSpan
        )
        {
            return this;
        }

        public RetryBuilder SetRetryCount(
            int count
        )
        {
            return this;
        }

        public IRetryHandler CreateHandler()
        {
            return null;
        }
    }
}