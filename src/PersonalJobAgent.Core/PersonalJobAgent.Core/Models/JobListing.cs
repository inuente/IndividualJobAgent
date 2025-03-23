using System;

namespace PersonalJobAgent.Core.Models
{
    /// <summary>
    /// Represents a job listing in the Personal Job Agent application.
    /// </summary>
    public class JobListing
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }
        public string JobType { get; set; }
        public string Source { get; set; }
        public string ExternalId { get; set; }
        public string Url { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        // Navigation property
        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }

}
