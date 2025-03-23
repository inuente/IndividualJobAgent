using System;
using System.Collections.Generic;

namespace PersonalJobAgent.Core.Models
{
    /// <summary>
    /// Represents a user profile in the Personal Job Agent application.
    /// </summary>
  
        public class UserProfile
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Location { get; set; }
            public string Summary { get; set; }
            public DateTime LastUpdated { get; set; }

            // Navigation properties
            public ICollection<Skill> Skills { get; set; } = new List<Skill>();
            public ICollection<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();
            public ICollection<Education> Education { get; set; } = new List<Education>();
            public ICollection<Application> Applications { get; set; } = new List<Application>();
            public ICollection<PlatformCredential> PlatformCredentials { get; set; } = new List<PlatformCredential>();
        }
    
}
