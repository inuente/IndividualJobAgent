-- Sample Data Script for Personal Job Agent
-- SQL Server script for populating the database with sample data

-- Insert sample user profiles
INSERT INTO UserProfile (FirstName, LastName, Email, PhoneNumber, Location, Title, Summary)
VALUES 
('John', 'Doe', 'john.doe@example.com', '(555) 123-4567', 'New York, NY', 'Senior Software Engineer', 
'Experienced software engineer with 8+ years of experience in full-stack development, specializing in .NET and React.'),
('Jane', 'Smith', 'jane.smith@example.com', '(555) 987-6543', 'San Francisco, CA', 'UX Designer', 
'Creative UX designer with 5 years of experience creating intuitive user experiences for web and mobile applications.');

-- Insert sample skills for John Doe
INSERT INTO Skill (ProfileID, Name, Category, Proficiency, YearsExperience, IsHighlighted)
VALUES 
(1, 'C#', 'Programming', 5, 8.0, 1),
(1, '.NET Core', 'Framework', 5, 6.0, 1),
(1, 'React', 'Frontend', 4, 4.0, 1),
(1, 'SQL Server', 'Database', 4, 7.0, 1),
(1, 'Azure', 'Cloud', 3, 3.0, 0),
(1, 'Git', 'Tools', 4, 7.0, 0),
(1, 'JavaScript', 'Programming', 4, 6.0, 0),
(1, 'TypeScript', 'Programming', 4, 4.0, 0);

-- Insert sample skills for Jane Smith
INSERT INTO Skill (ProfileID, Name, Category, Proficiency, YearsExperience, IsHighlighted)
VALUES 
(2, 'Figma', 'Design', 5, 4.0, 1),
(2, 'Adobe XD', 'Design', 5, 5.0, 1),
(2, 'Sketch', 'Design', 4, 3.0, 1),
(2, 'User Research', 'Research', 4, 5.0, 1),
(2, 'Prototyping', 'Design', 5, 5.0, 0),
(2, 'HTML/CSS', 'Frontend', 3, 4.0, 0),
(2, 'JavaScript', 'Programming', 2, 2.0, 0),
(2, 'Usability Testing', 'Research', 4, 4.0, 0);

-- Insert sample work experience for John Doe
INSERT INTO WorkExperience (ProfileID, CompanyName, Title, Location, StartDate, EndDate, IsCurrentPosition, Description)
VALUES 
(1, 'Tech Solutions Inc.', 'Senior Software Engineer', 'New York, NY', '2020-03-15', NULL, 1, 
'Leading development of enterprise web applications using .NET Core and React. Implemented CI/CD pipelines and mentored junior developers.'),
(1, 'Digital Innovations', 'Software Developer', 'Boston, MA', '2017-06-01', '2020-03-01', 0, 
'Developed and maintained web applications using ASP.NET MVC and Angular. Optimized database queries resulting in 30% performance improvement.'),
(1, 'StartUp Labs', 'Junior Developer', 'Boston, MA', '2015-01-15', '2017-05-30', 0, 
'Assisted in development of mobile applications using Xamarin. Implemented RESTful APIs and integrated third-party services.');

-- Insert sample work experience for Jane Smith
INSERT INTO WorkExperience (ProfileID, CompanyName, Title, Location, StartDate, EndDate, IsCurrentPosition, Description)
VALUES 
(2, 'Creative Design Co.', 'Senior UX Designer', 'San Francisco, CA', '2021-02-01', NULL, 1, 
'Leading UX design for enterprise SaaS products. Conducting user research, creating wireframes, prototypes, and collaborating with development teams.'),
(2, 'User First Design', 'UX Designer', 'Los Angeles, CA', '2018-07-15', '2021-01-15', 0, 
'Designed user interfaces for mobile applications. Conducted usability testing and implemented design systems.'),
(2, 'Web Creations', 'UI Designer', 'San Diego, CA', '2016-03-01', '2018-07-01', 0, 
'Created visual designs for websites and web applications. Collaborated with developers to ensure design implementation.');

-- Insert sample education for John Doe
INSERT INTO Education (ProfileID, Institution, Degree, FieldOfStudy, StartDate, EndDate, GPA, Description)
VALUES 
(1, 'Massachusetts Institute of Technology', 'Bachelor of Science', 'Computer Science', '2011-09-01', '2015-05-15', 3.8, 
'Focused on software engineering and database systems. Participated in hackathons and coding competitions.');

