using System;

namespace PersonalJobAgent.Core.Models
{
    /// <summary>
    /// Represents a job listing in the Personal Job Agent application.
    /// </summary>
    public class JobListing
    {
        /// <summary>
        /// Gets or sets the unique identifier for the job listing.
        /// </summary>
        public int JobId { get; set; }

        /// <summary>
        /// Gets or sets the job title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets the job location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the job description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the job type (e.g., Full-time, Part-time, Contract).
        /// </summary>
        public string JobType { get; set; }

        /// <summary>
        /// Gets or sets the salary information.
        /// </summary>
        public string Salary { get; set; }

        /// <summary>
        /// Gets or sets the date when the job was posted.
        /// </summary>
        public DateTime? PostedDate { get; set; }

        /// <summary>
        /// Gets or sets the closing date for applications.
        /// </summary>
        public DateTime? ClosingDate { get; set; }

        /// <summary>
        /// Gets or sets the source platform (e.g., LinkedIn, HeadHunter, AllJobs).
        /// </summary>
        public string SourcePlatform { get; set; }

        /// <summary>
        /// Gets or sets the external job ID from the source platform.
        /// </summary>
        public string ExternalJobId { get; set; }

        /// <summary>
        /// Gets or sets the URL to the job listing.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this job listing is active.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
