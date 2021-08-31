using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    public class JustCreatedBuild
    {
        public int Id { get; set; }
        public string BuildNumber { get; set; }
        public DateTime QueueTime { get; set; }
        public DateTime LastChangedDate { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string SourceBranch { get; set; }
        public List<object> Tags { get; set; }
        public Properties Properties { get; set; }
    }
}
