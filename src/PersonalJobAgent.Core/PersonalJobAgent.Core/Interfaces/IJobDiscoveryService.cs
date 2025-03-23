using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalJobAgent.Core.Models;

namespace PersonalJobAgent.Core.Interfaces
{
    /// <summary>
    /// Interface for job discovery service.
    /// </summary>
    public interface IJobDiscoveryService
    {
        /// <summary>
        /// Gets a job listing by ID.
        /// </summary>
        /// <param name="jobId">The job ID.</param>
        /// <returns>The job listing.</returns>
        Task<JobListing> GetJobAsync(int jobId);

        /// <summary>
        /// Searches for job listings based on criteria.
        /// </summary>
        /// <param name="keywords">Keywords to search for.</param>
        /// <param name="location">Location to search in.</param>
        /// <param name="jobType">Type of job (e.g., Full-time, Part-time).</param>
        /// <param name="platforms">Platforms to search on (e.g., LinkedIn, HeadHunter).</param>
        /// <param name="page">Page number for pagination.</param>
        /// <param name="pageSize">Page size for pagination.</param>
        /// <returns>A list of job listings matching the criteria.</returns>
        Task<IEnumerable<JobListing>> SearchJobsAsync(string keywords, string location, string jobType, IEnumerable<string> platforms, int page = 1, int pageSize = 20);

        /// <summary>
        /// Gets job recommendations for a user profile.
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <param name="count">Number of recommendations to return.</param>
        /// <returns>A list of recommended job listings with match scores.</returns>
        Task<IEnumerable<JobListing>> GetRecommendationsAsync(int profileId, int count = 10);

        /// <summary>
        /// Saves a job listing to the database.
        /// </summary>
        /// <param name="job">The job listing to save.</param>
        /// <returns>The saved job listing with ID assigned.</returns>
        Task<JobListing> SaveJobAsync(JobListing job);

        /// <summary>
        /// Updates an existing job listing.
        /// </summary>
        /// <param name="job">The job listing to update.</param>
        /// <returns>The updated job listing.</returns>
        Task<JobListing> UpdateJobAsync(JobListing job);

        /// <summary>
        /// Marks a job listing as inactive.
        /// </summary>
        /// <param name="jobId">The job ID to mark as inactive.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task MarkJobInactiveAsync(int jobId);

        /// <summary>
        /// Synchronizes job listings from external platforms.
        /// </summary>
        /// <param name="profileId">The profile ID to synchronize for.</param>
        /// <param name="platforms">Platforms to synchronize from.</param>
        /// <returns>The number of new job listings found.</returns>
        Task<int> SynchronizeJobsAsync(int profileId, IEnumerable<string> platforms);

        /// <summary>
        /// Creates a saved search for job listings.
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <param name="name">Name of the saved search.</param>
        /// <param name="keywords">Keywords to search for.</param>
        /// <param name="location">Location to search in.</param>
        /// <param name="jobType">Type of job.</param>
        /// <param name="platforms">Platforms to search on.</param>
        /// <returns>The ID of the created saved search.</returns>
        Task<int> CreateSavedSearchAsync(int profileId, string name, string keywords, string location, string jobType, IEnumerable<string> platforms);

        /// <summary>
        /// Gets saved searches for a user profile.
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <returns>A list of saved searches.</returns>
        Task<IEnumerable<object>> GetSavedSearchesAsync(int profileId);

        /// <summary>
        /// Deletes a saved search.
        /// </summary>
        /// <param name="savedSearchId">The saved search ID to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteSavedSearchAsync(int savedSearchId);
    }
}
