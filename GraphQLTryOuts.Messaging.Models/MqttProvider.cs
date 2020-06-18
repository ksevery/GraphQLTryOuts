using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using System;
using System.Threading.Tasks;

namespace GraphQLTryOuts.Messaging.Shared
{
    public class MqttProvider : IMessagingProvider
    {
        private MessagingSettings _settings;

        public MqttProvider(MessagingSettings settings)
        {
            _settings = settings;
        }

        public IMessagingClient MessagingClient { get; private set; }

        public async Task SetupMqtt()
        {
            var options = new ManagedMqttClientOptionsBuilder()
                            .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                            .WithClientOptions(new MqttClientOptionsBuilder()
                                .WithClientId(_settings.ClientId)
                                .WithCredentials(_settings.Username, _settings.UserPassword)
                                .WithTcpServer(_settings.Server)
                                .WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V500)
                                .Build())
                            .Build();

            var messagingClient = new MqttMessagingClient(_settings, new MqttFactory().CreateManagedMqttClient());
            await messagingClient.InternalClient.StartAsync(options);

            MessagingClient = messagingClient;
        }

        public void Dispose()
        {
            MessagingClient.Dispose();
        }
    }
}
