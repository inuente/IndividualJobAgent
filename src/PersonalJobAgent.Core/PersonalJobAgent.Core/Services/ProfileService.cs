    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using PersonalJobAgent.Core.Interfaces;
    using PersonalJobAgent.Core.Models;
    using PersonalJobAgent.Data.Repositories; 

    namespace PersonalJobAgent.Core.Services
    {
        public class ProfileService : IProfileService
        {
            private readonly IUserProfileRepository _userProfileRepository;

            public ProfileService(IUserProfileRepository userProfileRepository)
            {
                _userProfileRepository = userProfileRepository;
            }

            public async Task<UserProfile> GetCurrentUserProfileAsync()
            {
                // For now, return the first user profile or create a new one if none exists
                var profiles = await _userProfileRepository.GetAllAsync();
                return profiles.FirstOrDefault() ?? new UserProfile
                {
                    FirstName = "Default",
                    LastName = "User",
                    Email = "default@example.com"
                };
            }

            public async Task<UserProfile> GetUserProfileAsync(int id)
            {
                return await _userProfileRepository.GetByIdAsync(id);
            }

            public async Task<UserProfile> UpdateUserProfileAsync(UserProfile profile)
            {
                await _userProfileRepository.UpdateAsync(profile);
                return profile;
            }

            public async Task<Skill> AddSkillAsync(int userProfileId, Skill skill)
            {
                skill.UserProfileId = userProfileId;
                // Implementation depends on your repository pattern
                // This is a simplified example
                var profile = await _userProfileRepository.GetByIdAsync(userProfileId);
                profile.Skills.Add(skill);
                await _userProfileRepository.UpdateAsync(profile);
                return skill;
            }

            public async Task<Skill> UpdateSkillAsync(Skill skill)
            {
                // Implementation
                var profile = await _userProfileRepository.GetByIdAsync(skill.UserProfileId);
                var existingSkill = profile.Skills.FirstOrDefault(s => s.Id == skill.Id);
                if (existingSkill != null)
                {
                    // Update properties
                    existingSkill.Name = skill.Name;
                    existingSkill.Level = skill.Level;
                    existingSkill.Category = skill.Category;
                    await _userProfileRepository.UpdateAsync(profile);
                }
                return skill;
            }

            public async Task<bool> RemoveSkillAsync(int userProfileId, int skillId)
            {
                var profile = await _userProfileRepository.GetByIdAsync(userProfileId);
                var skill = profile.Skills.FirstOrDefault(s => s.Id == skillId);
                if (skill != null)
                {
                    profile.Skills.Remove(skill);
                    await _userProfileRepository.UpdateAsync(profile);
                    return true;
                }
                return false;
            }

            public async Task<WorkExperience> AddWorkExperienceAsync(int userProfileId, WorkExperience workExperience)
            {
                workExperience.UserProfileId = userProfileId;
                var profile = await _userProfileRepository.GetByIdAsync(userProfileId);
                profile.WorkExperiences.Add(workExperience);
                await _userProfileRepository.UpdateAsync(profile);
                return workExperience;
            }

            public async Task<WorkExperience> UpdateWorkExperienceAsync(WorkExperience workExperience)
            {
                var profile = await _userProfileRepository.GetByIdAsync(workExperience.UserProfileId);
                var existingExperience = profile.WorkExperiences.FirstOrDefault(w => w.Id == workExperience.Id);
                if (existingExperience != null)
                {
                    // Update properties
                    existingExperience.Company = workExperience.Company;
                    existingExperience.Title = workExperience.Title;
                    existingExperience.StartDate = workExperience.StartDate;
                    existingExperience.EndDate = workExperience.EndDate;
                    existingExperience.Description = workExperience.Description;
                    existingExperience.Location = workExperience.Location;
                    await _userProfileRepository.UpdateAsync(profile);
                }
                return workExperience;
            }

            public async Task<bool> RemoveWorkExperienceAsync(int userProfileId, int workExperienceId)
            {
                var profile = await _userProfileRepository.GetByIdAsync(userProfileId);
                var workExperience = profile.WorkExperiences.FirstOrDefault(w => w.Id == workExperienceId);
                if (workExperience != null)
                {
                    profile.WorkExperiences.Remove(workExperience);
                    await _userProfileRepository.UpdateAsync(profile);
                    return true;
                }
                return false;
            }

            public async Task<Education> AddEducationAsync(int userProfileId, Education education)
            {
                education.UserProfileId = userProfileId;
                var profile = await _userProfileRepository.GetByIdAsync(userProfileId);
                profile.Education.Add(education);
                await _userProfileRepository.UpdateAsync(profile);
                return education;
            }

            public async Task<Education> UpdateEducationAsync(Education education)
            {
                var profile = await _userProfileRepository.GetByIdAsync(education.UserProfileId);
                var existingEducation = profile.Education.FirstOrDefault(e => e.Id == education.Id);
                if (existingEducation != null)
                {
                    // Update properties
                    existingEducation.Institution = education.Institution;
                    existingEducation.Degree = education.Degree;
                    existingEducation.FieldOfStudy = education.FieldOfStudy;
                    existingEducation.StartDate = education.StartDate;
                    existingEducation.EndDate = education.EndDate;
                    existingEducation.Description = education.Description;
                    await _userProfileRepository.UpdateAsync(profile);
                }
                return education;
            }

            public async Task<bool> RemoveEducationAsync(int userProfileId, int educationId)
            {
                var profile = await _userProfileRepository.GetByIdAsync(userProfileId);
                var education = profile.Education.FirstOrDefault(e => e.Id == educationId);
                if (education != null)
                {
                    profile.Education.Remove(education);
                    await _userProfileRepository.UpdateAsync(profile);
                    return true;
                }
                return false;
            }
        }
    }



