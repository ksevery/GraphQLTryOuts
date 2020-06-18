using GraphQLTryOuts.Identity.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTryOuts.Identity.Models
{
    public class SignupResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public SignupResponse() { }

        public SignupResponse(AppUser user)
        {
            Id = user.Id;
            Email = user.Email;
        }
    }
}
