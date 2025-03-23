using System;

namespace PersonalJobAgent.Core.Models
{
    /// <summary>
    /// Represents a work experience entry in the Personal Job Agent application.
    /// </summary>
    public class WorkExperience
    {
        /// <summary>
        /// Gets or sets the unique identifier for the work experience.
        /// </summary>
        public int ExperienceId { get; set; }

        /// <summary>
        /// Gets or sets the profile ID this work experience belongs to.
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the job title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the location of the job.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the start date of the job.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the job (null if current position).
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is the current position.
        /// </summary>
        public bool IsCurrentPosition { get; set; }

        /// <summary>
        /// Gets or sets the job description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the user profile this work experience belongs to.
        /// </summary>
        public UserProfile UserProfile { get; set; }

        /// <summary>
        /// Gets the duration of the work experience in years.
        /// </summary>
        public decimal DurationInYears
        {
            get
            {
                var end = EndDate ?? DateTime.Now;
                var totalDays = (end - StartDate).TotalDays;
                return (decimal)(totalDays / 365.25);
            }
        }
    }
}
