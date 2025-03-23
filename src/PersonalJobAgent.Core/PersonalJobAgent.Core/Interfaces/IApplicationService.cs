using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalJobAgent.Core.Models;

namespace PersonalJobAgent.Core.Interfaces
{
    /// <summary>
    /// Interface for application service operations
    /// </summary>
    public interface IApplicationService
    {
        /// <summary>
        /// Gets applications for a user profile
        /// </summary>
        /// <param name="userProfileId">User profile ID</param>
        /// <returns>Collection of applications</returns>
        Task<IEnumerable<Application>> GetApplicationsAsync(int userProfileId);
        
        /// <summary>
        /// Gets recent applications for a user profile
        /// </summary>
        /// <param name="userProfileId">User profile ID</param>
        /// <param name="count">Number of applications to return</param>
        /// <returns>Collection of recent applications</returns>
        Task<IEnumerable<Application>> GetRecentApplicationsAsync(int userProfileId, int count = 5);
        
        /// <summary>
        /// Gets applications by status for a user profile
        /// </summary>
        /// <param name="userProfileId">User profile ID</param>
        /// <param name="status">Application status</param>
        /// <returns>Collection of applications with the specified status</returns>
        Task<IEnumerable<Application>> GetApplicationsByStatusAsync(int userProfileId, string status);
        
        /// <summary>
        /// Gets an application by ID
        /// </summary>
        /// <param name="applicationId">Application ID</param>
        /// <returns>Application</returns>
        Task<Application> GetApplicationAsync(int applicationId);
        
        /// <summary>
        /// Creates a new application
        /// </summary>
        /// <param name="userProfileId">User profile ID</param>
        /// <param name="jobListingId">Job listing ID</param>
        /// <param name="coverLetter">Optional cover letter</param>
        /// <param name="notes">Optional notes</param>
        /// <returns>Created application</returns>
        Task<Application> CreateApplicationAsync(int userProfileId, int jobListingId, string coverLetter = null, string notes = null);
        
        /// <summary>
        /// Updates an application's status
        /// </summary>
        /// <param name="applicationId">Application ID</param>
        /// <param name="status">New status</param>
        /// <param name="notes">Optional notes about the status change</param>
        /// <returns>Updated application</returns>
        Task<Application> UpdateApplicationStatusAsync(int applicationId, string status, string notes = null);
        
        /// <summary>
        /// Generates a cover letter for a job application
        /// </summary>
        /// <param name="userProfileId">User profile ID</param>
        /// <param name="jobListingId">Job listing ID</param>
        /// <returns>Generated cover letter</returns>
        Task<string> GenerateCoverLetterAsync(int userProfileId, int jobListingId);
        
        /// <summary>
        /// Gets interview preparation materials for a job application
        /// </summary>
        /// <param name="applicationId">Application ID</param>
        /// <returns>Interview preparation materials as JSON</returns>
        Task<string> GetInterviewPreparationAsync(int applicationId);
    }
}
