using GraphQLTryOuts.Messaging.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLTryOuts.Messaging.Shared
{
    public interface IMessagingClient : IDisposable
    {
        Task SubscribeToUserMessages(Func<User, Task> userMessageHandler);
        Task UnsubscribeFromUserMessages();

        Func<string, Task> DefaultMessageHandler { get; set; }

        Task PublishMessage<T>(T messagePayload, string topic) where T : class;
    }
}
