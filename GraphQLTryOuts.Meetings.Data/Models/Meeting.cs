using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GraphQLTryOuts.Meetings.Data.Models
{
    public class Meeting
    {
        public Meeting()
        {
            UsersInMeeting = new HashSet<UserMeeting>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string CreatorId { get; set; }

        public User Creator { get; set; }

        public ICollection<UserMeeting> UsersInMeeting { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
