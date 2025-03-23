"""
Cover Letter Generator for Personal Job Agent

This module provides functionality to generate personalized cover letters
based on user profiles and job listings using NLP techniques.
"""

import re
import random
from typing import Dict, Any, List, Optional
import datetime

class CoverLetterGenerator:
    """
    Class for generating personalized cover letters based on user profiles and job listings.
    """
    
    def __init__(self):
        """Initialize the cover letter generator."""
        self.templates = self._load_templates()
        
    def generate_cover_letter(self, user_profile: Dict[str, Any], job_listing: Dict[str, Any]) -> str:
        """
        Generate a personalized cover letter based on user profile and job listing.
        
        Args:
            user_profile: User profile data
            job_listing: Job listing data
            
        Returns:
            Generated cover letter text
        """
        # Extract key information
        user_name = self._get_user_name(user_profile)
        company_name = self._get_company_name(job_listing)
        job_title = self._get_job_title(job_listing)
        hiring_manager = self._get_hiring_manager(job_listing)
        
        # Select template
        template = self._select_template(user_profile, job_listing)
        
        # Extract key skills and experiences to highlight
        skills_to_highlight = self._extract_matching_skills(user_profile, job_listing)
        experiences_to_highlight = self._extract_relevant_experiences(user_profile, job_listing)
        
        # Generate paragraphs
        introduction = self._generate_introduction(user_name, company_name, job_title, hiring_manager)
        skills_paragraph = self._generate_skills_paragraph(skills_to_highlight)
        experience_paragraph = self._generate_experience_paragraph(experiences_to_highlight, company_name, job_title)
        closing = self._generate_closing(company_name)
        
        # Assemble cover letter
        cover_letter = template.format(
            date=self._get_current_date(),
            hiring_manager=hiring_manager,
            company_name=company_name,
            introduction=introduction,
            skills_paragraph=skills_paragraph,
            experience_paragraph=experience_paragraph,
            closing=closing,
            user_name=user_name
        )
        
        return cover_letter
    
    def _load_templates(self) -> List[str]:
        """
        Load cover letter templates.
        
        Returns:
            List of template strings
        """
        # In a real implementation, would load from files or database
        templates = [
            # Template 1: Standard format
            """{date}

{hiring_manager}
{company_name}

Dear {hiring_manager},

{introduction}

{skills_paragraph}

{experience_paragraph}

{closing}

Sincerely,

{user_name}""",
            
            # Template 2: Modern format
            """{date}

{hiring_manager}
{company_name}

Dear {hiring_manager},

{introduction}

{experience_paragraph}

{skills_paragraph}

{closing}

Best regards,

{user_name}"""
        ]
        
        return templates
    
    def _select_template(self, user_profile: Dict[str, Any], job_listing: Dict[str, Any]) -> str:
        """
        Select an appropriate template based on user profile and job listing.
        
        Args:
            user_profile: User profile data
            job_listing: Job listing data
            
        Returns:
            Selected template string
        """
        # In a real implementation, would use more sophisticated selection logic
        # For now, just randomly select a template
        return random.choice(self.templates)
    
    def _get_user_name(self, user_profile: Dict[str, Any]) -> str:
        """
        Get user's full name from profile.
        
        Args:
            user_profile: User profile data
            
        Returns:
            User's full name
        """
        personal_info = user_profile.get("personal_info", {})
        name = personal_info.get("name", "")
        
        if not name:
            first_name = personal_info.get("first_name", "")
            last_name = personal_info.get("last_name", "")
            name = f"{first_name} {last_name}".strip()
        
        return name or "Applicant Name"
    
    def _get_company_name(self, job_listing: Dict[str, Any]) -> str:
        """
        Get company name from job listing.
        
        Args:
            job_listing: Job listing data
            
        Returns:
            Company name
        """
        return job_listing.get("company", "the Company")
    
    def _get_job_title(self, job_listing: Dict[str, Any]) -> str:
        """
        Get job title from job listing.
        
        Args:
            job_listing: Job listing data
            
        Returns:
            Job title
        """
        return job_listing.get("title", "the position")
    
    def _get_hiring_manager(self, job_listing: Dict[str, Any]) -> str:
        """
        Get hiring manager name from job listing.
        
        Args:
            job_listing: Job listing data
            
        Returns:
            Hiring manager name or default greeting
        """
        hiring_manager = job_listing.get("hiring_manager", "")
        
        if hiring_manager:
            return hiring_manager
        
        return "Hiring Manager"
    
    def _get_current_date(self) -> str:
        """
        Get current date formatted for cover letter.
        
        Returns:
            Formatted date string
        """
        return datetime.datetime.now().strftime("%B %d, %Y")
    
    def _extract_matching_skills(self, user_profile: Dict[str, Any], job_listing: Dict[str, Any]) -> List[Dict[str, Any]]:
        """
        Extract skills from user profile that match job requirements.
        
        Args:
            user_profile: User profile data
            job_listing: Job listing data
            
        Returns:
            List of matching skills
        """
        user_skills = user_profile.get("skills", [])
        job_description = job_listing.get("description", "").lower()
        
        # Extract required skills from job description
        required_skills = []
        if "skills" in job_listing and isinstance(job_listing["skills"], list):
            required_skills = [skill.lower() for skill in job_listing["skills"]]
        else:
            # Try to extract skills from description
            skill_section = self._extract_skills_section(job_description)
            if skill_section:
                # Look for bullet points
                skills = re.findall(r"[•\-*]\s*([\w\s,/&+#]+)", skill_section)
                required_skills = [skill.strip().lower() for skill in skills]
        
        # Find matching skills
        matching_skills = []
        for skill in user_skills:
            skill_name = skill.get("name", "").lower()
            if not skill_name:
                continue
                
            # Check if skill matches any required skill
            for required_skill in required_skills:
                if skill_name in required_skill or required_skill in skill_name:
                    matching_skills.append(skill)
                    break
        
        # If we don't have enough matching skills, add some of the user's top skills
        if len(matching_skills) < 3 and len(user_skills) > 0:
            # Sort by proficiency if available
            sorted_skills = sorted(
                user_skills, 
                key=lambda s: s.get("proficiency", 0) + s.get("years_experience", 0),
                reverse=True
            )
            
            # Add top skills until we have at least 3 or run out
            for skill in sorted_skills:
                if skill not in matching_skills:
                    matching_skills.append(skill)
                    if len(matching_skills) >= 3:
                        break
        
        return matching_skills
    
    def _extract_skills_section(self, job_description: str) -> Optional[str]:
        """
        Extract the skills section from a job description.
        
        Args:
            job_description: Job description text
            
        Returns:
            Skills section text or None if not found
        """
        # Common skill section indicators
        skill_indicators = [
            r"skills required",
            r"required skills",
            r"technical skills",
            r"qualifications",
            r"requirements",
            r"you have",
            r"you should have",
            r"what you'll need",
            r"what we're looking for"
        ]
        
        # Try to find skills section
        for indicator in skill_indicators:
            match = re.search(f"{indicator}:?(.*?)(?:\n\n|\Z)", job_description, re.IGNORECASE | re.DOTALL)
            if match:
                return match.group(1)
        
        return None
    
    def _extract_relevant_experiences(self, user_profile: Dict[str, Any], job_listing: Dict[str, Any]) -> List[Dict[str, Any]]:
        """
        Extract experiences from user profile that are relevant to the job.
        
        Args:
            user_profile: User profile data
            job_listing: Job listing data
            
        Returns:
            List of relevant experiences
        """
        experiences = user_profile.get("experience", [])
        job_title = job_listing.get("title", "").lower()
        job_description = job_listing.get("description", "").lower()
        
        # Extract key terms from job title and description
        key_terms = set()
        
        # Add words from job title
        key_terms.update(job_title.split())
        
        # Add key terms from description
        key_terms.update(self._extract_key_terms(job_description))
        
        # Score experiences based on relevance
        scored_experiences = []
        for exp in experiences:
            score = 0
            exp_title = exp.get("title", "").lower()
            exp_description = exp.get("description", "").lower()
            
            # Check title relevance
            for term in key_terms:
                if term in exp_title:
                    score += 2
            
            # Check description relevance
            for term in key_terms:
                if term in exp_description:
                    score += 1
            
            # Add to scored experiences
            scored_experiences.append((score, exp))
        
        # Sort by score (descending) and take top 2
        scored_experiences.sort(reverse=True, key=lambda x: x[0])
        relevant_experiences = [exp for _, exp in scored_experiences[:2]]
        
        return relevant_experiences
    
    def _extract_key_terms(self, text: str) -> List[str]:
        """
        Extract key terms from text.
        
        Args:
            text: Input text
            
        Returns:
            List of key terms
        """
        # In a real implementation, would use more sophisticated NLP techniques
        # For now, just extract common job-related terms
        
        # Common job-related terms
        job_terms = [
            "develop", "design", "implement", "manage", "lead", "create",
            "analyze", "research", "coordinate", "organize", "plan",
            "software", "application", "system", "database", "network",
            "project", "team", "client", "customer", "user",
            "experience", "skill", "knowledge", "ability", "proficiency"
        ]
        
        # Extract terms that appear in the text
        key_terms = []
        for term in job_terms:
            if term in text:
                key_terms.append(term)
        
        return key_terms
    
    def _generate_introduction(self, user_name: str, company_name: str, job_title: str, hiring_manager: str) -> str:
        """
        Generate introduction paragraph.
        
        Args:
            user_name: User's name
            company_name: Company name
            job_title: Job title
            hiring_manager: Hiring manager name
            
        Returns:
            Introduction paragraph
        """
        # Introduction templates
        templates = [
            "I am writing to express my interest in the {job_title} position at {company_name}. With my background and experience, I believe I would be a valuable addition to your team.",
            
            "I was excited to see your posting for the {job_title} role at {company_name}. After reviewing the job description, I am confident that my skills and experiences align well with your requirements.",
            
            "I am enthusiastic about the opportunity to apply for the {job_title} position at {company_name}. My professional background and skill set make me an ideal candidate for this role."
        ]
        
        # Select random template
        template = random.choice(templates)
        
        # Format template
        introduction = template.format(
            job_title=job_title,
            company_name=company_name
        )
        
        return introduction
    
    def _generate_skills_paragraph(self, skills: List[Dict[str, Any]]) -> str:
        """
        Generate skills paragraph.
        
        Args:
            skills: List of skills to highlight
            
        Returns:
            Skills paragraph
        """
        if not skills:
            return "I have a diverse skill set that would be valuable in this role, including strong communication, problem-solving, and analytical abilities."
        
        # Extract skill names
        skill_names = [skill.get("name", "") for skill in skills]
        skill_names = [name for name in skill_names if name]
        
        # Skills paragraph templates
        templates = [
            "My expertise includes {skill_list}, which I believe are essential for success in this role. I am constantly expanding my knowledge and staying current with industry developments.",
            
            "I bring a strong set of technical skills to this role, including {skill_list}. These skills have allowed me to successfully deliver projects and solve complex problems throughout my career.",
            
            "Throughout my career, I have developed proficiency in {skill_list}. I am confident these skills would enable me to make significant contributions to your team."
        ]
        
        # Format skill list
        if len(skill_names) == 1:
            skill_list = skill_names[0]
        elif len(skill_names) == 2:
            skill_list = f"{skill_names[0]} and {skill_names[1]}"
        else:
            skill_list = ", ".join(skill_names[:-1]) + f", and {skill_names[-1]}"
        
        # Select random template
        template = random.choice(templates)
        
        # Format template
        skills_paragraph = template.format(skill_list=skill_list)
        
        return skills_paragraph
    
    def _generate_experience_paragraph(self, experiences: List[Dict[str, Any]], company_name: str, job_title: str) -> str:
        """
        Generate experience paragraph.
        
        Args:
            experiences: List of experiences to highlight
            company_name: Company name
            job_title: Job title
            
        Returns:
            Experience paragraph
        """
        if not experiences:
            return f"My professional experience has prepared me well for the {job_title} role at {company_name}. I have consistently demonstrated the ability to deliver results, work effectively in team environments, and adapt to new challenges."
        
        # Extract company and title from most relevant experience
        most_relevant = experiences[0]
        exp_company = most_relevant.get("company", "")
        exp_title = most_relevant.get("title", "")
        
        # Experience paragraph templates
        templates = [
            "In my role as {exp_title} at {exp_company}, I gained valuable experience that directly relates to the {job_title} position. {achievement} I am excited about the opportunity to bring these skills and experiences to {company_name}.",
            
            "My experience as {exp_title} at {exp_company} has prepared me well for this role. {achievement} I believe these experiences have positioned me to make immediate contributions at {company_name}.",
            
            "While working as {exp_title} at {exp_company}, I developed skills that align perfectly with the {job_title} role. {achievement} I am confident that my background would be an asset to your team at {company_name}."
        ]
        
        # Generate achievement statement
        achievement = self._generate_achievement_statement(most_relevant)
        
        # Select random template
        template = random.choice(templates)
        
        # Format template
        experience_paragraph = template.format(
            exp_title=exp_title,
            exp_company=exp_company,
            job_title=job_title,
            company_name=company_name,
            achievement=achievement
        )
        
        return experience_paragraph
    
    def _generate_achievement_statement(self, experience: Dict[str, Any]) -> str:
        """
        Generate achievement statement from experience.
        
        Args:
            experience: Experience data
            
        Returns:
            Achievement statement
        """
        description = experience.get("description", "")
        
        # If no description, use generic achievement
        if not description:
            generic_achievements = [
                "I successfully managed multiple projects simultaneously while meeting all deadlines and quality standards.",
                "I collaborated effectively with cross-functional teams to deliver successful outcomes.",
                "I implemented process improvements that increased efficiency and productivity."
            ]
            return random.choice(generic_achievements)
        
        # Try to extract achievement from description
        # Look for sentences with metrics or accomplishments
        achievement_indicators = [
            r"increased", r"decreased", r"improved", r"reduced", r"achieved",
            r"developed", r"implemented", r"created", r"launched", r"led",
            r"managed", r"coordinated", r"designed", r"\d+%", r"\$\d+"
        ]
        
        # Split description into sentences
        sentences = re.split(r'(?<=[.!?])\s+', description)
        
        # Look for sentences with achievement indicators
        achievement_sentences = []
        for sentence in sentences:
            for indicator in achievement_indicators:
                if re.search(indicator, sentence, re.IGNORECASE):
                    achievement_sentences.append(sentence)
                    break
        
        # If found, use the first achievement sentence
        if achievement_sentences:
            return achievement_sentences[0]
        
        # If no specific achievement found, use the first sentence of the description
        if sentences:
            return sentences[0]
        
        # Fallback to generic achievement
        return "I consistently delivered high-quality results and contributed to the team's success."
    
    def _generate_closing(self, company_name: str) -> str:
        """
        Generate closing paragraph.
        
        Args:
            company_name: Company name
            
        Returns:
            Closing paragraph
        """
        # Closing templates
        templates = [
            "I am excited about the opportunity to join {company_name} and would welcome the chance to discuss how my background and skills would be a good match for this position. Thank you for your time and consideration.",
            
            "I am eager to bring my skills and experiences to {company_name} and would appreciate the opportunity to discuss my application with you further. Thank you for considering my application.",
            
            "I am enthusiastic about the possibility of joining the team at {company_name} and contributing to your continued success. I look forward to the opportunity to discuss my qualifications in more detail. Thank you for your consideration."
        ]
        
        # Select random template
        template = random.choice(templates)
        
        # Format template
        closing = template.format(company_name=company_name)
        
        return closing