-- Insert sample education for Jane Smith
INSERT INTO Education (ProfileID, Institution, Degree, FieldOfStudy, StartDate, EndDate, GPA, Description)
VALUES 
(2, 'Rhode Island School of Design', 'Bachelor of Fine Arts', 'Graphic Design', '2012-09-01', '2016-05-15', 3.9, 
'Specialized in digital design and user experience. Completed capstone project on mobile app design.');

-- Insert sample job listings
INSERT INTO JobListing (Title, Company, Location, Description, JobType, Salary, PostedDate, ClosingDate, SourcePlatform, ExternalJobID, URL, IsActive)
VALUES 
('Senior .NET Developer', 'Enterprise Solutions', 'New York, NY', 
'We are seeking an experienced .NET Developer to join our team. The ideal candidate will have strong experience with C#, .NET Core, and React.

Responsibilities:
• Design and develop web applications using .NET Core and React
• Write clean, maintainable, and efficient code
• Collaborate with cross-functional teams
• Mentor junior developers
• Participate in code reviews

Requirements:
• 5+ years of experience in software development
• Strong proficiency in C#, .NET Core, and React
• Experience with SQL Server and Entity Framework
• Knowledge of RESTful APIs
• Familiarity with Git and CI/CD pipelines
• Bachelor''s degree in Computer Science or related field

We offer competitive salary, flexible working hours, and opportunities for professional growth.',
'Full-time', '$120,000 - $150,000', '2025-03-01', '2025-04-15', 'LinkedIn', 'LI-12345', 'https://example.com/jobs/senior-net-developer', 1),

('UX/UI Designer', 'Creative Tech', 'San Francisco, CA', 
'Creative Tech is looking for a talented UX/UI Designer to create amazing user experiences for our products.

Responsibilities:
• Create user flows, wireframes, and prototypes
• Conduct user research and usability testing
• Design intuitive and visually appealing interfaces
• Collaborate with developers to implement designs
• Maintain and evolve our design system

Requirements:
• 3+ years of experience in UX/UI design
• Proficiency in design tools such as Figma, Adobe XD, or Sketch
• Strong portfolio demonstrating UX process and UI skills
• Experience with user research and usability testing
• Understanding of HTML/CSS is a plus
• Bachelor''s degree in Design, HCI, or related field

We offer a creative work environment, competitive compensation, and excellent benefits.',
'Full-time', '$100,000 - $130,000', '2025-03-05', '2025-04-10', 'Indeed', 'IND-67890', 'https://example.com/jobs/ux-ui-designer', 1),

('Full Stack Developer', 'Tech Innovators', 'Remote', 
'Tech Innovators is seeking a Full Stack Developer to join our remote team.

Responsibilities:
• Develop and maintain web applications using modern technologies
• Work with both frontend and backend technologies
• Implement responsive design and ensure cross-browser compatibility
• Optimize applications for maximum speed and scalability
• Collaborate with team members via remote tools

Requirements:
• 4+ years of experience in full stack development
• Proficiency in JavaScript, TypeScript, and React
• Experience with Node.js, Express, and MongoDB
• Knowledge of RESTful APIs and GraphQL
• Familiarity with Git and CI/CD pipelines
• Strong communication skills for remote collaboration

We offer flexible working hours, competitive salary, and a supportive remote work environment.',
'Full-time', '$110,000 - $140,000', '2025-03-10', '2025-04-20', 'Stack Overflow', 'SO-54321', 'https://example.com/jobs/full-stack-developer', 1);

-- Insert sample applications
INSERT INTO Application (ProfileID, JobID, ApplicationDate, Status, ResumeVersion, CoverLetterVersion, Notes, LastStatusUpdateDate)
VALUES 
(1, 1, '2025-03-05', 'Applied', 1, 1, 'Applied through LinkedIn Easy Apply', '2025-03-05'),
(1, 3, '2025-03-12', 'Interviewing', 2, 2, 'Phone interview scheduled for March 20', '2025-03-15'),
(2, 2, '2025-03-07', 'Applied', 1, 1, 'Submitted portfolio along with application', '2025-03-07');

