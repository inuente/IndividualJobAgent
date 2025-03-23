using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using PersonalJobAgent.Core.Models;
using PersonalJobAgent.Core.Interfaces;

namespace PersonalJobAgent.UI.ViewModels
{
    /// <summary>
    /// ViewModel for the profile management view.
    /// </summary>
    public class ProfileViewModel : ViewModelBase
    {
        private readonly IProfileService _profileService;
        private readonly IAIService _aiService;
        
        private UserProfile _userProfile;
        private string _resumeText;
        private bool _isEditing;
        private bool _isBusy;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileViewModel"/> class.
        /// </summary>
        /// <param name="profileService">The profile service.</param>
        /// <param name="aiService">The AI service.</param>
        public ProfileViewModel(
            IProfileService profileService,
            IAIService aiService)
        {
            _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
            _aiService = aiService ?? throw new ArgumentNullException(nameof(aiService));
            
            SaveCommand = new RelayCommand(async () => await SaveProfileAsync(), () => IsEditing && !IsBusy);
            EditCommand = new RelayCommand(() => IsEditing = true, () => !IsEditing && !IsBusy);
            CancelCommand = new RelayCommand(() => IsEditing = false, () => IsEditing && !IsBusy);
            ImportResumeCommand = new RelayCommand(async () => await ImportResumeAsync(), () => !IsBusy);
            AddSkillCommand = new RelayCommand<Skill>(async (skill) => await AddSkillAsync(skill), (_) => IsEditing && !IsBusy);
            RemoveSkillCommand = new RelayCommand<int>(async (skillId) => await RemoveSkillAsync(skillId), (_) => IsEditing && !IsBusy);
            AddExperienceCommand = new RelayCommand<WorkExperience>(async (exp) => await AddExperienceAsync(exp), (_) => IsEditing && !IsBusy);
            RemoveExperienceCommand = new RelayCommand<int>(async (expId) => await RemoveExperienceAsync(expId), (_) => IsEditing && !IsBusy);
            AddEducationCommand = new RelayCommand<Education>(async (edu) => await AddEducationAsync(edu), (_) => IsEditing && !IsBusy);
            RemoveEducationCommand = new RelayCommand<int>(async (eduId) => await RemoveEducationAsync(eduId), (_) => IsEditing && !IsBusy);
        }
        
