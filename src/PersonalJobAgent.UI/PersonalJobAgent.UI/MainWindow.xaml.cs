using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using PersonalJobAgent.UI.ViewModels;

namespace PersonalJobAgent.UI
{
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, Type> _viewModelTypes;

        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            // Map view names to ViewModel types
            _viewModelTypes = new Dictionary<string, Type>
            {
                { "Dashboard", typeof(DashboardViewModel) },
                { "Profile", typeof(ProfileViewModel) },
                { "JobSearch", typeof(JobSearchViewModel) },
                { "Applications", typeof(ApplicationTrackingViewModel) }
            };

            // Set initial view to Dashboard
            NavigateToView("Dashboard");
        }

        private void NavigationList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NavigationList.SelectedItem is ListBoxItem selectedItem)
            {
                string viewName = null;

                if (selectedItem == DashboardItem)
                    viewName = "Dashboard";
                else if (selectedItem == ProfileItem)
                    viewName = "Profile";
                else if (selectedItem == JobSearchItem)
                    viewName = "JobSearch";
                else if (selectedItem == ApplicationsItem)
                    viewName = "Applications";

                if (viewName != null)
                    NavigateToView(viewName);
            }
        }

        private void NavigateToView(string viewName)
        {
            // Update status
            StatusText.Text = $"Loading {viewName} view...";
            LoadingIndicator.Visibility = Visibility.Visible;

            try
            {
                // Create the appropriate view
                Page view = null;

                switch (viewName)
                {
                    case "Dashboard":
                        view = _serviceProvider.GetRequiredService<Views.DashboardView>();
                        break;
                    case "Profile":
                        view = _serviceProvider.GetRequiredService<Views.ProfileView>();
                        break;
                    case "JobSearch":
                      //  view = _serviceProvider.GetRequiredService<Views.JobSearchView>();
                        break;
                    case "Applications":
                      //  view = _serviceProvider.GetRequiredService<Views.ApplicationsView>();
                        break;
                }

                // Navigate to the view
                if (view != null)
                {
                    // Get the appropriate ViewModel from the service provider
                    if (_viewModelTypes.TryGetValue(viewName, out Type viewModelType))
                    {
                        var viewModel = _serviceProvider.GetService(viewModelType);
                        if (viewModel != null)
                        {
                            view.DataContext = viewModel;
                        }
                    }

                    ContentFrame.Navigate(view);
                }

                // Update status
                StatusText.Text = $"{viewName} view loaded";
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Error loading {viewName} view: {ex.Message}";
            }
            finally
            {
                LoadingIndicator.Visibility = Visibility.Collapsed;
            }
        }
    }
}
