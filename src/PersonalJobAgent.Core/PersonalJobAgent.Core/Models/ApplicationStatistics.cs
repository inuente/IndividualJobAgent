using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalJobAgent.Core.Models
{
    public class ApplicationStatistics
    {
        public int TotalApplications { get; set; }
        public int PendingApplications { get; set; }
        public int InterviewsScheduled { get; set; }
        public int OffersReceived { get; set; }
        public int Rejected { get; set; }
        public Dictionary<string, int> ApplicationsByStatus { get; set; } = new Dictionary<string, int>();
    }

}
