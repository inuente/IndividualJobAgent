using System;
using System.Collections.Generic;

namespace PersonalJobAgent.Core.Models
{
    /// <summary>
    /// Represents a skill in the Personal Job Agent application.
    /// </summary>
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public string Category { get; set; }

        // Foreign key
        public int UserProfileId { get; set; }

        // Navigation property
        public UserProfile UserProfile { get; set; }
    }

}
