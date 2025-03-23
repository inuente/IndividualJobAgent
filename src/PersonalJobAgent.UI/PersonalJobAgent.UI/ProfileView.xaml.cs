using System;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace PersonalJobAgent.UI.Views
{
    public partial class ProfileView : Page
    {
        private readonly ViewModels.ProfileViewModel _viewModel;

        public ProfileView(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            // Get the ViewModel from the service provider
            _viewModel = serviceProvider.GetRequiredService<ViewModels.ProfileViewModel>();
            DataContext = _viewModel;

            // Load the current user's profile (using ID 1 for now)
            _viewModel.LoadProfileAsync(1);
        }
    }
}