        /// <summary>
        /// Gets or sets the user profile.
        /// </summary>
        public UserProfile UserProfile
        {
            get => _userProfile;
            set
            {
                _userProfile = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Gets or sets the resume text.
        /// </summary>
        public string ResumeText
        {
            get => _resumeText;
            set
            {
                _resumeText = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the profile is being edited.
        /// </summary>
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                OnPropertyChanged();
                (SaveCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (CancelCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (AddSkillCommand as RelayCommand<Skill>)?.RaiseCanExecuteChanged();
                (RemoveSkillCommand as RelayCommand<int>)?.RaiseCanExecuteChanged();
                (AddExperienceCommand as RelayCommand<WorkExperience>)?.RaiseCanExecuteChanged();
                (RemoveExperienceCommand as RelayCommand<int>)?.RaiseCanExecuteChanged();
                (AddEducationCommand as RelayCommand<Education>)?.RaiseCanExecuteChanged();
                (RemoveEducationCommand as RelayCommand<int>)?.RaiseCanExecuteChanged();
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the ViewModel is busy.
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                (SaveCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (CancelCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (ImportResumeCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (AddSkillCommand as RelayCommand<Skill>)?.RaiseCanExecuteChanged();
                (RemoveSkillCommand as RelayCommand<int>)?.RaiseCanExecuteChanged();
                (AddExperienceCommand as RelayCommand<WorkExperience>)?.RaiseCanExecuteChanged();
                (RemoveExperienceCommand as RelayCommand<int>)?.RaiseCanExecuteChanged();
                (AddEducationCommand as RelayCommand<Education>)?.RaiseCanExecuteChanged();
                (RemoveEducationCommand as RelayCommand<int>)?.RaiseCanExecuteChanged();
            }
        }
        
        /// <summary>
        /// Gets the command to save the profile.
        /// </summary>
        public ICommand SaveCommand { get; }
        
        /// <summary>
        /// Gets the command to edit the profile.
        /// </summary>
        public ICommand EditCommand { get; }
        
        /// <summary>
        /// Gets the command to cancel editing.
        /// </summary>
        public ICommand CancelCommand { get; }
        
        /// <summary>
        /// Gets the command to import a resume.
        /// </summary>
        public ICommand ImportResumeCommand { get; }
        
        /// <summary>
        /// Gets the command to add a skill.
        /// </summary>
        public ICommand AddSkillCommand { get; }
        
        /// <summary>
        /// Gets the command to remove a skill.
        /// </summary>
        public ICommand RemoveSkillCommand { get; }
        
        /// <summary>
        /// Gets the command to add a work experience.
        /// </summary>
        public ICommand AddExperienceCommand { get; }
        
        /// <summary>
        /// Gets the command to remove a work experience.
        /// </summary>
        public ICommand RemoveExperienceCommand { get; }
        
        /// <summary>
        /// Gets the command to add an education entry.
        /// </summary>
        public ICommand AddEducationCommand { get; }
        
        /// <summary>
        /// Gets the command to remove an education entry.
        /// </summary>
        public ICommand RemoveEducationCommand { get; }
        
        /// <summary>
        /// Loads the profile with the specified ID.
        /// </summary>
        /// <param name="profileId">The profile ID to load.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task LoadProfileAsync(int profileId)
        {
            try
            {
                IsBusy = true;
                UserProfile = await _profileService.GetProfileAsync(profileId);
            }
            catch (Exception ex)
            {
                // In a real implementation, would use a logging service
                Console.WriteLine($"Error loading profile: {ex.Message}");
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        /// <summary>
        /// Saves the current profile.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task SaveProfileAsync()
        {
            try
            {
                IsBusy = true;
                UserProfile = await _profileService.UpdateProfileAsync(UserProfile);
                IsEditing = false;
            }
            catch (Exception ex)
            {
                // In a real implementation, would use a logging service
                Console.WriteLine($"Error saving profile: {ex.Message}");
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        /// <summary>
        /// Imports a profile from a resume.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task ImportResumeAsync()
        {
            if (string.IsNullOrWhiteSpace(ResumeText))
            {
                return;
            }
            
            try
            {
                IsBusy = true;
                UserProfile = await _profileService.ImportProfileFromResumeAsync(ResumeText);
            }
            catch (Exception ex)
            {
                // In a real implementation, would use a logging service
                Console.WriteLine($"Error importing resume: {ex.Message}");
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        /// <summary>
        /// Adds a skill to the profile.
        /// </summary>
        /// <param name="skill">The skill to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task AddSkillAsync(Skill skill)
        {
            try
            {
                IsBusy = true;
                var addedSkill = await _profileService.AddSkillAsync(UserProfile.ProfileId, skill);
                UserProfile.Skills.Add(addedSkill);
            }
            catch (Exception ex)
            {
                // In a real implementation, would use a logging service
                Console.WriteLine($"Error adding skill: {ex.Message}");
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        /// <summary>
        /// Removes a skill from the profile.
        /// </summary>
        /// <param name="skillId">The ID of the skill to remove.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task RemoveSkillAsync(int skillId)
        {
            try
            {
                IsBusy = true;
                await _profileService.RemoveSkillAsync(skillId);
                
                // Remove from local collection
                var skillToRemove = null as Skill;
                foreach (var skill in UserProfile.Skills)
                {
                    if (skill.SkillId == skillId)
                    {
                        skillToRemove = skill;
                        break;
                    }
                }
                
                if (skillToRemove != null)
                {
                    UserProfile.Skills.Remove(skillToRemove);
                }
            }
            catch (Exception ex)
            {
                // In a real implementation, would use a logging service
                Console.WriteLine($"Error removing skill: {ex.Message}");
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        /// <summary>
        /// Adds a work experience to the profile.
        /// </summary>
        /// <param name="experience">The work experience to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task AddExperienceAsync(WorkExperience experience)
        {
            try
            {
                IsBusy = true;
                var addedExperience = await _profileService.AddWorkExperienceAsync(UserProfile.ProfileId, experience);
                UserProfile.WorkExperiences.Add(addedExperience);
            }
            catch (Exception ex)
            {
                // In a real implementation, would use a logging service
                Console.WriteLine($"Error adding work experience: {ex.Message}");
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        /// <summary>
        /// Removes a work experience from the profile.
        /// </summary>
        /// <param name="experienceId">The ID of the work experience to remove.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task RemoveExperienceAsync(int experienceId)
        {
            try
            {
                IsBusy = true;
                await _profileService.RemoveWorkExperienceAsync(experienceId);
                
                // Remove from local collection
                var experienceToRemove = null as WorkExperience;
                foreach (var experience in UserProfile.WorkExperiences)
                {
                    if (experience.ExperienceId == experienceId)
                    {
                        experienceToRemove = experience;
                        break;
                    }
                }
                
                if (experienceToRemove != null)
                {
                    UserProfile.WorkExperiences.Remove(experienceToRemove);
                }
            }
            catch (Exception ex)
            {
                // In a real implementation, would use a logging service
                Console.WriteLine($"Error removing work experience: {ex.Message}");
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        /// <summary>
        /// Adds an education entry to the profile.
        /// </summary>
        /// <param name="education">The education entry to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task AddEducationAsync(Education education)
        {
            try
            {
                IsBusy = true;
                var addedEducation = await _profileService.AddEducationAsync(UserProfile.ProfileId, education);
                UserProfile.Educations.Add(addedEducation);
            }
            catch (Exception ex)
            {
                // In a real implementation, would use a logging service
                Console.WriteLine($"Error adding education: {ex.Message}");
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        /// <summary>
        /// Removes an education entry from the profile.
        /// </summary>
        /// <param name="educationId">The ID of the education entry to remove.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task RemoveEducationAsync(int educationId)
        {
            try
            {
                IsBusy = true;
                await _profileService.RemoveEducationAsync(educationId);
                
                // Remove from local collection
                var educationToRemove = null as Education;
                foreach (var education in UserProfile.Educations)
                {
                    if (education.EducationId == educationId)
                    {
                        educationToRemove = education;
                        break;
                    }
                }
                
                if (educationToRemove != null)
                {
                    UserProfile.Educations.Remove(educationToRemove);
                }
            }
            catch (Exception ex)
            {
                // In a real implementation, would use a logging service
                Console.WriteLine($"Error removing education: {ex.Message}");
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
