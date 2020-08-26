using System;
using System.Net.Http;
using Alopeyk.Net.Enums;

namespace Alopeyk.Net.AspNet
{
    public class AlopeykConfiguration
    {
        public AlopeykEnvironments Environment { get; set; } = AlopeykEnvironments.Production;
        
        public Uri RemoteApiUri { get; set; }
        
        public string Token { get; set; }

        public Func<IServiceProvider, HttpClient> HttpClientFactory { get; set; }
        
        public IJsonSerializer JsonSerializer { get; set; }
    }
}