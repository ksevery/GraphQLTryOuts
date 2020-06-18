using GraphQLTryOuts.Meetings.Data;
using GraphQLTryOuts.Meetings.Data.Models;
using GraphQLTryOuts.Meetings.Models;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GraphQLTryOuts.Meetings.GraphQL
{
    public class Mutation
    {
        private IHttpContextAccessor _httpContextAccessor;

        public Mutation(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        public async Task<MeetingModel> CreateMeetingAsync(MeetingModel newMeeting, [Service]MeetingsDbContext dbContext)
        {
            var newDbMeeting = new Meeting
            {
                Name = newMeeting.Name,
                StartDate = newMeeting.StartDate,
                EndDate = newMeeting.EndDate,
                CreatorId = GetLoggedUserId()
            };

            foreach (var userId in newMeeting.UserIds)
            {
                if(!dbContext.Users.Any(u => u.Id == userId))
                {
                    await dbContext.Users.AddAsync(new User { Id = userId });
                }
            }

            if (!dbContext.Users.Any(u => u.Id == GetLoggedUserId()))
            {
                await dbContext.Users.AddAsync(new User { Id = GetLoggedUserId() });
            }

            await dbContext.SaveChangesAsync();

            foreach (var userId in newMeeting.UserIds)
            {
                newDbMeeting.UsersInMeeting.Add(new UserMeeting { UserId = userId, Meeting = newDbMeeting });
            }

            await dbContext.Meetings.AddAsync(newDbMeeting);
            await dbContext.SaveChangesAsync();

            return new MeetingModel
            {
                Id = newDbMeeting.Id,
                Name = newDbMeeting.Name,
                StartDate = newDbMeeting.StartDate,
                EndDate = newDbMeeting.EndDate,
                CreatorId = newDbMeeting.CreatorId,
                UserIds = newDbMeeting.UsersInMeeting.Select(um => um.UserId)
            };
        }

        private string GetLoggedUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
