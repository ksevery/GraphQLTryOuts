using HotChocolate.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTryOuts.Users.Models
{
    public class User
    {
        public string? Username { get; set; }

        public string? Id { get; set; }
    }
}
