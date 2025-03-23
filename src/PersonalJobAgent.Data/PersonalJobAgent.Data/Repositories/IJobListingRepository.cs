using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalJobAgent.Core.Models;

namespace PersonalJobAgent.Data.Repositories
{
    /// <summary>
    /// Interface for JobListing repository operations
    /// </summary>
    public interface IJobListingRepository : IRepository<JobListing>
    {
        /// <summary>
        /// Searches job listings by keywords
        /// </summary>
        /// <param name="keywords">Keywords to search for</param>
        /// <param name="pageNumber">Page number for pagination</param>
        /// <param name="pageSize">Page size for pagination</param>
        /// <returns>Collection of job listings matching the search criteria</returns>
        Task<IEnumerable<JobListing>> SearchAsync(string[] keywords, int pageNumber = 1, int pageSize = 20);
        
        /// <summary>
        /// Gets job listings by location
        /// </summary>
        /// <param name="location">Location to filter by</param>
        /// <param name="radiusInMiles">Radius in miles from the location</param>
        /// <returns>Collection of job listings in the specified location</returns>
        Task<IEnumerable<JobListing>> GetByLocationAsync(string location, int radiusInMiles = 25);
        
        /// <summary>
        /// Gets job listings by required skills
        /// </summary>
        /// <param name="skillIds">Collection of skill identifiers</param>
        /// <returns>Collection of job listings requiring the specified skills</returns>
        Task<IEnumerable<JobListing>> GetByRequiredSkillsAsync(IEnumerable<int> skillIds);
        
        /// <summary>
        /// Gets recommended job listings for a user profile
        /// </summary>
        /// <param name="userProfileId">User profile identifier</param>
        /// <param name="count">Number of recommendations to return</param>
        /// <returns>Collection of recommended job listings</returns>
        Task<IEnumerable<JobListing>> GetRecommendedForUserAsync(int userProfileId, int count = 10);
    }
}
