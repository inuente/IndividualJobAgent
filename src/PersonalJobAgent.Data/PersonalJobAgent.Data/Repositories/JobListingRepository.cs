using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalJobAgent.Core.Models;

namespace PersonalJobAgent.Data.Repositories
{
    /// <summary>
    /// Implementation of the JobListing repository interface
    /// </summary>
    public class JobListingRepository : Repository<JobListing>, IJobListingRepository
    {
        private readonly PersonalJobAgentDbContext _dbContext;

        public JobListingRepository(PersonalJobAgentDbContext dbContext) 
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<JobListing>> SearchAsync(string[] keywords, int pageNumber = 1, int pageSize = 20)
        {
            var query = _dbContext.JobListings.AsQueryable();
            
            if (keywords != null && keywords.Length > 0)
            {
                foreach (var keyword in keywords)
                {
                    var searchTerm = keyword.ToLower();
                    query = query.Where(j => 
                        j.Title.ToLower().Contains(searchTerm) || 
                        j.Description.ToLower().Contains(searchTerm) || 
                        j.Company.ToLower().Contains(searchTerm) ||
                        j.Location.ToLower().Contains(searchTerm));
                }
            }
            
            return await query
                .OrderByDescending(j => j.PostedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<JobListing>> GetByLocationAsync(string location, int radiusInMiles = 25)
        {
            // In a real implementation, this would use geospatial queries
            // For simplicity, we're just doing a string match on the location field
            var locationLower = location.ToLower();
            
            return await _dbContext.JobListings
                .Where(j => j.Location.ToLower().Contains(locationLower))
                .OrderByDescending(j => j.PostedDate)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<JobListing>> GetByRequiredSkillsAsync(IEnumerable<int> skillIds)
        {
            // This assumes there's a many-to-many relationship between JobListings and Skills
            // In a real implementation, you would have a JobListingSkill junction table
            
            // For simplicity, we're just checking if any of the required skills are in the job description
            var skills = await _dbContext.Skills
                .Where(s => skillIds.Contains(s.Id))
                .ToListAsync();
                
            var jobListings = new List<JobListing>();
            
            foreach (var skill in skills)
            {
                var matchingJobs = await _dbContext.JobListings
                    .Where(j => j.Description.ToLower().Contains(skill.Name.ToLower()))
                    .ToListAsync();
                    
                jobListings.AddRange(matchingJobs);
            }
            
            // Remove duplicates and return
            return jobListings
                .GroupBy(j => j.Id)
                .Select(g => g.First())
                .OrderByDescending(j => j.PostedDate);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<JobListing>> GetRecommendedForUserAsync(int userProfileId, int count = 10)
        {
            // In a real implementation, this would use the AI service to get recommendations
            // For now, we'll just return the most recent job listings
            
            var userProfile = await _dbContext.UserProfiles
                .Include(u => u.Skills)
                .FirstOrDefaultAsync(u => u.Id == userProfileId);
                
            if (userProfile == null)
                return Enumerable.Empty<JobListing>();
                
            // Get job listings that match any of the user's skills
            var jobListings = new List<JobListing>();
            
            foreach (var skill in userProfile.Skills)
            {
                var matchingJobs = await _dbContext.JobListings
                    .Where(j => j.Description.ToLower().Contains(skill.Name.ToLower()))
                    .ToListAsync();
                    
                jobListings.AddRange(matchingJobs);
            }
            
            // Remove duplicates, sort by relevance (for now, just by date), and take the requested count
            return jobListings
                .GroupBy(j => j.Id)
                .Select(g => g.First())
                .OrderByDescending(j => j.PostedDate)
                .Take(count);
        }
    }
}
