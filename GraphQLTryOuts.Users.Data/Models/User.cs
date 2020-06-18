using System.ComponentModel.DataAnnotations;

namespace GraphQLTryOuts.Users.Data.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
