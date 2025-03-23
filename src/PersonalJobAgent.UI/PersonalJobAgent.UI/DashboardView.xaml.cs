using System;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace PersonalJobAgent.UI.Views
{
    public partial class DashboardView : Page
    {
        private readonly ViewModels.DashboardViewModel _viewModel;

        public DashboardView(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            // Get the ViewModel from the service provider
            _viewModel = serviceProvider.GetRequiredService<ViewModels.DashboardViewModel>();
            DataContext = _viewModel;

            // Initialize the ViewModel and load data
            _viewModel.InitializeAsync();
        }
    }
}
