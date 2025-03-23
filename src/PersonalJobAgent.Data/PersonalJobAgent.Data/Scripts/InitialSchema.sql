-- Initial Database Schema for Personal Job Agent
-- SQL Server script

-- Create UserProfile table
CREATE TABLE UserProfile (
    ProfileID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(20),
    Location NVARCHAR(255),
    Title NVARCHAR(255),
    Summary NTEXT,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    LastModifiedDate DATETIME NOT NULL DEFAULT GETDATE()
);

-- Create Skill table
CREATE TABLE Skill (
    SkillID INT PRIMARY KEY IDENTITY(1,1),
    ProfileID INT FOREIGN KEY REFERENCES UserProfile(ProfileID) ON DELETE CASCADE,
    Name NVARCHAR(100) NOT NULL,
    Category NVARCHAR(50),
    Proficiency INT, -- 1-5 scale
    YearsExperience DECIMAL(5,2),
    IsHighlighted BIT DEFAULT 0
);

-- Create WorkExperience table
CREATE TABLE WorkExperience (
    ExperienceID INT PRIMARY KEY IDENTITY(1,1),
    ProfileID INT FOREIGN KEY REFERENCES UserProfile(ProfileID) ON DELETE CASCADE,
    CompanyName NVARCHAR(255) NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    Location NVARCHAR(255),
    StartDate DATE NOT NULL,
    EndDate DATE,
    IsCurrentPosition BIT DEFAULT 0,
    Description NTEXT
);

-- Create Education table
CREATE TABLE Education (
    EducationID INT PRIMARY KEY IDENTITY(1,1),
    ProfileID INT FOREIGN KEY REFERENCES UserProfile(ProfileID) ON DELETE CASCADE,
    Institution NVARCHAR(255) NOT NULL,
    Degree NVARCHAR(255),
    FieldOfStudy NVARCHAR(255),
    StartDate DATE,
    EndDate DATE,
    GPA DECIMAL(3,2),
    Description NTEXT
);

-- Create JobListing table
CREATE TABLE JobListing (
    JobID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL,
    Company NVARCHAR(255) NOT NULL,
    Location NVARCHAR(255),
    Description NTEXT,
    JobType NVARCHAR(50),
    Salary NVARCHAR(100),
    PostedDate DATETIME,
    ClosingDate DATETIME,
    SourcePlatform NVARCHAR(50),
    ExternalJobID NVARCHAR(100),
    URL NVARCHAR(500),
    IsActive BIT DEFAULT 1
);

-- Create Application table
CREATE TABLE Application (
    ApplicationID INT PRIMARY KEY IDENTITY(1,1),
    ProfileID INT FOREIGN KEY REFERENCES UserProfile(ProfileID) ON DELETE CASCADE,
    JobID INT FOREIGN KEY REFERENCES JobListing(JobID) ON DELETE CASCADE,
    ApplicationDate DATETIME NOT NULL DEFAULT GETDATE(),
    Status NVARCHAR(50) NOT NULL, -- Applied, Interviewing, Rejected, Offered, etc.
    ResumeVersion INT,
    CoverLetterVersion INT,
    Notes NTEXT,
    LastStatusUpdateDate DATETIME NOT NULL DEFAULT GETDATE()
);

-- Create PlatformCredential table
CREATE TABLE PlatformCredential (
    CredentialID INT PRIMARY KEY IDENTITY(1,1),
    ProfileID INT FOREIGN KEY REFERENCES UserProfile(ProfileID) ON DELETE CASCADE,
    PlatformName NVARCHAR(50) NOT NULL,
    Username NVARCHAR(255),
    EncryptedPassword VARBINARY(MAX),
    AccessToken NVARCHAR(MAX),
    RefreshToken NVARCHAR(MAX),
    TokenExpiry DATETIME,
    LastSyncDate DATETIME
);

-- Create Document table for storing resumes, cover letters, etc.
CREATE TABLE Document (
    DocumentID INT PRIMARY KEY IDENTITY(1,1),
    ProfileID INT FOREIGN KEY REFERENCES UserProfile(ProfileID) ON DELETE CASCADE,
    DocumentType NVARCHAR(50) NOT NULL, -- Resume, CoverLetter, etc.
    Version INT NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    Content NVARCHAR(MAX),
    BinaryContent VARBINARY(MAX),
    ContentType NVARCHAR(100),
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);

