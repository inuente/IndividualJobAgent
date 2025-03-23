using System;

namespace PersonalJobAgent.Core.Models
{
    /// <summary>
    /// Represents an education entry in the Personal Job Agent application.
    /// </summary>
    public class Education
    {
        public int Id { get; set; }
        public string Institution { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }

        // Foreign key
        public int UserProfileId { get; set; }

        // Navigation property
        public UserProfile UserProfile { get; set; }
    }

}
