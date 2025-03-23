using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalJobAgent.Core.Models
{
    using System;
    using System.Collections.Generic;

    namespace PersonalJobAgent.Core.Models
    {
        /// <summary>
        /// Represents a saved search for job listings in the Personal Job Agent application.
        /// </summary>
        public class SavedSearch
        {
            /// <summary>
            /// Gets or sets the unique identifier for the saved search.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the user profile identifier that this saved search belongs to.
            /// </summary>
            public int UserProfileId { get; set; }

            /// <summary>
            /// Gets or sets the keywords used for the search.
            /// </summary>
            public string[] Keywords { get; set; }

            /// <summary>
            /// Gets or sets the location filter for the search.
            /// </summary>
            public string Location { get; set; }

            /// <summary>
            /// Gets or sets the name of the saved search.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the date when the search was created.
            /// </summary>
            public DateTime CreatedDate { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether notifications are enabled for this saved search.
            /// </summary>
            public bool NotificationsEnabled { get; set; }

            /// <summary>
            /// Gets or sets the frequency of notifications (in days).
            /// </summary>
            public int? NotificationFrequency { get; set; }

            /// <summary>
            /// Gets or sets the date when the saved search was last executed.
            /// </summary>
            public DateTime? LastExecuted { get; set; }

            /// <summary>
            /// Gets or sets the navigation property to the user profile.
            /// </summary>
            public UserProfile UserProfile { get; set; }

            /// <summary>
            /// Gets or sets the navigation property to the job listings found by this saved search.
            /// </summary>
            public ICollection<JobListing> JobListings { get; set; } = new List<JobListing>();
        }
    }

}
