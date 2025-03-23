"""
Resume Parser Module for Personal Job Agent

This module provides functionality to parse and extract structured information from resumes.
It uses spaCy for NLP processing and custom logic for information extraction.
"""

import re
import spacy
from typing import Dict, List, Any, Optional
import json

# Load spaCy model - in production would use a larger model
try:
    nlp = spacy.load("en_core_web_sm")
except OSError:
    # If model not available, use blank model with basic components
    nlp = spacy.blank("en")
    print("Warning: Using blank spaCy model. For production, install en_core_web_sm.")


class ResumeParser:
    """
    Class for parsing resume text and extracting structured information.
    """
    
    def __init__(self):
        """Initialize the resume parser with necessary components."""
        self.sections = {
            "personal_info": ["personal information", "contact", "profile"],
            "summary": ["summary", "professional summary", "profile summary", "about me"],
            "experience": ["experience", "work experience", "employment history", "work history"],
            "education": ["education", "academic background", "educational background"],
            "skills": ["skills", "technical skills", "core competencies", "competencies"],
            "certifications": ["certifications", "certificates", "professional certifications"],
            "projects": ["projects", "key projects", "professional projects"],
            "languages": ["languages", "language proficiency"],
            "interests": ["interests", "hobbies", "activities"]
        }
        
        # Regex patterns for common information
        self.email_pattern = r'\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b'
        self.phone_pattern = r'(\+\d{1,3}[-.\s]?)?(\(?\d{3}\)?[-.\s]?)?\d{3}[-.\s]?\d{4}'
        self.url_pattern = r'(https?://)?([www]\.)?([-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b)([-a-zA-Z0-9()@:%_\+.~#?&//=]*)'
        
    def parse_resume(self, text: str) -> Dict[str, Any]:
        """
        Parse resume text and extract structured information.
        
        Args:
            text: The resume text to parse
            
        Returns:
            Dictionary containing structured resume information
        """
        # Process the text with spaCy
        doc = nlp(text)
        
        # Initialize result dictionary
        result = {
            "personal_info": {},
            "summary": "",
            "experience": [],
            "education": [],
            "skills": [],
            "certifications": [],
            "projects": [],
            "languages": [],
            "interests": []
        }
        
        # Extract sections from the resume
        sections = self._identify_sections(text)
        
        # Extract personal information
        result["personal_info"] = self._extract_personal_info(text)
        
        # Extract summary
        if "summary" in sections:
            result["summary"] = sections["summary"]
        
        # Extract experience
        if "experience" in sections:
            result["experience"] = self._extract_experience(sections["experience"])
        
        # Extract education
        if "education" in sections:
            result["education"] = self._extract_education(sections["education"])
        
        # Extract skills
        if "skills" in sections:
            result["skills"] = self._extract_skills(sections["skills"])
        
        # Extract other sections
        for section_name in ["certifications", "projects", "languages", "interests"]:
            if section_name in sections:
                result[section_name] = self._extract_list_items(sections[section_name])
        
        return result
    
    def _identify_sections(self, text: str) -> Dict[str, str]:
        """
        Identify different sections in the resume.
        
        Args:
            text: The resume text
            
        Returns:
            Dictionary mapping section names to their content
        """
        # Split text into lines
        lines = text.split('\n')
        
        # Initialize variables
        current_section = None
        sections = {}
        section_content = []
        
        # Process each line
        for line in lines:
            line = line.strip()
            if not line:
                continue
            
            # Check if this line is a section header
            found_section = False
            for section_key, section_headers in self.sections.items():
                for header in section_headers:
                    # Case-insensitive match for section headers
                    if re.search(r'\b' + re.escape(header) + r'\b', line.lower()):
                        # If we were already in a section, save its content
                        if current_section:
                            sections[current_section] = '\n'.join(section_content)
                        
                        # Start new section
                        current_section = section_key
                        section_content = []
                        found_section = True
                        break
                
                if found_section:
                    break
            
            # If not a section header, add to current section content
            if not found_section and current_section:
                section_content.append(line)
        
        # Save the last section
        if current_section and section_content:
            sections[current_section] = '\n'.join(section_content)
        
        return sections
    
    def _extract_personal_info(self, text: str) -> Dict[str, str]:
        """
        Extract personal information from the resume.
        
        Args:
            text: The resume text
            
        Returns:
            Dictionary containing personal information
        """
        personal_info = {}
        
        # Extract name (assuming it's at the beginning of the resume)
        lines = text.split('\n')
        for line in lines[:5]:  # Check first 5 lines
            line = line.strip()
            if line and len(line) < 50:  # Name is typically short
                # Check if line doesn't match common headers or contact info
                if not re.search(self.email_pattern, line) and \
                   not re.search(self.phone_pattern, line) and \
                   not re.search(self.url_pattern, line) and \
                   not any(header in line.lower() for header in sum(self.sections.values(), [])):
                    personal_info["name"] = line
                    break
        
        # Extract email
        email_match = re.search(self.email_pattern, text)
        if email_match:
            personal_info["email"] = email_match.group(0)
        
        # Extract phone
        phone_match = re.search(self.phone_pattern, text)
        if phone_match:
            personal_info["phone"] = phone_match.group(0)
        
        # Extract LinkedIn or other URLs
        url_match = re.search(self.url_pattern, text)
        if url_match:
            personal_info["url"] = url_match.group(0)
        
        # Extract location (this is more complex and would need refinement)
        # For now, we'll use a simple heuristic to look for location patterns
        location_patterns = [
            r'\b[A-Z][a-z]+,\s*[A-Z]{2}\b',  # City, State
            r'\b[A-Z][a-z]+,\s*[A-Z][a-z]+\b'  # City, Country
        ]
        
        for pattern in location_patterns:
            location_match = re.search(pattern, text)
            if location_match:
                personal_info["location"] = location_match.group(0)
                break
        
        return personal_info
    
    def _extract_experience(self, experience_text: str) -> List[Dict[str, str]]:
        """
        Extract work experience information.
        
        Args:
            experience_text: Text from the experience section
            
        Returns:
            List of dictionaries containing experience information
        """
        experiences = []
        
        # Split by potential job entries (this is a simplified approach)
        # In a real implementation, would use more sophisticated pattern matching
        job_entries = re.split(r'\n(?=[A-Z])', experience_text)
        
        for entry in job_entries:
            if not entry.strip():
                continue
                
            experience = {}
            
            # Extract company and title
            lines = entry.split('\n')
            if lines:
                # First line often contains title and company
                first_line = lines[0].strip()
                
                # Try to extract title and company
                title_company_match = re.search(r'(.*?)\s*(?:at|@|,)\s*(.*)', first_line)
                if title_company_match:
                    experience["title"] = title_company_match.group(1).strip()
                    experience["company"] = title_company_match.group(2).strip()
                else:
                    # If no clear separator, assume it's the company
                    experience["company"] = first_line
            
            # Extract dates
            date_pattern = r'(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)[a-z]*\.?\s*\d{4}\s*(?:-|–|to)\s*(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)[a-z]*\.?\s*\d{4}|(\d{4})\s*(?:-|–|to)\s*(\d{4}|Present|Current)'
            date_match = re.search(date_pattern, entry)
            if date_match:
                date_str = date_match.group(0)
                experience["date_range"] = date_str
                
                # Try to extract start and end dates
                if date_match.group(1) and date_match.group(2):  # Month Year - Month Year format
                    experience["start_date"] = f"{date_match.group(1)} {date_match.group(2)}"
                    experience["end_date"] = f"{date_match.group(3)} {date_match.group(4)}"
                elif date_match.group(3) and date_match.group(4):  # Year - Year format
                    experience["start_date"] = date_match.group(3)
                    experience["end_date"] = date_match.group(4)
            
            # Extract description
            # Remove the first line (title/company) and date line if found
            description_lines = lines[1:] if len(lines) > 1 else []
            if date_match and description_lines:
                # Remove the line containing the date
                date_line_index = next((i for i, line in enumerate(description_lines) 
                                      if date_match.group(0) in line), None)
                if date_line_index is not None:
                    description_lines.pop(date_line_index)
            
            if description_lines:
                experience["description"] = '\n'.join(description_lines).strip()
            
            experiences.append(experience)
        
        return experiences
    
    def _extract_education(self, education_text: str) -> List[Dict[str, str]]:
        """
        Extract education information.
        
        Args:
            education_text: Text from the education section
            
        Returns:
            List of dictionaries containing education information
        """
        education_entries = []
        
        # Split by potential education entries
        entries = re.split(r'\n(?=[A-Z])', education_text)
        
        for entry in entries:
            if not entry.strip():
                continue
                
            education = {}
            
            # Extract institution
            lines = entry.split('\n')
            if lines:
                education["institution"] = lines[0].strip()
            
            # Extract degree
            degree_patterns = [
                r'Bachelor[\'s]* of [A-Za-z\s]+',
                r'Master[\'s]* of [A-Za-z\s]+',
                r'Doctor of [A-Za-z\s]+',
                r'Ph\.?D\.?',
                r'B\.?S\.?',
                r'M\.?S\.?',
                r'B\.?A\.?',
                r'M\.?A\.?',
                r'M\.?B\.?A\.?',
                r'Associate[\'s]* Degree'
            ]
            
            for pattern in degree_patterns:
                degree_match = re.search(pattern, entry)
                if degree_match:
                    education["degree"] = degree_match.group(0)
                    break
            
            # Extract field of study
            field_pattern = r'in\s+([A-Za-z\s]+)'
            field_match = re.search(field_pattern, entry)
            if field_match:
                education["field_of_study"] = field_match.group(1).strip()
            
            # Extract dates
            date_pattern = r'(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)[a-z]*\.?\s*\d{4}\s*(?:-|–|to)\s*(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)[a-z]*\.?\s*\d{4}|(\d{4})\s*(?:-|–|to)\s*(\d{4}|Present|Current)'
            date_match = re.search(date_pattern, entry)
            if date_match:
                education["date_range"] = date_match.group(0)
            
            # Extract GPA
            gpa_pattern = r'GPA:?\s*(\d+\.\d+)'
            gpa_match = re.search(gpa_pattern, entry)
            if gpa_match:
                education["gpa"] = gpa_match.group(1)
            
            education_entries.append(education)
        
        return education_entries
    
    def _extract_skills(self, skills_text: str) -> List[Dict[str, Any]]:
        """
        Extract skills information.
        
        Args:
            skills_text: Text from the skills section
            
        Returns:
            List of dictionaries containing skill information
        """
        skills = []
        
        # Split by lines, commas, or semicolons
        skill_items = re.split(r'[,;\n]', skills_text)
        
        for item in skill_items:
            item = item.strip()
            if not item:
                continue
            
            # Check for skill with proficiency level
            proficiency_match = re.search(r'(.*?)\s*\(([^)]+)\)', item)
            if proficiency_match:
                skill_name = proficiency_match.group(1).strip()
                proficiency = proficiency_match.group(2).strip()
                skills.append({
                    "name": skill_name,
                    "proficiency": proficiency
                })
            else:
                skills.append({"name": item})
        
        return skills
    
    def _extract_list_items(self, text: str) -> List[str]:
        """
        Extract list items from text.
        
        Args:
            text: Text containing list items
            
        Returns:
            List of extracted items
        """
        # Split by lines, bullets, commas, or semicolons
        items = re.split(r'[,;\n•]', text)
        
        # Clean and filter items
        return [item.strip() for item in items if item.strip()]


