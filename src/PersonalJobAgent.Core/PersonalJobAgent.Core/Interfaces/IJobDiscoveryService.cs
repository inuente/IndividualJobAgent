using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalJobAgent.Core.Models;
using PersonalJobAgent.Core.Models.PersonalJobAgent.Core.Models;

namespace PersonalJobAgent.Core.Interfaces
{
    /// <summary>
    /// Interface for job discovery service operations
    /// </summary>
    public interface IJobDiscoveryService
    {
        /// <summary>
        /// Creates a saved search for job listings
        /// </summary>
        /// <param name="userProfileId">User profile ID</param>
        /// <param name="keywords">Keywords to search for</param>
        /// <param name="location">Optional location filter</param>
        /// <param name="name">Name of the saved search</param>
        /// <returns>The created saved search</returns>
        Task<SavedSearch> CreateSavedSearchAsync(int userProfileId, string[] keywords, string location = null, string name = null);

        /// <summary>
        /// Searches for job listings based on keywords
        /// </summary>
        /// <param name="keywords">Keywords to search for</param>
        /// <param name="location">Optional location filter</param>
        /// <param name="pageNumber">Page number for pagination</param>
        /// <param name="pageSize">Page size for pagination</param>
        /// <returns>Collection of job listings matching the search criteria</returns>
        Task<IEnumerable<JobListing>> SearchJobsAsync(string[] keywords, string location = null, int pageNumber = 1, int pageSize = 20);
        
        /// <summary>
        /// Gets job listings by location
        /// </summary>
        /// <param name="location">Location to filter by</param>
        /// <param name="radiusInMiles">Radius in miles from the location</param>
        /// <param name="pageNumber">Page number for pagination</param>
        /// <param name="pageSize">Page size for pagination</param>
        /// <returns>Collection of job listings in the specified location</returns>
        Task<IEnumerable<JobListing>> GetJobsByLocationAsync(string location, int radiusInMiles = 25, int pageNumber = 1, int pageSize = 20);
        
        /// <summary>
        /// Gets recommended job listings for a user profile
        /// </summary>
        /// <param name="userProfileId">User profile identifier</param>
        /// <param name="count">Number of recommendations to return</param>
        /// <returns>Collection of recommended job listings</returns>
        Task<IEnumerable<JobListing>> GetRecommendedJobsAsync(int userProfileId, int count = 10);
        
        /// <summary>
        /// Gets job listings from external platforms
        /// </summary>
        /// <param name="platforms">Collection of platform names to search</param>
        /// <param name="keywords">Keywords to search for</param>
        /// <param name="location">Optional location filter</param>
        /// <param name="count">Number of results to return per platform</param>
        /// <returns>Collection of job listings from external platforms</returns>
        Task<IEnumerable<JobListing>> GetExternalJobsAsync(string[] platforms, string[] keywords, string location = null, int count = 10);
        
        /// <summary>
        /// Saves a job listing to the database
        /// </summary>
        /// <param name="jobListing">Job listing to save</param>
        /// <returns>Saved job listing with ID</returns>
        Task<JobListing> SaveJobListingAsync(JobListing jobListing);
        
        /// <summary>
        /// Gets a job listing by ID
        /// </summary>
        /// <param name="id">Job listing ID</param>
        /// <returns>Job listing</returns>
        Task<JobListing> GetJobListingAsync(int id);
    }
}
