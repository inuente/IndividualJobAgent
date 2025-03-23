using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalJobAgent.Core.Models;

namespace PersonalJobAgent.Core.Interfaces
{
    /// <summary>
    /// Interface for profile management service.
    /// </summary>
    public interface IProfileService
    {
        /// <summary>
        /// Gets a user profile by ID.
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <returns>The user profile.</returns>
        Task<UserProfile> GetProfileAsync(int profileId);

        /// <summary>
        /// Creates a new user profile.
        /// </summary>
        /// <param name="profile">The profile to create.</param>
        /// <returns>The created profile with ID assigned.</returns>
        Task<UserProfile> CreateProfileAsync(UserProfile profile);

        /// <summary>
        /// Updates an existing user profile.
        /// </summary>
        /// <param name="profile">The profile to update.</param>
        /// <returns>The updated profile.</returns>
        Task<UserProfile> UpdateProfileAsync(UserProfile profile);

        /// <summary>
        /// Adds a skill to a user profile.
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <param name="skill">The skill to add.</param>
        /// <returns>The added skill with ID assigned.</returns>
        Task<Skill> AddSkillAsync(int profileId, Skill skill);

        /// <summary>
        /// Updates a skill.
        /// </summary>
        /// <param name="skill">The skill to update.</param>
        /// <returns>The updated skill.</returns>
        Task<Skill> UpdateSkillAsync(Skill skill);

        /// <summary>
        /// Removes a skill from a user profile.
        /// </summary>
        /// <param name="skillId">The skill ID to remove.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task RemoveSkillAsync(int skillId);

        /// <summary>
        /// Adds a work experience to a user profile.
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <param name="experience">The work experience to add.</param>
        /// <returns>The added work experience with ID assigned.</returns>
        Task<WorkExperience> AddWorkExperienceAsync(int profileId, WorkExperience experience);

        /// <summary>
        /// Updates a work experience.
        /// </summary>
        /// <param name="experience">The work experience to update.</param>
        /// <returns>The updated work experience.</returns>
        Task<WorkExperience> UpdateWorkExperienceAsync(WorkExperience experience);

        /// <summary>
        /// Removes a work experience from a user profile.
        /// </summary>
        /// <param name="experienceId">The experience ID to remove.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task RemoveWorkExperienceAsync(int experienceId);

        /// <summary>
        /// Adds an education entry to a user profile.
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <param name="education">The education entry to add.</param>
        /// <returns>The added education entry with ID assigned.</returns>
        Task<Education> AddEducationAsync(int profileId, Education education);

        /// <summary>
        /// Updates an education entry.
        /// </summary>
        /// <param name="education">The education entry to update.</param>
        /// <returns>The updated education entry.</returns>
        Task<Education> UpdateEducationAsync(Education education);

        /// <summary>
        /// Removes an education entry from a user profile.
        /// </summary>
        /// <param name="educationId">The education ID to remove.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task RemoveEducationAsync(int educationId);

        /// <summary>
        /// Adds a platform credential to a user profile.
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <param name="credential">The credential to add.</param>
        /// <returns>The added credential with ID assigned.</returns>
        Task<PlatformCredential> AddPlatformCredentialAsync(int profileId, PlatformCredential credential);

        /// <summary>
        /// Updates a platform credential.
        /// </summary>
        /// <param name="credential">The credential to update.</param>
        /// <returns>The updated credential.</returns>
        Task<PlatformCredential> UpdatePlatformCredentialAsync(PlatformCredential credential);

        /// <summary>
        /// Removes a platform credential from a user profile.
        /// </summary>
        /// <param name="credentialId">The credential ID to remove.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task RemovePlatformCredentialAsync(int credentialId);

        /// <summary>
        /// Imports a profile from a resume document.
        /// </summary>
        /// <param name="resumeText">The resume text content.</param>
        /// <returns>The imported profile.</returns>
        Task<UserProfile> ImportProfileFromResumeAsync(string resumeText);
    }
}
