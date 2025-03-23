using System;

namespace PersonalJobAgent.Core.Models
{
    /// <summary>
    /// Represents platform credentials in the Personal Job Agent application.
    /// </summary>
    public class PlatformCredential
    {
        /// <summary>
        /// Gets or sets the unique identifier for the credential.
        /// </summary>
        public int CredentialId { get; set; }

        /// <summary>
        /// Gets or sets the profile ID this credential belongs to.
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Gets or sets the platform name (e.g., LinkedIn, HeadHunter, AllJobs).
        /// </summary>
        public string PlatformName { get; set; }

        /// <summary>
        /// Gets or sets the username for the platform.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the encrypted password for the platform.
        /// </summary>
        public byte[] EncryptedPassword { get; set; }

        /// <summary>
        /// Gets or sets the access token for the platform API.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the refresh token for the platform API.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the token expiry date.
        /// </summary>
        public DateTime? TokenExpiry { get; set; }

        /// <summary>
        /// Gets or sets the date when the platform was last synchronized.
        /// </summary>
        public DateTime? LastSyncDate { get; set; }

        /// <summary>
        /// Gets or sets the user profile this credential belongs to.
        /// </summary>
        public UserProfile UserProfile { get; set; }
    }
}
