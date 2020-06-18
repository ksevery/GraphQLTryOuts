using GraphQLTryOuts.Messaging.Shared;
using GraphQLTryOuts.Messaging.Shared.Models;
using GraphQLTryOuts.Users.Data;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLTryOuts.Users
{
    public class MessagingService : IHostedService
    {
        private IMessagingProvider _mqttProvider;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MessagingService(IMessagingProvider mqttProvider, IServiceScopeFactory serviceScopeFactory)
        {
            _mqttProvider = mqttProvider;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var client = _mqttProvider.MessagingClient;
            await client.SubscribeToUserMessages(HandleUserMessageReceived);
        }

        private async Task HandleUserMessageReceived(User user)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
            var existingUser = dbContext.Users.Find(user.Id);
            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.Email = user.Email;
                dbContext.Update(existingUser);
            }
            else
            {
                var newDbUser = new Data.Models.User
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email
                };

                dbContext.Users.Add(newDbUser);
            }

            await dbContext.SaveChangesAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _mqttProvider.Dispose();
            return Task.CompletedTask;
        }
    }
}