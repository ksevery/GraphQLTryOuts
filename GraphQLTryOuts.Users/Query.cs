using GraphQLTryOuts.Users.Models;
using HotChocolate.AspNetCore.Authorization;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GraphQLTryOuts.Users
{
    public class Query
    {
        private IHttpContextAccessor _httpContextAccessor;

        public Query(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        public User? GetMe()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = _httpContextAccessor.HttpContext.User.FindFirst(JwtClaimTypes.Name)?.Value;

            return new User
            {
                Id = userId,
                Username = username
            };
        }
    }
}
