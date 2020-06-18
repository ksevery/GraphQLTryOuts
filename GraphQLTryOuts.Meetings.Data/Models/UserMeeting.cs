using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLTryOuts.Meetings.Data.Models
{
    public class UserMeeting
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int MeetingId { get; set; }

        public Meeting Meeting { get; set; }
    }
}
