using System;
using System.Net.Http;
using Alopeyk.Net;
using Alopeyk.Net.AspNet;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceConfigurationExtensions
    {
        public static void AddAlopeyk(
            this IServiceCollection serviceCollection,
            Action<AlopeykConfiguration> config
        )
        {
            if (serviceCollection is null) throw new ArgumentNullException(nameof(serviceCollection));
            if (config is null) throw new ArgumentNullException(nameof(config));

            var settings = new AlopeykConfiguration();

            config(settings);

            serviceCollection.AddSingleton(settings);

            serviceCollection.AddScoped<IAlopeykClient, AlopeykClient>(sp => sp.GetService<AlopeykClient>());
            serviceCollection.AddScoped<AlopeykClient>(AlopeykClientFactory);
        }

        private static AlopeykClient AlopeykClientFactory(
            IServiceProvider sp
        )
        {
            var config = sp.GetService<AlopeykConfiguration>();
            if (config is null)
            {
                throw new InvalidOperationException();
            }

            var remoteUri = config.RemoteServiceUri;
            
            var httpClient = config.HttpClientFactory?.Invoke(sp) ?? new HttpClient
            {
                Timeout = config.Timeout,
            };

            var jsonSerializer = config.JsonSerializer;

            var retryBuilder = config.RetryBuilder;

            return new AlopeykClient(
                remoteUri,
                config.Token,
                httpClient,
                jsonSerializer,
                retryBuilder?.CreateHandler()
            );
        }
    }
}