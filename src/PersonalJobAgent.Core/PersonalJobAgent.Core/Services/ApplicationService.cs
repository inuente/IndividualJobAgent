using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalJobAgent.Core.Interfaces;
using PersonalJobAgent.Core.Models;
using PersonalJobAgent.Data.Repositories; 

namespace PersonalJobAgent.Core.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IJobListingRepository _jobListingRepository;
        private readonly IAIService _aiService;

        public ApplicationService(
            IApplicationRepository applicationRepository,
            IUserProfileRepository userProfileRepository,
            IJobListingRepository jobListingRepository,
            IAIService aiService)
        {
            _applicationRepository = applicationRepository;
            _userProfileRepository = userProfileRepository;
            _jobListingRepository = jobListingRepository;
            _aiService = aiService;
        }

        public async Task<IEnumerable<Application>> GetApplicationsAsync(int userProfileId)
        {
            return await _applicationRepository.GetByUserProfileIdAsync(userProfileId);
        }

        public async Task<IEnumerable<Application>> GetRecentApplicationsAsync(int userProfileId, int count = 5)
        {
            var applications = await _applicationRepository.GetByUserProfileIdAsync(userProfileId);
            return applications.OrderByDescending(a => a.ApplicationDate).Take(count);
        }

        public async Task<IEnumerable<Application>> GetApplicationsByStatusAsync(int userProfileId, string status)
        {
            var applications = await _applicationRepository.GetByUserProfileIdAsync(userProfileId);
            return applications.Where(a => a.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Application> GetApplicationAsync(int applicationId)
        {
            return await _applicationRepository.GetByIdAsync(applicationId);
        }

        public async Task<Application> CreateApplicationAsync(int userProfileId, int jobListingId, string coverLetter = null, string notes = null)
        {
            var application = new Application
            {
                UserProfileId = userProfileId,
                JobListingId = jobListingId,
                ApplicationDate = DateTime.Now,
                Status = "Applied",
                CoverLetter = coverLetter,
                Notes = notes,
                LastUpdated = DateTime.Now
            };

            await _applicationRepository.AddAsync(application);
            return application;
        }

        public async Task<Application> UpdateApplicationStatusAsync(int applicationId, string status, string notes = null)
        {
            var application = await _applicationRepository.GetByIdAsync(applicationId);
            if (application != null)
            {
                application.Status = status;
                if (!string.IsNullOrEmpty(notes))
                {
                    application.Notes = string.IsNullOrEmpty(application.Notes)
                        ? notes
                        : $"{application.Notes}\n\n{DateTime.Now:g} - {status}:\n{notes}";
                }
                application.LastUpdated = DateTime.Now;

                await _applicationRepository.UpdateAsync(application);
            }

            return application;
        }

        public async Task<string> GenerateCoverLetterAsync(int userProfileId, int jobListingId)
        {
            var userProfile = await _userProfileRepository.GetByIdAsync(userProfileId);
            var jobListing = await _jobListingRepository.GetByIdAsync(jobListingId);

            if (userProfile == null || jobListing == null)
            {
                return null;
            }

            // Convert to JSON for AI service
            var userProfileJson = System.Text.Json.JsonSerializer.Serialize(userProfile);
            var jobListingJson = System.Text.Json.JsonSerializer.Serialize(jobListing);

            // Generate cover letter using AI service
            return await _aiService.GenerateCoverLetterAsync(userProfileJson, jobListingJson);
        }

        public async Task<string> GetInterviewPreparationAsync(int applicationId)
        {
            var application = await _applicationRepository.GetByIdAsync(applicationId);
            if (application == null)
            {
                return null;
            }

            var userProfile = await _userProfileRepository.GetByIdAsync(application.UserProfileId);
            var jobListing = await _jobListingRepository.GetByIdAsync(application.JobListingId);

            if (userProfile == null || jobListing == null)
            {
                return null;
            }

            // Convert to JSON for AI service
            var userProfileJson = System.Text.Json.JsonSerializer.Serialize(userProfile);
            var jobListingJson = System.Text.Json.JsonSerializer.Serialize(jobListing);

            // Generate interview preparation using AI service
            return await _aiService.PrepareInterviewAsync(userProfileJson, jobListingJson);
        }
    }
}
