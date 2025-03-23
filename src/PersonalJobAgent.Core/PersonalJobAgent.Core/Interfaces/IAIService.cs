using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalJobAgent.Core.Models;

namespace PersonalJobAgent.Core.Interfaces
{
    /// <summary>
    /// Interface for AI service integration.
    /// </summary>
    public interface IAIService
    {
        /// <summary>
        /// Parses a resume and extracts structured information.
        /// </summary>
        /// <param name="resumeText">The resume text content.</param>
        /// <returns>Structured resume information.</returns>
        Task<Dictionary<string, object>> ParseResumeAsync(string resumeText);

        /// <summary>
        /// Matches a user profile with job listings.
        /// </summary>
        /// <param name="profile">The user profile.</param>
        /// <param name="jobListings">The job listings to match against.</param>
        /// <returns>Job listings with match scores.</returns>
        Task<IEnumerable<object>> MatchJobsAsync(UserProfile profile, IEnumerable<JobListing> jobListings);

        /// <summary>
        /// Generates a tailored resume for a specific job.
        /// </summary>
        /// <param name="profile">The user profile.</param>
        /// <param name="job">The target job.</param>
        /// <returns>Tailored resume content.</returns>
        Task<string> GenerateTailoredResumeAsync(UserProfile profile, JobListing job);

        /// <summary>
        /// Generates a cover letter for a specific job.
        /// </summary>
        /// <param name="profile">The user profile.</param>
        /// <param name="job">The target job.</param>
        /// <returns>Cover letter content.</returns>
        Task<string> GenerateCoverLetterAsync(UserProfile profile, JobListing job);

        /// <summary>
        /// Generates interview questions for a specific job.
        /// </summary>
        /// <param name="job">The job listing.</param>
        /// <param name="count">Number of questions to generate.</param>
        /// <returns>List of interview questions.</returns>
        Task<IEnumerable<string>> GenerateInterviewQuestionsAsync(JobListing job, int count = 10);

        /// <summary>
        /// Analyzes a job description and extracts key requirements.
        /// </summary>
        /// <param name="jobDescription">The job description text.</param>
        /// <returns>Structured job requirements.</returns>
        Task<Dictionary<string, object>> AnalyzeJobDescriptionAsync(string jobDescription);

        /// <summary>
        /// Analyzes user's skills gap for a specific job.
        /// </summary>
        /// <param name="profile">The user profile.</param>
        /// <param name="job">The target job.</param>
        /// <returns>Skills gap analysis.</returns>
        Task<Dictionary<string, object>> AnalyzeSkillsGapAsync(UserProfile profile, JobListing job);

        /// <summary>
        /// Provides feedback on a resume.
        /// </summary>
        /// <param name="resumeText">The resume text content.</param>
        /// <returns>Resume feedback.</returns>
        Task<Dictionary<string, object>> ProvideResumeFeedbackAsync(string resumeText);

        /// <summary>
        /// Provides feedback on a cover letter.
        /// </summary>
        /// <param name="coverLetterText">The cover letter text content.</param>
        /// <param name="jobDescription">The job description text.</param>
        /// <returns>Cover letter feedback.</returns>
        Task<Dictionary<string, object>> ProvideCoverLetterFeedbackAsync(string coverLetterText, string jobDescription);
    }
}
