using GraphQLTryOuts.Messaging.Shared.Models;
using MQTTnet;
using MQTTnet.Client.Receiving;
using MQTTnet.Extensions.ManagedClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLTryOuts.Messaging.Shared
{
    public class MqttMessagingClient : IMessagingClient, IMqttApplicationMessageReceivedHandler
    {
        private readonly MessagingSettings _settings;

        public MqttMessagingClient(MessagingSettings settings, IManagedMqttClient client)
        {
            InternalClient = client;
            InternalClient.ApplicationMessageReceivedHandler = this;
            _settings = settings;
        }

        public IManagedMqttClient InternalClient { get; private set; }

        protected Func<User, Task> UserMessageHandler { get; set; }
        public Func<string, Task> DefaultMessageHandler { get; set; }

        public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            switch (eventArgs.ApplicationMessage.ContentType)
            {
                case nameof(User):
                    await UserMessageHandler?.Invoke(JsonConvert.DeserializeObject<User>(eventArgs.ApplicationMessage.ConvertPayloadToString()));
                    break;
                default:
                    await DefaultMessageHandler?.Invoke(eventArgs.ApplicationMessage.ConvertPayloadToString());
                    break;
            }
        }

        public async Task PublishMessage<T>(T messagePayload, string topic) where T: class
        {
            var appMessage = new MqttApplicationMessageBuilder()
                        .WithContentType(typeof(T).Name)
                        .WithPayload(JsonConvert.SerializeObject(messagePayload))
                        .WithTopic(topic)
                        .Build();

            await InternalClient.PublishAsync(appMessage, CancellationToken.None);
        }

        public async Task SubscribeToUserMessages(Func<User, Task> userMessageHandler)
        {
            await InternalClient.SubscribeAsync(_settings.UserMessagesTopic);
            UserMessageHandler = userMessageHandler;
        }

        public async Task UnsubscribeFromUserMessages()
        {
            await InternalClient.UnsubscribeAsync(_settings.UserMessagesTopic);
            UserMessageHandler = null;
        }

        public async void Dispose()
        {
            await InternalClient.StopAsync();
        }
    }
}
