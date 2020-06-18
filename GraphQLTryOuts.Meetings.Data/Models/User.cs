using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GraphQLTryOuts.Meetings.Data.Models
{
    public class User
    {
        public User()
        {
            MeetingsForUser = new HashSet<UserMeeting>();
        }

        [Key]
        public string Id { get; set; }

        public ICollection<UserMeeting> MeetingsForUser { get; set; }
    }
}
