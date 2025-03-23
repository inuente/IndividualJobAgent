using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalJobAgent.Core.Interfaces;
using PersonalJobAgent.Core.Models;
using PersonalJobAgent.Data.Repositories; 

namespace PersonalJobAgent.Core.Services
{
    public class JobDiscoveryService : IJobDiscoveryService
    {
        private readonly IJobListingRepository _jobListingRepository;
        private readonly IAIService _aiService;

        public JobDiscoveryService(IJobListingRepository jobListingRepository, IAIService aiService)
        {
            _jobListingRepository = jobListingRepository;
            _aiService = aiService;
        }

        public async Task<IEnumerable<JobListing>> SearchJobsAsync(string[] keywords, string location = null, int pageNumber = 1, int pageSize = 20)
        {
            // Implementation
            var allJobs = await _jobListingRepository.GetAllAsync();

            // Filter by keywords and location
            var filteredJobs = allJobs.Where(j =>
                keywords.Any(k => j.Title.Contains(k, StringComparison.OrdinalIgnoreCase) ||
                                j.Description.Contains(k, StringComparison.OrdinalIgnoreCase)) &&
                (location == null || j.Location.Contains(location, StringComparison.OrdinalIgnoreCase)))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return filteredJobs;
        }

        public async Task<IEnumerable<JobListing>> GetJobsByLocationAsync(string location, int radiusInMiles = 25, int pageNumber = 1, int pageSize = 20)
        {
            // Implementation
            var allJobs = await _jobListingRepository.GetAllAsync();

            // Filter by location
            var filteredJobs = allJobs.Where(j =>
                j.Location.Contains(location, StringComparison.OrdinalIgnoreCase))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return filteredJobs;
        }

        public async Task<IEnumerable<JobListing>> GetRecommendedJobsAsync(int userProfileId, int count = 10)
        {
            // Implementation
            var allJobs = await _jobListingRepository.GetAllAsync();

            // For now, just return the most recent jobs
            // In a real implementation, this would use the AI service to match jobs to the user profile
            var recommendedJobs = allJobs.OrderByDescending(j => j.PostedDate).Take(count);

            return recommendedJobs;
        }

        public async Task<IEnumerable<JobListing>> GetExternalJobsAsync(string[] platforms, string[] keywords, string location = null, int count = 10)
        {
            // Implementation
            // This would typically call external APIs to get job listings
            // For now, return an empty list
            return new List<JobListing>();
        }

        public async Task<JobListing> SaveJobListingAsync(JobListing jobListing)
        {
            if (jobListing.Id == 0)
            {
                // New job listing
                await _jobListingRepository.AddAsync(jobListing);
            }
            else
            {
                // Existing job listing
                await _jobListingRepository.UpdateAsync(jobListing);
            }

            return jobListing;
        }

        public async Task<JobListing> GetJobListingAsync(int id)
        {
            return await _jobListingRepository.GetByIdAsync(id);
        }
    }
}

