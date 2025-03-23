using System;

namespace PersonalJobAgent.Core.Models
{
    /// <summary>
    /// Represents an education entry in the Personal Job Agent application.
    /// </summary>
    public class Education
    {
        /// <summary>
        /// Gets or sets the unique identifier for the education entry.
        /// </summary>
        public int EducationId { get; set; }

        /// <summary>
        /// Gets or sets the profile ID this education entry belongs to.
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Gets or sets the name of the educational institution.
        /// </summary>
        public string Institution { get; set; }

        /// <summary>
        /// Gets or sets the degree obtained.
        /// </summary>
        public string Degree { get; set; }

        /// <summary>
        /// Gets or sets the field of study.
        /// </summary>
        public string FieldOfStudy { get; set; }

        /// <summary>
        /// Gets or sets the start date of the education.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the education.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the GPA (Grade Point Average).
        /// </summary>
        public decimal? GPA { get; set; }

        /// <summary>
        /// Gets or sets the description or additional information about the education.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the user profile this education entry belongs to.
        /// </summary>
        public UserProfile UserProfile { get; set; }
    }
}
