using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalJobAgent.Core.Models;

namespace PersonalJobAgent.Core.Interfaces
{
    /// <summary>
    /// Interface for application tracking service.
    /// </summary>
    public interface IApplicationService
    {
        /// <summary>
        /// Gets an application by ID.
        /// </summary>
        /// <param name="applicationId">The application ID.</param>
        /// <returns>The application.</returns>
        Task<Application> GetApplicationAsync(int applicationId);

        /// <summary>
        /// Gets all applications for a user profile.
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <returns>A list of applications.</returns>
        Task<IEnumerable<Application>> GetApplicationsForProfileAsync(int profileId);

        /// <summary>
        /// Gets applications by status for a user profile.
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <param name="status">The application status.</param>
        /// <returns>A list of applications with the specified status.</returns>
        Task<IEnumerable<Application>> GetApplicationsByStatusAsync(int profileId, string status);

        /// <summary>
        /// Creates a new application.
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <param name="jobId">The job ID.</param>
        /// <param name="resumeVersion">The resume version used.</param>
        /// <param name="coverLetterVersion">The cover letter version used.</param>
        /// <param name="notes">Notes about the application.</param>
        /// <returns>The created application with ID assigned.</returns>
        Task<Application> CreateApplicationAsync(int profileId, int jobId, int? resumeVersion, int? coverLetterVersion, string notes);

        /// <summary>
        /// Updates an application status.
        /// </summary>
        /// <param name="applicationId">The application ID.</param>
        /// <param name="status">The new status.</param>
        /// <param name="notes">Additional notes about the status change.</param>
        /// <returns>The updated application.</returns>
        Task<Application> UpdateApplicationStatusAsync(int applicationId, string status, string notes);

        /// <summary>
        /// Updates application notes.
        /// </summary>
        /// <param name="applicationId">The application ID.</param>
        /// <param name="notes">The new notes.</param>
        /// <returns>The updated application.</returns>
        Task<Application> UpdateApplicationNotesAsync(int applicationId, string notes);

        /// <summary>
        /// Generates a tailored resume for a job application.
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <param name="jobId">The job ID.</param>
        /// <returns>The generated resume content and version number.</returns>
        Task<(string Content, int Version)> GenerateTailoredResumeAsync(int profileId, int jobId);

        /// <summary>
        /// Generates a cover letter for a job application.
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <param name="jobId">The job ID.</param>
        /// <returns>The generated cover letter content and version number.</returns>
        Task<(string Content, int Version)> GenerateCoverLetterAsync(int profileId, int jobId);

        /// <summary>
        /// Gets application statistics for a user profile.
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <returns>Application statistics.</returns>
        Task<object> GetApplicationStatisticsAsync(int profileId);

        /// <summary>
        /// Gets upcoming follow-ups for applications.
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <returns>A list of applications requiring follow-up.</returns>
        Task<IEnumerable<Application>> GetUpcomingFollowUpsAsync(int profileId);

        /// <summary>
        /// Submits an application to an external platform.
        /// </summary>
        /// <param name="applicationId">The application ID.</param>
        /// <returns>True if submission was successful, false otherwise.</returns>
        Task<bool> SubmitApplicationToExternalPlatformAsync(int applicationId);
    }
}
