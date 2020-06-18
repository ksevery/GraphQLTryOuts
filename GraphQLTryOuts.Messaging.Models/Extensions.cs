using Microsoft.Extensions.DependencyInjection;

namespace GraphQLTryOuts.Messaging.Shared
{
    public static class Extensions
    {
        public static IServiceCollection AddMqttClientProvider(this IServiceCollection services, MessagingSettings messagingSettings)
        {
            services.AddSingleton<IMessagingProvider>(ctx =>
            {
                var mqttProvider = new MqttProvider(messagingSettings);
                mqttProvider.SetupMqtt();

                return mqttProvider;
            });

            return services;
        }
    }
}
