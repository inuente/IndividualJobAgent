using System;

namespace PersonalJobAgent.Core.Models
{
    /// <summary>
    /// Represents a job application in the Personal Job Agent application.
    /// </summary>
    public class Application
    {
        /// <summary>
        /// Gets or sets the unique identifier for the application.
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the profile ID this application belongs to.
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Gets or sets the job ID this application is for.
        /// </summary>
        public int JobId { get; set; }

        /// <summary>
        /// Gets or sets the date when the application was submitted.
        /// </summary>
        public DateTime ApplicationDate { get; set; }

        /// <summary>
        /// Gets or sets the status of the application (e.g., Applied, Interviewing, Rejected, Offered).
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the resume version used for this application.
        /// </summary>
        public int? ResumeVersion { get; set; }

        /// <summary>
        /// Gets or sets the cover letter version used for this application.
        /// </summary>
        public int? CoverLetterVersion { get; set; }

        /// <summary>
        /// Gets or sets notes about the application.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the date when the status was last updated.
        /// </summary>
        public DateTime LastStatusUpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the user profile this application belongs to.
        /// </summary>
        public UserProfile UserProfile { get; set; }

        /// <summary>
        /// Gets or sets the job listing this application is for.
        /// </summary>
        public JobListing JobListing { get; set; }
    }
}