def generate_cover_letter(user_profile: Dict[str, Any], job_listing: Dict[str, Any]) -> str:
    """
    Generate a personalized cover letter based on user profile and job listing.
    
    Args:
        user_profile: User profile data
        job_listing: Job listing data
        
    Returns:
        Generated cover letter text
    """
    generator = CoverLetterGenerator()
    return generator.generate_cover_letter(user_profile, job_listing)


if __name__ == "__main__":
    # Example usage
    sample_profile = {
        "personal_info": {
            "name": "John Doe",
            "email": "john.doe@example.com",
            "phone": "(555) 123-4567",
            "location": "New York, NY"
        },
        "summary": "Experienced software engineer with 5+ years of experience in full-stack development.",
        "experience": [
            {
                "title": "Senior Software Engineer",
                "company": "ABC Tech",
                "date_range": "Jan 2020 - Present",
                "description": "Led development of RESTful APIs using Django and Flask. Implemented CI/CD pipelines using Jenkins and GitHub Actions. Reduced API response time by 40% through optimization."
            },
            {
                "title": "Software Developer",
                "company": "XYZ Solutions",
                "date_range": "Mar 2017 - Dec 2019",
                "description": "Built responsive web applications using React and Node.js. Optimized database queries resulting in 30% performance improvement."
            }
        ],
        "skills": [
            {"name": "Python", "proficiency": 5},
            {"name": "JavaScript", "proficiency": 4},
            {"name": "React", "proficiency": 4},
            {"name": "Django", "proficiency": 5},
            {"name": "Flask", "proficiency": 4},
            {"name": "SQL", "proficiency": 3},
            {"name": "Git", "proficiency": 4}
        ]
    }
    
    sample_job = {
        "title": "Senior Full Stack Developer",
        "company": "Tech Innovators",
        "location": "New York, NY",
        "description": "We are looking for a Senior Full Stack Developer with 5+ years of experience in Python and JavaScript frameworks. The ideal candidate should have experience with React, Django, and RESTful APIs. Bachelor's degree in Computer Science or related field required.",
        "skills": ["Python", "JavaScript", "React", "Django", "RESTful API", "Git"]
    }
    
    cover_letter = generate_cover_letter(sample_profile, sample_job)
    print(cover_letter)
