using System;

namespace PersonalJobAgent.Core.Models
{
    /// <summary>
    /// Represents a work experience entry in the Personal Job Agent application.
    /// </summary>
    public class WorkExperience
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        // Foreign key
        public int UserProfileId { get; set; }

        // Navigation property
        public UserProfile UserProfile { get; set; }
    }

}
