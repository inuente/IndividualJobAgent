using System;
using System.Threading.Tasks;

namespace PersonalJobAgent.Core.Interfaces
{
    /// <summary>
    /// Interface for AI services
    /// </summary>
    public interface IAIService : IDisposable
    {
        /// <summary>
        /// Parses a resume and extracts structured information
        /// </summary>
        /// <param name="resumeText">Resume text content</param>
        /// <returns>JSON string with parsed resume data</returns>
        Task<string> ParseResumeAsync(string resumeText);
        
        /// <summary>
        /// Matches a user profile with job listings
        /// </summary>
        /// <param name="userProfileJson">User profile as JSON</param>
        /// <param name="jobListingsJson">Job listings as JSON</param>
        /// <returns>JSON string with matching results</returns>
        Task<string> MatchJobsAsync(string userProfileJson, string jobListingsJson);
        
        /// <summary>
        /// Generates a cover letter based on user profile and job listing
        /// </summary>
        /// <param name="userProfileJson">User profile as JSON</param>
        /// <param name="jobListingJson">Job listing as JSON</param>
        /// <returns>Generated cover letter text</returns>
        Task<string> GenerateCoverLetterAsync(string userProfileJson, string jobListingJson);
        
        /// <summary>
        /// Prepares interview questions and tips based on job listing
        /// </summary>
        /// <param name="userProfileJson">User profile as JSON</param>
        /// <param name="jobListingJson">Job listing as JSON</param>
        /// <returns>JSON string with interview preparation data</returns>
        Task<string> PrepareInterviewAsync(string userProfileJson, string jobListingJson);
    }
}