def parse_resume(resume_text: str) -> Dict[str, Any]:
    """
    Parse a resume and return structured information.
    
    Args:
        resume_text: The text content of the resume
        
    Returns:
        Dictionary containing structured resume information
    """
    parser = ResumeParser()
    return parser.parse_resume(resume_text)


if __name__ == "__main__":
    # Example usage
    sample_resume = """
    John Doe
    john.doe@example.com | (555) 123-4567 | linkedin.com/in/johndoe
    New York, NY
    
    SUMMARY
    Experienced software engineer with 5+ years of experience in full-stack development.
    Proficient in Python, JavaScript, and cloud technologies.
    
    EXPERIENCE
    Senior Software Engineer at ABC Tech
    Jan 2020 - Present
    - Developed and maintained RESTful APIs using Django and Flask
    - Implemented CI/CD pipelines using Jenkins and GitHub Actions
    - Led a team of 3 junior developers on a customer-facing project
    
    Software Developer at XYZ Solutions
    Mar 2017 - Dec 2019
    - Built responsive web applications using React and Node.js
    - Optimized database queries resulting in 30% performance improvement
    - Collaborated with UX designers to implement user-friendly interfaces
    
    EDUCATION
    University of Technology
    Master of Science in Computer Science
    2015 - 2017
    GPA: 3.8
    
    State University
    Bachelor of Science in Software Engineering
    2011 - 2015
    
    SKILLS
    Programming Languages: Python, JavaScript, TypeScript, Java
    Frameworks: React, Angular, Django, Flask, Express
    Databases: PostgreSQL, MongoDB, Redis
    Tools: Git, Docker, Kubernetes, AWS, Azure
    
    CERTIFICATIONS
    AWS Certified Solutions Architect
    Certified Scrum Master
    Google Cloud Professional Developer
    
    LANGUAGES
    English (Native), Spanish (Intermediate), French (Basic)
    """
    
    result = parse_resume(sample_resume)
    print(json.dumps(result, indent=2))
