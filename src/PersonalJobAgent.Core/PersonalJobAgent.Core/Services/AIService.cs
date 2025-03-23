using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PersonalJobAgent.Core.Interfaces;
using Python.Runtime;

namespace PersonalJobAgent.Core.Services
{
    /// <summary>
    /// Implementation of the AI service interface using Python.NET
    /// </summary>
    public class AIService : IAIService
    {
        private readonly ILogger<AIService> _logger;
        private readonly string _pythonScriptsPath;
        private bool _isPythonInitialized = false;        
        
        public AIService(string pythonScriptsPath)
        {
              _pythonScriptsPath = pythonScriptsPath;
        }


        public AIService(ILogger<AIService> logger)
        {
            _logger = logger;
            
            // Path to Python scripts directory - adjust as needed for your environment
            _pythonScriptsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PythonScripts");
        }

        /// <summary>
        /// Initializes the Python runtime
        /// </summary>
        private void InitializePython()
        {
            if (_isPythonInitialized)
                return;

            try
            {
                // Set Python home to the virtual environment
                string pythonHome = Environment.GetEnvironmentVariable("PYTHONHOME");
                if (string.IsNullOrEmpty(pythonHome))
                {
                    // Default to a common location if not set
                    pythonHome = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "venv");
                    Environment.SetEnvironmentVariable("PYTHONHOME", pythonHome);
                }

                // Set Python path to include our scripts directory
                string pythonPath = Environment.GetEnvironmentVariable("PYTHONPATH") ?? "";
                if (!pythonPath.Contains(_pythonScriptsPath))
                {
                    pythonPath = string.IsNullOrEmpty(pythonPath) 
                        ? _pythonScriptsPath 
                        : $"{pythonPath}{Path.PathSeparator}{_pythonScriptsPath}";
                    Environment.SetEnvironmentVariable("PYTHONPATH", pythonPath);
                }

                // Initialize Python runtime
                PythonEngine.Initialize();
                _isPythonInitialized = true;
                _logger.LogInformation("Python runtime initialized successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize Python runtime");
                throw new InvalidOperationException("Failed to initialize Python runtime", ex);
            }
        }

        /// <summary>
        /// Parses a resume and extracts structured information
        /// </summary>
        public async Task<string> ParseResumeAsync(string resumeText)
        {
            return await Task.Run(() =>
            {
                InitializePython();

                using (Py.GIL())
                {
                    try
                    {
                        // Import the resume parser module
                        dynamic resumeParser = Py.Import("resume_parser");
                        
                        // Call the parse_resume function
                        dynamic result = resumeParser.parse_resume(resumeText);
                        
                        // Convert Python dictionary to JSON string
                        return result.ToString();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error parsing resume");
                        throw new InvalidOperationException("Error parsing resume", ex);
                    }
                }
            });
        }

        /// <summary>
        /// Matches a user profile with job listings
        /// </summary>
        public async Task<string> MatchJobsAsync(string userProfileJson, string jobListingsJson)
        {
            return await Task.Run(() =>
            {
                InitializePython();

                using (Py.GIL())
                {
                    try
                    {
                        // Import the job matcher module
                        dynamic jobMatcher = Py.Import("job_matcher");
                        
                        // Call the match_jobs function
                        dynamic result = jobMatcher.match_jobs(userProfileJson, jobListingsJson);
                        
                        // Convert Python dictionary to JSON string
                        return result.ToString();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error matching jobs");
                        throw new InvalidOperationException("Error matching jobs", ex);
                    }
                }
            });
        }

        /// <summary>
        /// Generates a cover letter based on user profile and job listing
        /// </summary>
        public async Task<string> GenerateCoverLetterAsync(string userProfileJson, string jobListingJson)
        {
            return await Task.Run(() =>
            {
                InitializePython();

                using (Py.GIL())
                {
                    try
                    {
                        // Import the cover letter generator module
                        dynamic coverLetterGenerator = Py.Import("cover_letter_generator");
                        
                        // Call the generate_cover_letter function
                        dynamic result = coverLetterGenerator.generate_cover_letter(userProfileJson, jobListingJson);
                        
                        // Return the generated cover letter text
                        return result.ToString();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error generating cover letter");
                        throw new InvalidOperationException("Error generating cover letter", ex);
                    }
                }
            });
        }

        /// <summary>
        /// Prepares interview questions and tips based on job listing
        /// </summary>
        public async Task<string> PrepareInterviewAsync(string userProfileJson, string jobListingJson)
        {
            return await Task.Run(() =>
            {
                InitializePython();

                using (Py.GIL())
                {
                    try
                    {
                        // Import the interview preparation module
                        dynamic interviewPreparation = Py.Import("interview_preparation");
                        
                        // Call the prepare_interview function
                        dynamic result = interviewPreparation.prepare_interview(userProfileJson, jobListingJson);
                        
                        // Convert Python dictionary to JSON string
                        return result.ToString();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error preparing interview");
                        throw new InvalidOperationException("Error preparing interview", ex);
                    }
                }
            });
        }

        /// <summary>
        /// Disposes the Python runtime
        /// </summary>
        public void Dispose()
        {
            if (_isPythonInitialized)
            {
                try
                {
                    PythonEngine.Shutdown();
                    _isPythonInitialized = false;
                    _logger.LogInformation("Python runtime shut down successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error shutting down Python runtime");
                }
            }
        }
    }
}
