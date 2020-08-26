using System;
using System.Net.Http;
using Alopeyk.Net.Enums;

namespace Alopeyk.Net.AspNet
{
    public class AlopeykConfiguration
    {
        internal RetryBuilder RetryBuilder { get; private set; }
        
        public AlopeykEnvironments Environment { get; set; } = AlopeykEnvironments.Production;
        
        public Uri RemoteServiceUri { get; set; }
        
        public string Token { get; set; }

        public Func<IServiceProvider, HttpClient> HttpClientFactory { get; set; }
        
        public IJsonSerializer JsonSerializer { get; set; }

        public RetryBuilder AddRetry()
        {
            RetryBuilder = new RetryBuilder();
            return RetryBuilder;
        }
    }
}