using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalJobAgent.Core.Models;

namespace PersonalJobAgent.Data.Repositories
{
    /// <summary>
    /// Implementation of the UserProfile repository interface
    /// </summary>
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        private readonly PersonalJobAgentDbContext _dbContext;

        public UserProfileRepository(PersonalJobAgentDbContext dbContext) 
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<UserProfile> GetByEmailAsync(string email)
        {
            return await _dbContext.UserProfiles
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <inheritdoc/>
        public async Task<UserProfile> GetWithRelatedDataAsync(int id)
        {
            return await _dbContext.UserProfiles
                .Include(u => u.Skills)
                .Include(u => u.WorkExperiences)
                .Include(u => u.Education)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <inheritdoc/>
        public async Task UpdateSkillsAsync(int userId, IEnumerable<Skill> skills)
        {
            var user = await GetByIdAsync(userId);
            if (user == null)
                throw new ArgumentException($"User with ID {userId} not found");

            // Get existing skills for this user
            var existingSkills = await _dbContext.Skills
                .Where(s => s.UserProfileId == userId)
                .ToListAsync();

            // Remove skills that are no longer in the list
            foreach (var existingSkill in existingSkills)
            {
                if (!skills.Any(s => s.Id == existingSkill.Id))
                {
                    _dbContext.Skills.Remove(existingSkill);
                }
            }

            // Add or update skills
            foreach (var skill in skills)
            {
                var existingSkill = existingSkills.FirstOrDefault(s => s.Id == skill.Id);
                if (existingSkill == null)
                {
                    // New skill
                    skill.UserProfileId = userId;
                    await _dbContext.Skills.AddAsync(skill);
                }
                else
                {
                    // Update existing skill
                    existingSkill.Name = skill.Name;
                    existingSkill.Level = skill.Level;
                    existingSkill.Category = skill.Category;
                    _dbContext.Skills.Update(existingSkill);
                }
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
