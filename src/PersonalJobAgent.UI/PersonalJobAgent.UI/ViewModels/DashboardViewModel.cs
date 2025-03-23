using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalJobAgent.Core.Models;
using PersonalJobAgent.Core.Interfaces;

namespace PersonalJobAgent.UI.ViewModels
{
    /// <summary>
    /// ViewModel for the main dashboard view.
    /// </summary>
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IProfileService _profileService;
        private readonly IJobDiscoveryService _jobDiscoveryService;
        private readonly IApplicationService _applicationService;
        
        private UserProfile _userProfile;
        private IEnumerable<JobListing> _recommendedJobs;
        private IEnumerable<Application> _recentApplications;
        private IEnumerable<object> _upcomingInterviews;
        private object _applicationStats;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardViewModel"/> class.
        /// </summary>
        /// <param name="profileService">The profile service.</param>
        /// <param name="jobDiscoveryService">The job discovery service.</param>
        /// <param name="applicationService">The application service.</param>
        public DashboardViewModel(
            IProfileService profileService,
            IJobDiscoveryService jobDiscoveryService,
            IApplicationService applicationService)
        {
            _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
            _jobDiscoveryService = jobDiscoveryService ?? throw new ArgumentNullException(nameof(jobDiscoveryService));
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
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
        /// Gets or sets the recommended jobs.
        /// </summary>
        public IEnumerable<JobListing> RecommendedJobs
        {
            get => _recommendedJobs;
            set
            {
                _recommendedJobs = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Gets or sets the recent applications.
        /// </summary>
        public IEnumerable<Application> RecentApplications
        {
            get => _recentApplications;
            set
            {
                _recentApplications = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Gets or sets the upcoming interviews.
        /// </summary>
        public IEnumerable<object> UpcomingInterviews
        {
            get => _upcomingInterviews;
            set
            {
                _upcomingInterviews = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Gets or sets the application statistics.
        /// </summary>
        public object ApplicationStats
        {
            get => _applicationStats;
            set
            {
                _applicationStats = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Loads dashboard data for the specified profile.
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task LoadDashboardDataAsync(int profileId)
        {
            try
            {
                // Load user profile
                UserProfile = await _profileService.GetProfileAsync(profileId);
                
                // Load recommended jobs
                RecommendedJobs = await _jobDiscoveryService.GetRecommendationsAsync(profileId, 5);
                
                // Load recent applications
                RecentApplications = await _applicationService.GetApplicationsForProfileAsync(profileId);
                
                // Load application statistics
                ApplicationStats = await _applicationService.GetApplicationStatisticsAsync(profileId);
                
                // Load upcoming follow-ups
                UpcomingInterviews = await _applicationService.GetUpcomingFollowUpsAsync(profileId);
            }
            catch (Exception ex)
            {
                // In a real implementation, would use a logging service
                Console.WriteLine($"Error loading dashboard data: {ex.Message}");
                throw;
            }
        }
    }
}
