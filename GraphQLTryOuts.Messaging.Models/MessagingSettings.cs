using System.IO;

namespace GraphQLTryOuts.Messaging.Shared
{
    public class MessagingSettings
    {
        public string Username { get; set; }

        public string UserPassword { get; set; }

        public string Server { get; set; }

        public string ClientId { get; set; }

        public string UserMessagesTopic { get; set; } = Constants.DefaultUserMessagesTopic;
    }
}