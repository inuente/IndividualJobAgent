using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalJobAgent.Core.Models;

namespace PersonalJobAgent.Data.Repositories
{
    /// <summary>
    /// Interface for UserProfile repository operations
    /// </summary>
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        /// <summary>
        /// Gets user profile by email address
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns>User profile or null if not found</returns>
        Task<UserProfile> GetByEmailAsync(string email);
        
        /// <summary>
        /// Gets user profile with related skills, education, and work experience
        /// </summary>
        /// <param name="id">User profile identifier</param>
        /// <returns>User profile with related data or null if not found</returns>
        Task<UserProfile> GetWithRelatedDataAsync(int id);
        
        /// <summary>
        /// Updates user profile skills
        /// </summary>
        /// <param name="userId">User profile identifier</param>
        /// <param name="skills">Collection of skills</param>
        Task UpdateSkillsAsync(int userId, IEnumerable<Skill> skills);
    }
}
