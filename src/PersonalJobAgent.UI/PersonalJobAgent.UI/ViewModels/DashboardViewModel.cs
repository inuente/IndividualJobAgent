using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PersonalJobAgent.Core.Interfaces;
using PersonalJobAgent.Core.Models;

namespace PersonalJobAgent.UI.ViewModels
{
    /// <summary>
    /// ViewModel for the Dashboard view
    /// </summary>
    public class DashboardViewModel : ViewModelBase
    {
        private readonly IProfileService _profileService;
        private readonly IJobDiscoveryService _jobDiscoveryService;
        private readonly IApplicationService _applicationService;
        
        private UserProfile _userProfile;
        private ObservableCollection<JobListing> _recommendedJobs;
        private ObservableCollection<Application> _recentApplications;
        private bool _isLoading;
        private string _statusMessage;

        public DashboardViewModel(
            IProfileService profileService,
            IJobDiscoveryService jobDiscoveryService,
            IApplicationService applicationService)
        {
            _profileService = profileService;
            _jobDiscoveryService = jobDiscoveryService;
            _applicationService = applicationService;
            
            RecommendedJobs = new ObservableCollection<JobListing>();
            RecentApplications = new ObservableCollection<Application>();
            
            RefreshCommand = CreateCommand(async () => await LoadDataAsync());
            ViewProfileCommand = CreateCommand(ViewProfile);
            SearchJobsCommand = CreateCommand(SearchJobs);
            ViewApplicationsCommand = CreateCommand(ViewApplications);
        }

        /// <summary>
        /// Gets or sets the user profile
        /// </summary>
        public UserProfile UserProfile
        {
            get => _userProfile;
            set => SetProperty(ref _userProfile, value);
        }

        /// <summary>
        /// Gets or sets the collection of recommended jobs
        /// </summary>
        public ObservableCollection<JobListing> RecommendedJobs
        {
            get => _recommendedJobs;
            set => SetProperty(ref _recommendedJobs, value);
        }

        /// <summary>
        /// Gets or sets the collection of recent applications
        /// </summary>
        public ObservableCollection<Application> RecentApplications
        {
            get => _recentApplications;
            set => SetProperty(ref _recentApplications, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether data is being loaded
        /// </summary>
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        /// <summary>
        /// Gets or sets the status message
        /// </summary>
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        /// <summary>
        /// Gets the command to refresh the dashboard data
        /// </summary>
        public ICommand RefreshCommand { get; }

        /// <summary>
        /// Gets the command to view the user profile
        /// </summary>
        public ICommand ViewProfileCommand { get; }

        /// <summary>
        /// Gets the command to search for jobs
        /// </summary>
        public ICommand SearchJobsCommand { get; }

        /// <summary>
        /// Gets the command to view applications
        /// </summary>
        public ICommand ViewApplicationsCommand { get; }

        /// <summary>
        /// Initializes the view model
        /// </summary>
        public async Task InitializeAsync()
        {
            await LoadDataAsync();
        }

        /// <summary>
        /// Loads the dashboard data
        /// </summary>
        private async Task LoadDataAsync()
        {
            try
            {
                IsLoading = true;
                StatusMessage = "Loading dashboard data...";

                // Load user profile
                UserProfile = await _profileService.GetCurrentUserProfileAsync();

                // Load recommended jobs
                var recommendedJobs = await _jobDiscoveryService.GetRecommendedJobsAsync(UserProfile.Id, 5);
                RecommendedJobs.Clear();
                foreach (var job in recommendedJobs)
                {
                    RecommendedJobs.Add(job);
                }

                // Load recent applications
                var recentApplications = await _applicationService.GetRecentApplicationsAsync(UserProfile.Id, 5);
                RecentApplications.Clear();
                foreach (var application in recentApplications)
                {
                    RecentApplications.Add(application);
                }

                StatusMessage = "Dashboard data loaded successfully";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error loading dashboard data: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// Navigates to the profile view
        /// </summary>
        private void ViewProfile()
        {
            // Navigation logic would be implemented here
            // This would typically use a navigation service to navigate to the profile view
        }

        /// <summary>
        /// Navigates to the job search view
        /// </summary>
        private void SearchJobs()
        {
            // Navigation logic would be implemented here
            // This would typically use a navigation service to navigate to the job search view
        }

        /// <summary>
        /// Navigates to the applications view
        /// </summary>
        private void ViewApplications()
        {
            // Navigation logic would be implemented here
            // This would typically use a navigation service to navigate to the applications view
        }
    }
}
