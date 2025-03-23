using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using PersonalJobAgent.Core.Models;
using PersonalJobAgent.Core.Interfaces;

namespace PersonalJobAgent.UI.ViewModels
{
    /// <summary>
    /// ViewModel for the job search view.
    /// </summary>
    public class JobSearchViewModel : ViewModelBase
    {
        private readonly IJobDiscoveryService _jobDiscoveryService;
        private readonly IApplicationService _applicationService;
        private readonly IAIService _aiService;
        
        private string _keywords;
        private string _location;
        private string _jobType;
        private List<string> _selectedPlatforms;
        private IEnumerable<JobListing> _searchResults;
        private JobListing _selectedJob;
        private bool _isBusy;
        private int _currentPage;
        private int _pageSize;
        private int _totalResults;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="JobSearchViewModel"/> class.
        /// </summary>
        /// <param name="jobDiscoveryService">The job discovery service.</param>
        /// <param name="applicationService">The application service.</param>
        /// <param name="aiService">The AI service.</param>
        public JobSearchViewModel(
            IJobDiscoveryService jobDiscoveryService,
            IApplicationService applicationService,
            IAIService aiService)
        {
            _jobDiscoveryService = jobDiscoveryService ?? throw new ArgumentNullException(nameof(jobDiscoveryService));
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
            _aiService = aiService ?? throw new ArgumentNullException(nameof(aiService));
            
            _selectedPlatforms = new List<string>();
            _currentPage = 1;
            _pageSize = 20;
            
            SearchCommand = new RelayCommand(async () => await SearchJobsAsync(), () => !IsBusy);
            ApplyCommand = new RelayCommand(async () => await ApplyToJobAsync(), () => SelectedJob != null && !IsBusy);
            SaveJobCommand = new RelayCommand(async () => await SaveJobAsync(), () => SelectedJob != null && !IsBusy);
            NextPageCommand = new RelayCommand(async () => await LoadNextPageAsync(), () => !IsBusy && HasNextPage);
            PreviousPageCommand = new RelayCommand(async () => await LoadPreviousPageAsync(), () => !IsBusy && HasPreviousPage);
            CreateSavedSearchCommand = new RelayCommand(async () => await CreateSavedSearchAsync(), () => !string.IsNullOrWhiteSpace(Keywords) && !IsBusy);
        }
        
