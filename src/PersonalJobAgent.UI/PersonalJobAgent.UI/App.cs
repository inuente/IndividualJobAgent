using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PersonalJobAgent.Core.Interfaces;
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
    }

    public class JobDiscoveryService : IJobDiscoveryService
    {
        public JobDiscoveryService() { }
    }

    public class ApplicationService : IApplicationService
    {
        public ApplicationService() { }
    }
}
