using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalJobAgent.Core.Models;

namespace PersonalJobAgent.Core.Interfaces
{
    /// <summary>
    /// Interface for profile service operations
    /// </summary>
    public interface IProfileService
    {
        /// <summary>
        /// Imports a user profile from resume text
        /// </summary>
        /// <param name="resumeText">The resume text to parse</param>
        /// <returns>A new or updated user profile based on the resume</returns>
        Task<UserProfile> ImportProfileFromResumeAsync(string resumeText);

        /// <summary>
        /// Gets the current user profile
        /// </summary>
        /// <returns>Current user profile</returns>
        Task<UserProfile> GetCurrentUserProfileAsync();
        
        /// <summary>
        /// Gets a user profile by ID
        /// </summary>
        /// <param name="id">User profile ID</param>
        /// <returns>User profile</returns>
        Task<UserProfile> GetUserProfileAsync(int id);
        
        /// <summary>
        /// Updates a user profile
        /// </summary>
        /// <param name="profile">User profile to update</param>
        /// <returns>Updated user profile</returns>
        Task<UserProfile> UpdateUserProfileAsync(UserProfile profile);
        
        /// <summary>
        /// Adds a skill to a user profile
        /// </summary>
        /// <param name="userProfileId">User profile ID</param>
        /// <param name="skill">Skill to add</param>
        /// <returns>Added skill</returns>
        Task<Skill> AddSkillAsync(int userProfileId, Skill skill);
        
        /// <summary>
        /// Updates a skill
        /// </summary>
        /// <param name="skill">Skill to update</param>
        /// <returns>Updated skill</returns>
        Task<Skill> UpdateSkillAsync(Skill skill);
        
        /// <summary>
        /// Removes a skill from a user profile
        /// </summary>
        /// <param name="userProfileId">User profile ID</param>
        /// <param name="skillId">Skill ID</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> RemoveSkillAsync(int userProfileId, int skillId);
        
        /// <summary>
        /// Adds work experience to a user profile
        /// </summary>
        /// <param name="userProfileId">User profile ID</param>
        /// <param name="workExperience">Work experience to add</param>
        /// <returns>Added work experience</returns>
        Task<WorkExperience> AddWorkExperienceAsync(int userProfileId, WorkExperience workExperience);
        
        /// <summary>
        /// Updates work experience
        /// </summary>
        /// <param name="workExperience">Work experience to update</param>
        /// <returns>Updated work experience</returns>
        Task<WorkExperience> UpdateWorkExperienceAsync(WorkExperience workExperience);
        
        /// <summary>
        /// Removes work experience from a user profile
        /// </summary>
        /// <param name="userProfileId">User profile ID</param>
        /// <param name="workExperienceId">Work experience ID</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> RemoveWorkExperienceAsync(int userProfileId, int workExperienceId);
        
        /// <summary>
        /// Adds education to a user profile
        /// </summary>
        /// <param name="userProfileId">User profile ID</param>
        /// <param name="education">Education to add</param>
        /// <returns>Added education</returns>
        Task<Education> AddEducationAsync(int userProfileId, Education education);
        
        /// <summary>
        /// Updates education
        /// </summary>
        /// <param name="education">Education to update</param>
        /// <returns>Updated education</returns>
        Task<Education> UpdateEducationAsync(Education education);
        
        /// <summary>
        /// Removes education from a user profile
        /// </summary>
        /// <param name="userProfileId">User profile ID</param>
        /// <param name="educationId">Education ID</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> RemoveEducationAsync(int userProfileId, int educationId);
    }
}
