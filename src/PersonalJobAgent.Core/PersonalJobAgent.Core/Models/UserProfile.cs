using System;
using System.Collections.Generic;

namespace PersonalJobAgent.Core.Models
{
    /// <summary>
    /// Represents a user profile in the Personal Job Agent application.
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// Gets or sets the unique identifier for the profile.
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user's phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the user's location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the user's professional title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the user's professional summary.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the date when the profile was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the profile was last modified.
        /// </summary>
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the collection of skills associated with this profile.
        /// </summary>
        public ICollection<Skill> Skills { get; set; } = new List<Skill>();

        /// <summary>
        /// Gets or sets the collection of work experiences associated with this profile.
        /// </summary>
        public ICollection<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();

        /// <summary>
        /// Gets or sets the collection of education entries associated with this profile.
        /// </summary>
        public ICollection<Education> Educations { get; set; } = new List<Education>();

        /// <summary>
        /// Gets or sets the collection of applications associated with this profile.
        /// </summary>
        public ICollection<Application> Applications { get; set; } = new List<Application>();

        /// <summary>
        /// Gets or sets the collection of platform credentials associated with this profile.
        /// </summary>
        public ICollection<PlatformCredential> PlatformCredentials { get; set; } = new List<PlatformCredential>();

        /// <summary>
        /// Gets the full name of the user.
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";
    }
}
