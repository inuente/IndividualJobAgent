using System;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace PersonalJobAgent.UI.Views
{
    /// <summary>
    /// Interaction logic for JobSearchView.xaml
    /// </summary>
    public partial class JobSearchView : Page
    {
        private readonly ViewModels.JobSearchViewModel _viewModel;

        public JobSearchView(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            // Get the ViewModel from the service provider
            _viewModel = serviceProvider.GetRequiredService<ViewModels.JobSearchViewModel>();
            DataContext = _viewModel;

            // No need to call initialization method as the ViewModel doesn't have InitializeAsync
            // Initial search will be triggered by user via the search button
        }
    }
}
