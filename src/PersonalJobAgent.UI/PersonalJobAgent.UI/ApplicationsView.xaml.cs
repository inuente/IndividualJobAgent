using System;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace PersonalJobAgent.UI.Views
{
    /// <summary>
    /// Interaction logic for ApplicationsView.xaml
    /// </summary>
    public partial class ApplicationsView : Page
    {
        private readonly ViewModels.ApplicationTrackingViewModel _viewModel;

        public ApplicationsView(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            // Get the ViewModel from the service provider
            _viewModel = serviceProvider.GetRequiredService<ViewModels.ApplicationTrackingViewModel>();
            DataContext = _viewModel;

            // Initialize the ViewModel and load data
            _viewModel.LoadApplicationsAsync();
        }
    }
}
