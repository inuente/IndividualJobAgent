using System;

namespace PersonalJobAgent.Core.Models
{
    /// <summary>
    /// Represents platform credentials in the Personal Job Agent application.
    /// </summary>
    public class PlatformCredential
    {
        public int Id { get; set; }
        public string PlatformName { get; set; } // Changed from Platform
        public string Username { get; set; }
        public string Password { get; set; }
        public string ApiKey { get; set; }
        public DateTime LastUpdated { get; set; }

        // Foreign key
        public int UserProfileId { get; set; }

        // Navigation property
        public UserProfile UserProfile { get; set; }
    }

}
