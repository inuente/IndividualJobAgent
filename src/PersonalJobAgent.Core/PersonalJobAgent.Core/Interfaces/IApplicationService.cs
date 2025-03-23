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
        // Existing methods
        Task<IEnumerable<Application>> GetApplicationsAsync(int userProfileId);
        Task<IEnumerable<Application>> GetRecentApplicationsAsync(int userProfileId, int count = 5);
        Task<IEnumerable<Application>> GetApplicationsByStatusAsync(int userProfileId, string status);
        Task<Application> GetApplicationAsync(int applicationId);
        Task<Application> CreateApplicationAsync(int userProfileId, int jobListingId, string coverLetter = null, string notes = null);
        Task<Application> UpdateApplicationStatusAsync(int applicationId, string status, string notes = null);
        Task<string> GenerateCoverLetterAsync(int userProfileId, int jobListingId);
        Task<string> GetInterviewPreparationAsync(int applicationId);

        // Add these new methods
        Task<IEnumerable<Application>> GetApplicationsForProfileAsync(int profileId);
        Task<ApplicationStatistics> GetApplicationStatisticsAsync(int profileId);
    }

}