-- Insert sample application history
INSERT INTO ApplicationHistory (ApplicationID, OldStatus, NewStatus, Notes, ChangedDate)
VALUES 
(1, NULL, 'Applied', 'Initial application submitted', '2025-03-05'),
(2, NULL, 'Applied', 'Initial application submitted', '2025-03-12'),
(2, 'Applied', 'Interviewing', 'Received email for interview request', '2025-03-15'),
(3, NULL, 'Applied', 'Initial application submitted', '2025-03-07');

-- Insert sample interviews
INSERT INTO Interview (ApplicationID, InterviewDate, InterviewType, InterviewerName, Location, Notes, Feedback, Status)
VALUES 
(2, '2025-03-20 14:00:00', 'Phone', 'Sarah Johnson', 'N/A', 'Initial screening interview', NULL, 'Scheduled');

-- Insert sample interview questions
INSERT INTO InterviewQuestion (InterviewID, Question, Answer, IsUserGenerated)
VALUES 
(1, 'Tell me about your experience with React and TypeScript', 'I have 4 years of experience using React for frontend development and have been using TypeScript for the last 3 years to improve code quality and maintainability.', 1),
(1, 'How do you handle state management in large applications?', 'For large applications, I prefer using Redux for global state management, combined with React Context for more localized state. I also follow best practices like normalizing state and using selectors for derived data.', 1),
(1, 'Describe a challenging technical problem you solved recently', 'Recently, I had to optimize a data-heavy dashboard that was experiencing performance issues. I implemented virtualization for large lists, memoized expensive calculations, and refactored components to minimize re-renders.', 1);

-- Insert sample platform credentials
INSERT INTO PlatformCredential (ProfileID, PlatformName, Username, AccessToken, LastSyncDate)
VALUES 
(1, 'LinkedIn', 'john.doe@example.com', 'dummy_token_for_demo', '2025-03-01'),
(2, 'Indeed', 'jane.smith@example.com', 'dummy_token_for_demo', '2025-03-02');

-- Insert sample documents
INSERT INTO Document (ProfileID, DocumentType, Version, Name, Content, ContentType, CreatedDate)
VALUES 
(1, 'Resume', 1, 'John_Doe_Resume_v1.docx', 'Sample resume content for demonstration purposes', 'application/vnd.openxmlformats-officedocument.wordprocessingml.document', '2025-02-15'),
(1, 'Resume', 2, 'John_Doe_Resume_v2.docx', 'Updated resume content with recent projects', 'application/vnd.openxmlformats-officedocument.wordprocessingml.document', '2025-03-10'),
(1, 'CoverLetter', 1, 'John_Doe_CoverLetter_Enterprise.docx', 'Cover letter for Enterprise Solutions application', 'application/vnd.openxmlformats-officedocument.wordprocessingml.document', '2025-03-05'),
(1, 'CoverLetter', 2, 'John_Doe_CoverLetter_TechInnovators.docx', 'Cover letter for Tech Innovators application', 'application/vnd.openxmlformats-officedocument.wordprocessingml.document', '2025-03-12'),
(2, 'Resume', 1, 'Jane_Smith_Resume.pdf', 'Sample resume content for demonstration purposes', 'application/pdf', '2025-02-20'),
(2, 'CoverLetter', 1, 'Jane_Smith_CoverLetter_CreativeTech.docx', 'Cover letter for Creative Tech application', 'application/vnd.openxmlformats-officedocument.wordprocessingml.document', '2025-03-07');

-- Insert sample saved searches
INSERT INTO SavedSearch (ProfileID, Name, Keywords, Location, JobType, Platforms, CreatedDate, LastRunDate)
VALUES 
(1, '.NET Developer Jobs in NY', '.NET Developer C#', 'New York, NY', 'Full-time', 'LinkedIn,Indeed,Glassdoor', '2025-02-10', '2025-03-15'),
(2, 'UX Designer Jobs in SF', 'UX Designer UI', 'San Francisco, CA', 'Full-time', 'LinkedIn,Indeed,Dribbble', '2025-02-12', '2025-03-14');

-- Insert sample AI model settings
INSERT INTO AIModelSettings (ProfileID, SettingName, SettingValue, CreatedDate, LastModifiedDate)
VALUES 
(1, 'ResumeParserModel', 'standard', '2025-02-01', '2025-02-01'),
(1, 'JobMatchThreshold', '0.75', '2025-02-01', '2025-03-01'),
(2, 'ResumeParserModel', 'creative', '2025-02-05', '2025-02-05'),
(2, 'JobMatchThreshold', '0.70', '2025-02-05', '2025-02-05');