-- Create SavedSearch table
CREATE TABLE SavedSearch (
    SavedSearchID INT PRIMARY KEY IDENTITY(1,1),
    ProfileID INT FOREIGN KEY REFERENCES UserProfile(ProfileID) ON DELETE CASCADE,
    Name NVARCHAR(255) NOT NULL,
    Keywords NVARCHAR(255),
    Location NVARCHAR(255),
    JobType NVARCHAR(50),
    Platforms NVARCHAR(255), -- Comma-separated list of platforms
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    LastRunDate DATETIME
);

-- Create ApplicationHistory table for tracking status changes
CREATE TABLE ApplicationHistory (
    HistoryID INT PRIMARY KEY IDENTITY(1,1),
    ApplicationID INT FOREIGN KEY REFERENCES Application(ApplicationID) ON DELETE CASCADE,
    OldStatus NVARCHAR(50),
    NewStatus NVARCHAR(50) NOT NULL,
    Notes NTEXT,
    ChangedDate DATETIME NOT NULL DEFAULT GETDATE()
);

-- Create Interview table
CREATE TABLE Interview (
    InterviewID INT PRIMARY KEY IDENTITY(1,1),
    ApplicationID INT FOREIGN KEY REFERENCES Application(ApplicationID) ON DELETE CASCADE,
    InterviewDate DATETIME NOT NULL,
    InterviewType NVARCHAR(50), -- Phone, Video, In-person, etc.
    InterviewerName NVARCHAR(255),
    Location NVARCHAR(255),
    Notes NTEXT,
    Feedback NTEXT,
    Status NVARCHAR(50) -- Scheduled, Completed, Cancelled, etc.
);

-- Create InterviewQuestion table
CREATE TABLE InterviewQuestion (
    QuestionID INT PRIMARY KEY IDENTITY(1,1),
    InterviewID INT FOREIGN KEY REFERENCES Interview(InterviewID) ON DELETE CASCADE,
    Question NTEXT NOT NULL,
    Answer NTEXT,
    IsUserGenerated BIT DEFAULT 0
);

-- Create AIModelSettings table
CREATE TABLE AIModelSettings (
    SettingID INT PRIMARY KEY IDENTITY(1,1),
    ProfileID INT FOREIGN KEY REFERENCES UserProfile(ProfileID) ON DELETE CASCADE,
    SettingName NVARCHAR(100) NOT NULL,
    SettingValue NVARCHAR(MAX),
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    LastModifiedDate DATETIME NOT NULL DEFAULT GETDATE()
);

-- Create indexes for performance
CREATE INDEX IX_Skill_ProfileID ON Skill(ProfileID);
CREATE INDEX IX_WorkExperience_ProfileID ON WorkExperience(ProfileID);
CREATE INDEX IX_Education_ProfileID ON Education(ProfileID);
CREATE INDEX IX_Application_ProfileID ON Application(ProfileID);
CREATE INDEX IX_Application_JobID ON Application(JobID);
CREATE INDEX IX_PlatformCredential_ProfileID ON PlatformCredential(ProfileID);
CREATE INDEX IX_Document_ProfileID ON Document(ProfileID);
CREATE INDEX IX_SavedSearch_ProfileID ON SavedSearch(ProfileID);
CREATE INDEX IX_ApplicationHistory_ApplicationID ON ApplicationHistory(ApplicationID);
CREATE INDEX IX_Interview_ApplicationID ON Interview(ApplicationID);
CREATE INDEX IX_InterviewQuestion_InterviewID ON InterviewQuestion(InterviewID);
CREATE INDEX IX_AIModelSettings_ProfileID ON AIModelSettings(ProfileID);

-- Create full-text search catalog
CREATE FULLTEXT CATALOG ft_catalog AS DEFAULT;

-- Create full-text index on JobListing
CREATE FULLTEXT INDEX ON JobListing(Title, Company, Description) 
KEY INDEX PK_JobListing_JobID
WITH STOPLIST = SYSTEM;

-- Create full-text index on UserProfile
CREATE FULLTEXT INDEX ON UserProfile(FirstName, LastName, Summary) 
KEY INDEX PK_UserProfile_ProfileID
WITH STOPLIST = SYSTEM;
