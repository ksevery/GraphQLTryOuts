using GraphQLTryOuts.Meetings.Data;
using GraphQLTryOuts.Meetings.Data.Models;
using GraphQLTryOuts.Meetings.Models;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTryOuts.Meetings.GraphQL
{
    public class Query
    {
        private IHttpContextAccessor _httpContextAccessor;

        public Query(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        public IEnumerable<MeetingModel> GetAllMeetings([Service]MeetingsDbContext dbContext)
        {
            return dbContext.Meetings.Select(m => new MeetingModel
            {
                Id = m.Id,
                Name = m.Name,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                CreatorId = m.CreatorId,
                UserIds = m.UsersInMeeting.Select(um => um.UserId)
            })
                .ToList();
        }
    }
}
