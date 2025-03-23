using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalJobAgent.Core.Models;

namespace PersonalJobAgent.Data.Repositories
{
    /// <summary>
    /// Interface for Application repository operations
    /// </summary>
    public interface IApplicationRepository : IRepository<Application>
    {
        /// <summary>
        /// Gets applications by user profile identifier
        /// </summary>
        /// <param name="userProfileId">User profile identifier</param>
        /// <returns>Collection of applications for the specified user</returns>
        Task<IEnumerable<Application>> GetByUserProfileIdAsync(int userProfileId);
        
        /// <summary>
        /// Gets applications by status
        /// </summary>
        /// <param name="userProfileId">User profile identifier</param>
        /// <param name="status">Application status</param>
        /// <returns>Collection of applications with the specified status</returns>
        Task<IEnumerable<Application>> GetByStatusAsync(int userProfileId, string status);
        
        /// <summary>
        /// Gets applications with related job listing data
        /// </summary>
        /// <param name="userProfileId">User profile identifier</param>
        /// <returns>Collection of applications with job listing data</returns>
        Task<IEnumerable<Application>> GetWithJobListingsAsync(int userProfileId);
        
        /// <summary>
        /// Updates application status
        /// </summary>
        /// <param name="applicationId">Application identifier</param>
        /// <param name="status">New status</param>
        /// <param name="notes">Optional notes about the status change</param>
        Task UpdateStatusAsync(int applicationId, string status, string notes = null);
    }
}
