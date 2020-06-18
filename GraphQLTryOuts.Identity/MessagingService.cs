using GraphQLTryOuts.Messaging.Shared;
using GraphQLTryOuts.Messaging.Shared.Models;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using MQTTnet.Extensions.ManagedClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLTryOuts.Identity
{
    public class MessagingService : IHostedService
    {
        private IMessagingProvider _mqttProvider;

        public MessagingService(IMessagingProvider mqttProvider)
        {
            _mqttProvider = mqttProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var client = _mqttProvider.MessagingClient;
            await client.SubscribeToUserMessages(HandleUserMessageReceived);
        }

        private Task HandleUserMessageReceived(User user)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _mqttProvider.Dispose();
            return Task.CompletedTask;
        }
    }
}
