# Personal Job Agent - Project README

## Overview

Personal Job Agent is a comprehensive Windows desktop application designed to automate and optimize the job search process. It provides tools for managing job applications, tracking progress, and leveraging AI to improve job search outcomes.

## Key Features

- Profile management with resume parsing
- Automated job discovery and matching
- Application tracking and status management
- AI-powered cover letter generation
- Interview preparation assistance
- Integration with popular job platforms

## Project Structure

```
PersonalJobAgent/
├── src/                           # Source code
│   ├── PersonalJobAgent.UI/       # WPF UI project
│   ├── PersonalJobAgent.Core/     # Core business logic
│   ├── PersonalJobAgent.Data/     # Data access layer
│   ├── PersonalJobAgent.AI/       # AI integration components
│   ├── PersonalJobAgent.Common/   # Shared utilities
│   └── PersonalJobAgent.Services/ # External services integration
├── docs/                          # Documentation
│   ├── ImplementationGuide.md     # Detailed implementation instructions
│   ├── WindowsStoreDeploymentGuide.md # Windows Store submission guide
│   └── screenshots/               # UI mockups and screenshots
└── README.md                      # This file
```

## Technology Stack

- **Frontend**: WPF (Windows Presentation Foundation) with MVVM pattern
- **Backend**: .NET 6.0, C#
- **Database**: SQL Server Express LocalDB
- **AI Components**: Python 3.10 with spaCy, scikit-learn, and sentence-transformers
- **Integration**: Python.NET for C# to Python communication

## Getting Started

Please refer to the [Implementation Guide](docs/ImplementationGuide.md) for detailed instructions on setting up the development environment and implementing the application.

## Windows Store Deployment

For information on packaging and submitting the application to the Windows Store, see the [Windows Store Deployment Guide](docs/WindowsStoreDeploymentGuide.md).

## Requirements

- Windows 10/11 (64-bit)
- .NET 6.0 Desktop Runtime
- SQL Server Express LocalDB 2019 or later
- Python 3.10
- Visual C++ Redistributable 2019

## License

This project is licensed under the MIT License - see the LICENSE file for details.