        /// <summary>
        /// Gets or sets the search keywords.
        /// </summary>
        public string Keywords
        {
            get => _keywords;
            set
            {
                _keywords = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Gets or sets the search location.
        /// </summary>
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Gets or sets the job type filter.
        /// </summary>
        public string JobType
        {
            get => _jobType;
            set
            {
                _jobType = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Gets or sets the selected platforms.
        /// </summary>
        public List<string> SelectedPlatforms
        {
            get => _selectedPlatforms;
            set
            {
                _selectedPlatforms = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Gets or sets the search results.
        /// </summary>
        public IEnumerable<JobListing> SearchResults
        {
            get => _searchResults;
            set
            {
                _searchResults = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Gets or sets the selected job.
        /// </summary>
        public JobListing SelectedJob
        {
            get => _selectedJob;
            set
            {
                _selectedJob = value;
                OnPropertyChanged();
                (ApplyCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (SaveJobCommand as RelayCommand)?.RaiseCanExecuteChanged();
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
                (SearchCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (ApplyCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (SaveJobCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (NextPageCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (PreviousPageCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (CreateSavedSearchCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }
        
        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasNextPage));
                OnPropertyChanged(nameof(HasPreviousPage));
            }
        }
        
        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasNextPage));
            }
        }
        
        /// <summary>
        /// Gets or sets the total number of results.
        /// </summary>
        public int TotalResults
        {
            get => _totalResults;
            set
            {
                _totalResults = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasNextPage));
            }
        }
        
        /// <summary>
        /// Gets a value indicating whether there is a next page of results.
        /// </summary>
        public bool HasNextPage => CurrentPage * PageSize < TotalResults;
        
        /// <summary>
        /// Gets a value indicating whether there is a previous page of results.
        /// </summary>
        public bool HasPreviousPage => CurrentPage > 1;
        
        /// <summary>
        /// Gets the command to search for jobs.
        /// </summary>
        public ICommand SearchCommand { get; }
        
        /// <summary>
        /// Gets the command to apply to the selected job.
        /// </summary>
        public ICommand ApplyCommand { get; }
        
        /// <summary>
        /// Gets the command to save the selected job.
        /// </summary>
        public ICommand SaveJobCommand { get; }
        
        /// <summary>
        /// Gets the command to load the next page of results.
        /// </summary>
        public ICommand NextPageCommand { get; }
        
        /// <summary>
        /// Gets the command to load the previous page of results.
        /// </summary>
        public ICommand PreviousPageCommand { get; }
        
        /// <summary>
        /// Gets the command to create a saved search.
        /// </summary>
        public ICommand CreateSavedSearchCommand { get; }
        
        /// <summary>
        /// Searches for jobs based on the current criteria.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task SearchJobsAsync()
        {
            try
            {
                IsBusy = true;
                CurrentPage = 1;
                SearchResults = await _jobDiscoveryService.SearchJobsAsync(
                    Keywords,
                    Location,
                    JobType,
                    SelectedPlatforms,
                    CurrentPage,
                    PageSize);
                
                // In a real implementation, would get the total count from the service
                TotalResults = 100; // Mock value
            }
            catch (Exception ex)
            {
                // In a real implementation, would use a logging service
                Console.WriteLine($"Error searching jobs: {ex.Message}");
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        /// <summary>
        /// Applies to the selected job.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task ApplyToJobAsync()
        {
            if (SelectedJob == null)
            {
                return;
            }
            
            try
            {
                IsBusy = true;
                
                // In a real implementation, would show a dialog to confirm and collect additional information
                await _applicationService.CreateApplicationAsync(
                    1, // Mock profile ID
                    SelectedJob.JobId,
                    null, // Resume version
                    null, // Cover letter version
                    "Applied via Personal Job Agent");
            }
            catch (Exception ex)
            {
                // In a real implementation, would use a logging service
                Console.WriteLine($"Error applying to job: {ex.Message}");
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        /// <summary>
        /// Saves the selected job.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task SaveJobAsync()
        {
            if (SelectedJob == null)
            {
                return;
            }
            
            try
            {
                IsBusy = true;
                await _jobDiscoveryService.SaveJobAsync(SelectedJob);
            }
            catch (Exception ex)
            {
                // In a real implementation, would use a logging service
                Console.WriteLine($"Error saving job: {ex.Message}");
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        /// <summary>
        /// Loads the next page of results.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task LoadNextPageAsync()
        {
            if (!HasNextPage)
            {
                return;
            }
            
            try
            {
                IsBusy = true;
                CurrentPage++;
                SearchResults = await _jobDiscoveryService.SearchJobsAsync(
                    Keywords,
                    Location,
                    JobType,
                    SelectedPlatforms,
                    CurrentPage,
                    PageSize);
            }
            catch (Exception ex)
            {
                // In a real implementation, would use a logging service
                Console.WriteLine($"Error loading next page: {ex.Message}");
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        /// <summary>
        /// Loads the previous page of results.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task LoadPreviousPageAsync()
        {
            if (!HasPreviousPage)
            {
                return;
            }
            
            try
            {
                IsBusy = true;
                CurrentPage--;
                SearchResults = await _jobDiscoveryService.SearchJobsAsync(
                    Keywords,
                    Location,
                    JobType,
                    SelectedPlatforms,
                    CurrentPage,
                    PageSize);
            }
            catch (Exception ex)
            {
                // In a real implementation, would use a logging service
                Console.WriteLine($"Error loading previous page: {ex.Message}");
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        /// <summary>
        /// Creates a saved search based on the current criteria.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task CreateSavedSearchAsync()
        {
            try
            {
                IsBusy = true;
                
                // In a real implementation, would show a dialog to name the saved search
                string name = $"Search for {Keywords} in {Location}";
                
                await _jobDiscoveryService.CreateSavedSearchAsync(
                    1, // Mock profile ID
                    name,
                    Keywords,
                    Location,
                    JobType,
                    SelectedPlatforms);
            }
            catch (Exception ex)
            {
                // In a real implementation, would use a logging service
                Console.WriteLine($"Error creating saved search: {ex.Message}");
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
