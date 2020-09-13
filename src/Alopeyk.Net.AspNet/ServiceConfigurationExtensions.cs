using System;
using System.Net.Http;
using Alopeyk.Net;
using Alopeyk.Net.AspNet;
using Alopeyk.Net.Enums;

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
            var config = sp.GetRequiredService<AlopeykConfiguration>();
            if (config is null)
            {
                throw new InvalidOperationException();
            }

            var remoteUri = config.RemoteServiceUri ?? new Uri(
                config.Environment == AlopeykEnvironments.Production
                    ? AlopeykConfiguration.ProductionApiEndpoint
                    : AlopeykConfiguration.SandboxApiEndpoint
            );

            var httpClient = config.HttpClientFactory?.Invoke(sp) ?? new HttpClient
            {
                Timeout = config.Timeout,
            };

            var jsonSerializer = config.JsonSerializer;

            if (jsonSerializer is null)
            {
                throw new AlopeykException("JsonSerializer should not be empty, you could set it to 'new AlopeykJsonNetJsonSerializer()' from Alopey.Net.JsonNet");
            }

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