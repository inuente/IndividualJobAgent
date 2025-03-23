using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PersonalJobAgent.Core.Interfaces;
using PersonalJobAgent.Core.Models.PersonalJobAgent.Core.Models;
using PersonalJobAgent.Core.Services;
using PersonalJobAgent.Data.Repositories;
using PersonalJobAgent.UI.ViewModels;

namespace PersonalJobAgent.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Configuration
            string pythonScriptsPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AI", "Scripts");
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PersonalJobAgentDB;Integrated Security=True";

            // Repositories
            services.AddSingleton<IUserProfileRepository>(provider => new UserProfileRepository(connectionString));
            services.AddSingleton<IJobRepository>(provider => new JobRepository(connectionString));
            services.AddSingleton<IApplicationRepository>(provider => new ApplicationRepository(connectionString));

            // Services
            services.AddSingleton<IProfileService, ProfileService>();
            services.AddSingleton<IJobDiscoveryService, JobDiscoveryService>();
            services.AddSingleton<IApplicationService, ApplicationService>();
            services.AddSingleton<IAIService>(provider => new AIService(pythonScriptsPath));

            //
            // Add logging services
            services.AddSingleton<IAIService>(provider => new AIService(provider.GetRequiredService<ILogger<AIService>>()));
            // Register AIService with its dependencies
            services.AddScoped<IAIService, AIService>();

            // ViewModels
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<ProfileViewModel>();
            services.AddTransient<JobSearchViewModel>();
            services.AddTransient<ApplicationTrackingViewModel>();

            // Main Window
            services.AddTransient<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

    // Mock repository interfaces and implementations for template purposes
    public interface IUserProfileRepository { }
    public interface IJobRepository { }
    public interface IApplicationRepository { }

    public class UserProfileRepository : IUserProfileRepository
    {
        public UserProfileRepository(string connectionString) { }
    }

    public class JobRepository : IJobRepository
    {
        public JobRepository(string connectionString) { }
    }

    public class ApplicationRepository : IApplicationRepository
    {
        public ApplicationRepository(string connectionString) { }
    }

    public class ProfileService : IProfileService
    {
        public ProfileService() { }

        public Task<Core.Models.Education> AddEducationAsync(int userProfileId, Core.Models.Education education)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Models.Skill> AddSkillAsync(int userProfileId, Core.Models.Skill skill)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Models.WorkExperience> AddWorkExperienceAsync(int userProfileId, Core.Models.WorkExperience workExperience)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Models.UserProfile> GetCurrentUserProfileAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Core.Models.UserProfile> GetUserProfileAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Models.UserProfile> ImportProfileFromResumeAsync(string resumeText)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveEducationAsync(int userProfileId, int educationId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveSkillAsync(int userProfileId, int skillId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveWorkExperienceAsync(int userProfileId, int workExperienceId)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Models.Education> UpdateEducationAsync(Core.Models.Education education)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Models.Skill> UpdateSkillAsync(Core.Models.Skill skill)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Models.UserProfile> UpdateUserProfileAsync(Core.Models.UserProfile profile)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Models.WorkExperience> UpdateWorkExperienceAsync(Core.Models.WorkExperience workExperience)
        {
            throw new NotImplementedException();
        }
    }

    public class JobDiscoveryService : IJobDiscoveryService
    {
        public JobDiscoveryService() { }

        public Task<SavedSearch> CreateSavedSearchAsync(int userProfileId, string[] keywords, string location = null, string name = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Core.Models.JobListing>> GetExternalJobsAsync(string[] platforms, string[] keywords, string location = null, int count = 10)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Models.JobListing> GetJobListingAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Core.Models.JobListing>> GetJobsByLocationAsync(string location, int radiusInMiles = 25, int pageNumber = 1, int pageSize = 20)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Core.Models.JobListing>> GetRecommendedJobsAsync(int userProfileId, int count = 10)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Models.JobListing> SaveJobListingAsync(Core.Models.JobListing jobListing)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Core.Models.JobListing>> SearchJobsAsync(string[] keywords, string location = null, int pageNumber = 1, int pageSize = 20)
        {
            throw new NotImplementedException();
        }
    }

    public class ApplicationService : IApplicationService
    {
        public ApplicationService() { }

        public Task<Core.Models.Application> CreateApplicationAsync(int userProfileId, int jobListingId, string coverLetter = null, string notes = null)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateCoverLetterAsync(int userProfileId, int jobListingId)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Models.Application> GetApplicationAsync(int applicationId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Core.Models.Application>> GetApplicationsAsync(int userProfileId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Core.Models.Application>> GetApplicationsByStatusAsync(int userProfileId, string status)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Core.Models.Application>> GetApplicationsForProfileAsync(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Models.ApplicationStatistics> GetApplicationStatisticsAsync(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetInterviewPreparationAsync(int applicationId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Core.Models.Application>> GetRecentApplicationsAsync(int userProfileId, int count = 5)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Models.Application> UpdateApplicationStatusAsync(int applicationId, string status, string notes = null)
        {
            throw new NotImplementedException();
        }
    }
}
