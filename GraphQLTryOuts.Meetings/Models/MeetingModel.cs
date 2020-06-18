using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTryOuts.Meetings.Models
{
    public class MeetingModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CreatorId { get; set; }

        public IEnumerable<string> UserIds { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
