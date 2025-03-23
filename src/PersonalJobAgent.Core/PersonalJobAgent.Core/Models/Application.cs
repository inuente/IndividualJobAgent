using System;

namespace PersonalJobAgent.Core.Models
{
    /// <summary>
    /// Represents a job application in the Personal Job Agent application.
    /// </summary>
    public class Application
    {
        public int Id { get; set; }
        public DateTime ApplicationDate { get; set; } // Changed from AppliedDate
        public string Status { get; set; }
        public string CoverLetter { get; set; }
        public string Notes { get; set; }
        public DateTime LastUpdated { get; set; }

        // Foreign keys
        public int UserProfileId { get; set; }
        public int JobListingId { get; set; }

        // Navigation properties
        public UserProfile UserProfile { get; set; }
        public JobListing JobListing { get; set; }
    }

}
