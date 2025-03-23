using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalJobAgent.Core.Models;

namespace PersonalJobAgent.Data.Repositories
{
    /// <summary>
    /// Implementation of the Application repository interface
    /// </summary>
    public class ApplicationRepository : Repository<Application>, IApplicationRepository
    {
        private readonly PersonalJobAgentDbContext _dbContext;

        public ApplicationRepository(PersonalJobAgentDbContext dbContext) 
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Application>> GetByUserProfileIdAsync(int userProfileId)
        {
            return await _dbContext.Applications
                .Where(a => a.UserProfileId == userProfileId)
                .OrderByDescending(a => a.ApplicationDate)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Application>> GetByStatusAsync(int userProfileId, string status)
        {
            return await _dbContext.Applications
                .Where(a => a.UserProfileId == userProfileId && a.Status == status)
                .OrderByDescending(a => a.ApplicationDate)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Application>> GetWithJobListingsAsync(int userProfileId)
        {
            return await _dbContext.Applications
                .Where(a => a.UserProfileId == userProfileId)
                .Include(a => a.JobListing)
                .OrderByDescending(a => a.ApplicationDate)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateStatusAsync(int applicationId, string status, string notes = null)
        {
            var application = await GetByIdAsync(applicationId);
            if (application == null)
                throw new ArgumentException($"Application with ID {applicationId} not found");

            application.Status = status;
            application.LastUpdated = DateTime.UtcNow;
            
            if (!string.IsNullOrEmpty(notes))
            {
                application.Notes = string.IsNullOrEmpty(application.Notes)
                    ? notes
                    : $"{application.Notes}\n\n{DateTime.UtcNow:g}: {notes}";
            }

            await UpdateAsync(application);
        }
    }
}
