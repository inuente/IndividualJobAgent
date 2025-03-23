using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using PersonalJobAgent.Core.Models;
using PersonalJobAgent.Core.Interfaces;
using CommunityToolkit.Mvvm.Input;

namespace PersonalJobAgent.UI.ViewModels
{
    /// <summary>
    /// ViewModel for the application tracking view.
    /// </summary>
    public class ApplicationTrackingViewModel : ViewModelBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IJobDiscoveryService _jobDiscoveryService;
        
        private IEnumerable<Application> _applications;
        private Application _selectedApplication;
        private string _statusFilter;
        private bool _isBusy;
        private object _applicationStats;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationTrackingViewModel"/> class.
        /// </summary>
        /// <param name="applicationService">The application service.</param>
        /// <param name="jobDiscoveryService">The job discovery service.</param>
        public ApplicationTrackingViewModel(
            IApplicationService applicationService,
            IJobDiscoveryService jobDiscoveryService)
        {
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
            _jobDiscoveryService = jobDiscoveryService ?? throw new ArgumentNullException(nameof(jobDiscoveryService));
            
            UpdateStatusCommand = new RelayCommand<string>(async (status) => await UpdateApplicationStatusAsync(status), (_) => SelectedApplication != null && !IsBusy);
            AddNotesCommand = new RelayCommand<string>(async (notes) => await UpdateApplicationNotesAsync(notes), (_) => SelectedApplication != null && !IsBusy);
            FilterByStatusCommand = new RelayCommand<string>(async (status) => await FilterByStatusAsync(status), (_) => !IsBusy);
            ViewAllApplicationsCommand = new RelayCommand(async () => await LoadApplicationsAsync(), () => !IsBusy);
        }
        
        /// <summary>
        /// Gets or sets the applications.
        /// </summary>
        public IEnumerable<Application> Applications
        {
            get => _applications;
            set
            {
                _applications = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Gets or sets the selected application.
        /// </summary>
        public Application SelectedApplication
        {
            get => _selectedApplication;
            set
            {
                _selectedApplication = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();               
            }
        }
        
        /// <summary>
        /// Gets or sets the status filter.
        /// </summary>
        public string StatusFilter
        {
            get => _statusFilter;
            set
            {
                _statusFilter = value;
                OnPropertyChanged();
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
                (UpdateStatusCommand as RelayCommand<string>)?.NotifyCanExecuteChanged();
                (AddNotesCommand as RelayCommand<string>)?.NotifyCanExecuteChanged();
                (FilterByStatusCommand as RelayCommand<string>)?.NotifyCanExecuteChanged();
                (ViewAllApplicationsCommand as RelayCommand)?.NotifyCanExecuteChanged();
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
        /// Gets the command to update the application status.
        /// </summary>
        public ICommand UpdateStatusCommand { get; }
        
        /// <summary>
        /// Gets the command to add notes to the application.
        /// </summary>
        public ICommand AddNotesCommand { get; }
        
        /// <summary>
        /// Gets the command to filter applications by status.
        /// </summary>
        public ICommand FilterByStatusCommand { get; }
        
        /// <summary>
        /// Gets the command to view all applications.
        /// </summary>
        public ICommand ViewAllApplicationsCommand { get; }
        
        /// <summary>
        /// Loads applications for the specified profile.
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task LoadApplicationsAsync(int profileId = 1)
        {
            try
            {
                IsBusy = true;
                Applications = await _applicationService.GetApplicationsForProfileAsync(profileId);
                ApplicationStats = await _applicationService.GetApplicationStatisticsAsync(profileId);
                StatusFilter = null;
            }
            catch (Exception ex)
            {
                // In a real implementation, would use a logging service
                Console.WriteLine($"Error loading applications: {ex.Message}");
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        /// <summary>
        /// Updates the status of the selected application.
        /// </summary>
        /// <param name="status">The new status.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task UpdateApplicationStatusAsync(string status)
        {
            if (SelectedApplication == null || string.IsNullOrWhiteSpace(status))
            {
                return;
            }
            
            try
            {
                IsBusy = true;
                SelectedApplication = await _applicationService.UpdateApplicationStatusAsync(
                    SelectedApplication.Id,
                    status,
                    $"Status updated to {status}");
                
                // Refresh the applications list
                await LoadApplicationsAsync();
            }
            catch (Exception ex)
            {
                // In a real implementation, would use a logging service
                Console.WriteLine($"Error updating application status: {ex.Message}");
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        /// <summary>
        /// Updates the notes of the selected application.
        /// </summary>
        /// <param name="notes">The new notes.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task UpdateApplicationNotesAsync(string notes)
        {
            if (SelectedApplication == null)
            {
                return;
            }
            
            try
            {
                IsBusy = true;
                SelectedApplication = await _applicationService.UpdateApplicationStatusAsync(
                    SelectedApplication.Id,
                    notes);
            }
            catch (Exception ex)
            {
                // In a real implementation, would use a logging service
                Console.WriteLine($"Error updating application notes: {ex.Message}");
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        /// <summary>
        /// Filters applications by status.
        /// </summary>
        /// <param name="status">The status to filter by.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task FilterByStatusAsync(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                await LoadApplicationsAsync();
                return;
            }
            
            try
            {
                IsBusy = true;
                StatusFilter = status;
                Applications = await _applicationService.GetApplicationsByStatusAsync(1, status); // Mock profile ID
            }
            catch (Exception ex)
            {
                // In a real implementation, would use a logging service
                Console.WriteLine($"Error filtering applications: {ex.Message}");
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
