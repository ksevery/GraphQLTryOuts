using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLTryOuts.Messaging.Shared.Models
{
    public class Meeting
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CreatorId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
