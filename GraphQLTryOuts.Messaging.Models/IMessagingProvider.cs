using MQTTnet.Extensions.ManagedClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTryOuts.Messaging.Shared
{
    public interface IMessagingProvider : IDisposable
    {
        IMessagingClient MessagingClient { get; }
    }
}
