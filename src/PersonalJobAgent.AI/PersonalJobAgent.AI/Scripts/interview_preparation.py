"""
Interview Preparation Module for Personal Job Agent

This module provides functionality to generate interview questions and preparation
materials based on job listings and user profiles.
"""

import re
import random
from typing import Dict, Any, List, Optional, Tuple
import json

class InterviewPreparationModule:
    """
    Class for generating interview questions and preparation materials.
    """
    
    def __init__(self):
        """Initialize the interview preparation module."""
        self.question_templates = self._load_question_templates()
        self.answer_templates = self._load_answer_templates()
        
    def generate_interview_questions(self, job_listing: Dict[str, Any], count: int = 10) -> List[Dict[str, Any]]:
        """
        Generate interview questions based on job listing.
        
        Args:
            job_listing: Job listing data
            count: Number of questions to generate
            
        Returns:
            List of generated questions with suggested answers
        """
        # Extract key information from job listing
        job_title = job_listing.get("title", "")
        company = job_listing.get("company", "")
        description = job_listing.get("description", "")
        
        # Extract required skills
        required_skills = self._extract_required_skills(job_listing)
        
        # Generate different types of questions
        technical_questions = self._generate_technical_questions(job_title, description, required_skills)
        behavioral_questions = self._generate_behavioral_questions(job_title, description)
        company_questions = self._generate_company_questions(company, description)
        role_questions = self._generate_role_questions(job_title, description)
        
        # Combine all questions
        all_questions = []
        all_questions.extend(technical_questions)
        all_questions.extend(behavioral_questions)
        all_questions.extend(company_questions)
        all_questions.extend(role_questions)
        
        # Shuffle questions
        random.shuffle(all_questions)
        
        # Return requested number of questions
        return all_questions[:count]
    
    def generate_preparation_tips(self, job_listing: Dict[str, Any], user_profile: Optional[Dict[str, Any]] = None) -> Dict[str, Any]:
        """
        Generate interview preparation tips based on job listing and user profile.
        
        Args:
            job_listing: Job listing data
            user_profile: Optional user profile data
            
        Returns:
            Dictionary of preparation tips
        """
        # Extract key information
        job_title = job_listing.get("title", "")
        company = job_listing.get("company", "")
        
        # Generate preparation tips
        research_tips = self._generate_research_tips(company)
        presentation_tips = self._generate_presentation_tips()
        question_tips = self._generate_question_tips(job_title)
        
        # If user profile is provided, generate personalized tips
        strength_weakness_tips = []
        if user_profile:
            strength_weakness_tips = self._generate_strength_weakness_tips(user_profile, job_listing)
        
        # Combine all tips
        preparation_tips = {
            "research_tips": research_tips,
            "presentation_tips": presentation_tips,
            "question_tips": question_tips
        }
        
        if strength_weakness_tips:
            preparation_tips["strength_weakness_tips"] = strength_weakness_tips
        
        return preparation_tips
    
    def analyze_job_requirements(self, job_listing: Dict[str, Any]) -> Dict[str, Any]:
        """
        Analyze job requirements from job listing.
        
        Args:
            job_listing: Job listing data
            
        Returns:
            Dictionary of analyzed requirements
        """
        # Extract key information
        job_title = job_listing.get("title", "")
        description = job_listing.get("description", "")
        
        # Extract requirements
        required_skills = self._extract_required_skills(job_listing)
        required_experience = self._extract_required_experience(description)
        required_education = self._extract_required_education(description)
        
        # Extract key responsibilities
        responsibilities = self._extract_responsibilities(description)
        
        # Return analyzed requirements
        return {
            "required_skills": required_skills,
            "required_experience": required_experience,
            "required_education": required_education,
            "key_responsibilities": responsibilities
        }
    
    def _load_question_templates(self) -> Dict[str, List[str]]:
        """
        Load question templates.
        
        Returns:
            Dictionary of question templates by category
        """
        # In a real implementation, would load from files or database
        return {
            "technical": [
                "Can you explain your experience with {skill}?",
                "How would you solve a problem using {skill}?",
                "What projects have you worked on that involved {skill}?",
                "How do you stay updated with developments in {skill}?",
                "Can you describe a challenging technical problem you solved using {skill}?"
            ],
            "behavioral": [
                "Tell me about a time when you had to deal with a difficult team member.",
                "Describe a situation where you had to meet a tight deadline.",
                "Give an example of a time when you showed leadership skills.",
                "Tell me about a time when you failed and what you learned from it.",
                "Describe a situation where you had to learn something new quickly.",
                "Tell me about a time when you had to make a difficult decision.",
                "Describe a project where you had to work with limited resources.",
                "Give an example of how you handled criticism of your work.",
                "Tell me about a time when you exceeded expectations.",
                "Describe a situation where you had to resolve a conflict."
            ],
            "company": [
                "What do you know about {company}?",
                "Why do you want to work for {company}?",
                "What interests you about our products/services?",
                "How do you think you can contribute to {company}'s mission?",
                "What do you think sets {company} apart from its competitors?"
            ],
            "role": [
                "Why are you interested in this {role} position?",
                "What makes you a good fit for this {role} role?",
                "Where do you see yourself in five years if you get this {role} position?",
                "What aspects of being a {role} do you find most challenging?",
                "How does this {role} position align with your career goals?"
            ]
        }
    
    def _load_answer_templates(self) -> Dict[str, List[str]]:
        """
        Load answer templates.
        
        Returns:
            Dictionary of answer templates by category
        """
        # In a real implementation, would load from files or database
        return {
            "technical": [
                "When discussing {skill}, focus on specific projects where you've applied it. Mention the problem you were solving, the approach you took, and the results you achieved.",
                "For {skill} questions, demonstrate both theoretical knowledge and practical experience. Use the STAR method (Situation, Task, Action, Result) to structure your response.",
                "With {skill} questions, highlight your problem-solving process and how you stay current with best practices. Mention any certifications or continuous learning you've pursued."
            ],
            "behavioral": [
                "Use the STAR method (Situation, Task, Action, Result) to structure your response. Be specific about the situation, what your role was, the actions you took, and the positive outcome.",
                "Choose examples that highlight your strengths relevant to the job. Focus on what you learned and how you've grown from the experience.",
                "Be honest and reflective. Show self-awareness and a growth mindset. Emphasize how you've applied what you learned to subsequent situations."
            ],
            "company": [
                "Research {company}'s mission, values, products, services, and recent news before the interview. Show genuine interest and alignment with their culture.",
                "Connect your skills and experiences to {company}'s needs and challenges. Demonstrate how you can add value to their organization.",
                "Express enthusiasm for {company}'s industry position and future direction. Show that you understand their market and competitive landscape."
            ],
            "role": [
                "Align your skills and experiences with the key requirements of the {role} position. Use specific examples that demonstrate your qualifications.",
                "Show understanding of the challenges and responsibilities of the {role}. Discuss how your background has prepared you for these aspects.",
                "Express genuine interest in the {role} and how it fits into your career path. Show that you've thought about your future with the company."
            ]
        }
    
    def _generate_technical_questions(self, job_title: str, description: str, required_skills: List[str]) -> List[Dict[str, Any]]:
        """
        Generate technical questions based on job requirements.
        
        Args:
            job_title: Job title
            description: Job description
            required_skills: List of required skills
            
        Returns:
            List of technical questions with suggested answers
        """
        technical_questions = []
        templates = self.question_templates["technical"]
        answer_templates = self.answer_templates["technical"]
        
        # Generate questions for each required skill
        for skill in required_skills:
            # Select random template
            template = random.choice(templates)
            
            # Format question
            question = template.format(skill=skill)
            
            # Generate suggested answer
            answer_template = random.choice(answer_templates)
            answer = answer_template.format(skill=skill)
            
            # Add to questions list
            technical_questions.append({
                "question": question,
                "type": "technical",
                "skill": skill,
                "suggested_answer": answer
            })
        
        return technical_questions
    
    def _generate_behavioral_questions(self, job_title: str, description: str) -> List[Dict[str, Any]]:
        """
        Generate behavioral questions based on job requirements.
        
        Args:
            job_title: Job title
            description: Job description
            
        Returns:
            List of behavioral questions with suggested answers
        """
        behavioral_questions = []
        templates = self.question_templates["behavioral"]
        answer_templates = self.answer_templates["behavioral"]
        
        # Select random templates
        selected_templates = random.sample(templates, min(5, len(templates)))
        
        # Generate questions
        for template in selected_templates:
            # Format question
            question = template
            
            # Generate suggested answer
            answer = random.choice(answer_templates)
            
            # Add to questions list
            behavioral_questions.append({
                "question": question,
                "type": "behavioral",
                "suggested_answer": answer
            })
        
        return behavioral_questions
    
    def _generate_company_questions(self, company: str, description: str) -> List[Dict[str, Any]]:
        """
        Generate company-specific questions.
        
        Args:
            company: Company name
            description: Job description
            
        Returns:
            List of company questions with suggested answers
        """
        company_questions = []
        templates = self.question_templates["company"]
        answer_templates = self.answer_templates["company"]
        
        # Select random templates
        selected_templates = random.sample(templates, min(2, len(templates)))
        
        # Generate questions
        for template in selected_templates:
            # Format question
            question = template.format(company=company)
            
            # Generate suggested answer
            answer = random.choice(answer_templates).format(company=company)
            
            # Add to questions list
            company_questions.append({
                "question": question,
                "type": "company",
                "suggested_answer": answer
            })
        
        return company_questions
    
    def _generate_role_questions(self, job_title: str, description: str) -> List[Dict[str, Any]]:
        """
        Generate role-specific questions.
        
        Args:
            job_title: Job title
            description: Job description
            
        Returns:
            List of role questions with suggested answers
        """
        role_questions = []
        templates = self.question_templates["role"]
        answer_templates = self.answer_templates["role"]
        
        # Select random templates
        selected_templates = random.sample(templates, min(3, len(templates)))
        
        # Generate questions
        for template in selected_templates:
            # Format question
            question = template.format(role=job_title)
            
            # Generate suggested answer
            answer = random.choice(answer_templates).format(role=job_title)
            
            # Add to questions list
            role_questions.append({
                "question": question,
                "type": "role",
                "suggested_answer": answer
            })
        
        return role_questions
    
    def _extract_required_skills(self, job_listing: Dict[str, Any]) -> List[str]:
        """
        Extract required skills from job listing.
        
        Args:
            job_listing: Job listing data
            
        Returns:
            List of required skills
        """
        # If job has explicit skills list, use it
        if "skills" in job_listing and isinstance(job_listing["skills"], list):
            return job_listing["skills"]
        
        # Otherwise, extract skills from description
        description = job_listing.get("description", "")
        
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
        skills_section = None
        for indicator in skill_indicators:
            match = re.search(f"{indicator}:?(.*?)(?:\n\n|\Z)", description, re.IGNORECASE | re.DOTALL)
            if match:
                skills_section = match.group(1)
                break
        
        if not skills_section:
            # If no clear skills section, use the whole description
            skills_section = description
        
        # Extract skills using common patterns
        skills = []
        
        # Look for bullet points or list items
        bullet_items = re.findall(r"[•\-*]\s*(.*?)(?:\n|$)", skills_section)
        for item in bullet_items:
            # If item is short, it's likely a skill
            if len(item.split()) <= 5:
                skills.append(item.strip())
            else:
                # Try to extract skill phrases from longer items
                skill_phrases = re.findall(r"(?:knowledge of|experience with|proficiency in|familiar with)\s+([\w\s,/&+#]+)", item, re.IGNORECASE)
                skills.extend([phrase.strip() for phrase in skill_phrases])
        
        # Look for common programming languages and technologies
        tech_keywords = [
            "Python", "Java", "JavaScript", "C#", "C\\+\\+", "Ruby", "PHP", "Swift",
            "SQL", "HTML", "CSS", "React", "Angular", "Vue", "Node.js", "Django",
            "Flask", "Spring", "ASP.NET", "Express", "TensorFlow", "PyTorch",
            "Docker", "Kubernetes", "AWS", "Azure", "GCP", "Git", "REST", "GraphQL"
        ]
        
        for keyword in tech_keywords:
            if re.search(r"\b" + keyword + r"\b", description, re.IGNORECASE):
                skills.append(keyword)
        
        # Remove duplicates and return
        return list(set(skills))
    
    def _extract_required_experience(self, description: str) -> Dict[str, Any]:
        """
        Extract required experience from job description.
        
        Args:
            description: Job description
            
        Returns:
            Dictionary with experience requirements
        """
        # Common patterns for years of experience
        patterns = [
            r"(\d+)\+?\s*(?:years|yrs)(?:\s*of)?\s*experience",
            r"experience\s*(?:of)?\s*(\d+)\+?\s*(?:years|yrs)",
            r"(\d+)\+?\s*(?:years|yrs)(?:\s*of)?\s*work\s*experience",
            r"minimum\s*(?:of)?\s*(\d+)\s*(?:years|yrs)"
        ]
        
        years = 0
        for pattern in patterns:
            match = re.search(pattern, description, re.IGNORECASE)
            if match:
                years = int(match.group(1))
                break
        
        # Look for specific experience requirements
        specific_experience = []
        exp_patterns = [
            r"experience (?:in|with) ([\w\s,/&+#]+)",
            r"background (?:in|with) ([\w\s,/&+#]+)"
        ]
        
        for pattern in exp_patterns:
            matches = re.findall(pattern, description, re.IGNORECASE)
            for match in matches:
                if len(match.split()) <= 5:  # Limit to short phrases
                    specific_experience.append(match.strip())
        
        return {
            "years": years,
            "specific_areas": list(set(specific_experience))
        }
    
    def _extract_required_education(self, description: str) -> Dict[str, Any]:
        """
        Extract required education from job description.
        
        Args:
            description: Job description
            
        Returns:
            Dictionary with education requirements
        """
        # Common degree patterns
        degree_patterns = {
            "bachelor": [r"bachelor'?s?", r"ba", r"bs", r"b\.a", r"b\.s", r"undergraduate"],
            "master": [r"master'?s?", r"ma", r"ms", r"m\.a", r"m\.s", r"graduate"],
            "phd": [r"ph\.?d", r"doctorate", r"doctoral"],
            "associate": [r"associate'?s?", r"a\.a", r"a\.s"]
        }
        
        degree = ""
        for deg, patterns in degree_patterns.items():
            for pattern in patterns:
                if re.search(r"\b" + pattern + r"\b", description, re.IGNORECASE):
                    degree = deg
                    break
            if degree:
                break
        
        # Extract field of study
        field_patterns = [
            r"degree in ([\w\s]+)",
            r"([\w\s]+) degree",
            r"background in ([\w\s]+)",
            r"([\w\s]+) background"
        ]
        
        field = ""
        for pattern in field_patterns:
            match = re.search(pattern, description, re.IGNORECASE)
            if match:
                field = match.group(1).strip()
                break
        
        return {
            "degree": degree,
            "field": field
        }
    
    def _extract_responsibilities(self, description: str) -> List[str]:
        """
        Extract key responsibilities from job description.
        
        Args:
            description: Job description
            
        Returns:
            List of key responsibilities
        """
        # Common responsibility section indicators
        resp_indicators = [
            r"responsibilities",
            r"duties",
            r"what you'll do",
            r"job description",
            r"the role",
            r"your role"
        ]
        
        # Try to find responsibilities section
        resp_section = None
        for indicator in resp_indicators:
            match = re.search(f"{indicator}:?(.*?)(?:\n\n|\Z)", description, re.IGNORECASE | re.DOTALL)
            if match:
                resp_section = match.group(1)
                break
        
        if not resp_section:
            return []
        
        # Extract responsibilities from bullet points
        responsibilities = []
        bullet_items = re.findall(r"[•\-*]\s*(.*?)(?:\n|$)", resp_section)
        
        for item in bullet_items:
            if item.strip():
                responsibilities.append(item.strip())
        
        return responsibilities
    
    def _generate_research_tips(self, company: str) -> List[str]:
        """
        Generate company research tips.
        
        Args:
            company: Company name
            
        Returns:
            List of research tips
        """
        return [
            f"Research {company}'s mission, vision, and values from their website.",
            f"Review {company}'s recent news, press releases, and social media posts.",
            f"Understand {company}'s products, services, and target market.",
            f"Research {company}'s competitors and industry position.",
            f"Look up the backgrounds of key executives and potential interviewers on LinkedIn.",
            f"Check company reviews on sites like Glassdoor to understand the culture.",
            f"Prepare questions that show your interest in {company}'s future plans and challenges."
        ]
    
    def _generate_presentation_tips(self) -> List[str]:
        """
        Generate presentation tips for the interview.
        
        Returns:
            List of presentation tips
        """
        return [
            "Dress professionally and appropriately for the company culture.",
            "Arrive 10-15 minutes early for in-person interviews or log in 5 minutes early for virtual ones.",
            "Bring extra copies of your resume, a notepad, and pen.",
            "Maintain good posture and eye contact throughout the interview.",
            "Speak clearly and at a moderate pace, avoiding filler words like 'um' and 'like'.",
            "Use the STAR method (Situation, Task, Action, Result) to structure your answers to behavioral questions.",
            "Be prepared to give a concise 1-2 minute introduction about yourself.",
            "Practice your answers to common questions, but avoid sounding rehearsed.",
            "Show enthusiasm and positive energy throughout the interview.",
            "Send a thank-you email within 24 hours after the interview."
        ]
    
    def _generate_question_tips(self, job_title: str) -> List[str]:
        """
        Generate tips for questions to ask the interviewer.
        
        Args:
            job_title: Job title
            
        Returns:
            List of question tips
        """
        return [
            f"What does success look like in this {job_title} role?",
            "What are the biggest challenges the team is currently facing?",
            "Can you describe the team I would be working with?",
            "What is the typical career progression for someone in this role?",
            "How would you describe the company culture?",
            "What do you enjoy most about working here?",
            "What are the next steps in the interview process?",
            f"How is performance measured for the {job_title} position?",
            "What opportunities are there for professional development?",
            "What are the company's plans for growth in the next few years?"
        ]
    
    def _generate_strength_weakness_tips(self, user_profile: Dict[str, Any], job_listing: Dict[str, Any]) -> Dict[str, List[str]]:
        """
        Generate personalized strength and weakness tips based on user profile and job listing.
        
        Args:
            user_profile: User profile data
            job_listing: Job listing data
            
        Returns:
            Dictionary with strength and weakness tips
        """
        # Extract user skills
        user_skills = [skill.get("name", "") for skill in user_profile.get("skills", [])]
        user_skills = [skill for skill in user_skills if skill]
        
        # Extract required skills
        required_skills = self._extract_required_skills(job_listing)
        
        # Identify matching skills (strengths)
        strengths = []
        for skill in user_skills:
            for req_skill in required_skills:
                if skill.lower() in req_skill.lower() or req_skill.lower() in skill.lower():
                    strengths.append(skill)
                    break
        
        # Identify missing skills (potential weaknesses)
        weaknesses = []
        for req_skill in required_skills:
            is_missing = True
            for skill in user_skills:
                if skill.lower() in req_skill.lower() or req_skill.lower() in skill.lower():
                    is_missing = False
                    break
            if is_missing:
                weaknesses.append(req_skill)
        
        # Generate strength tips
        strength_tips = []
        for strength in strengths[:3]:  # Limit to top 3 strengths
            strength_tips.append(f"Highlight your experience with {strength} and provide specific examples of how you've used it successfully.")
        
        # Add general strength tips if needed
        if len(strength_tips) < 3:
            general_strengths = [
                "Emphasize your problem-solving abilities with concrete examples.",
                "Highlight your teamwork and collaboration skills.",
                "Discuss your adaptability and ability to learn quickly.",
                "Showcase your communication skills and how they've benefited previous employers.",
                "Emphasize your leadership experience, even in informal roles."
            ]
            strength_tips.extend(general_strengths[:3 - len(strength_tips)])
        
        # Generate weakness tips
        weakness_tips = []
        for weakness in weaknesses[:2]:  # Limit to top 2 weaknesses
            weakness_tips.append(f"Acknowledge limited experience with {weakness}, but emphasize your ability to learn quickly and any related skills you do have.")
        
        # Add general weakness tips
        general_weaknesses = [
            "When discussing weaknesses, focus on skills you're actively improving and the steps you're taking to develop them.",
            "Frame weaknesses as areas for growth rather than deficiencies.",
            "Choose weaknesses that aren't critical to the core job functions.",
            "Avoid cliché weaknesses like 'perfectionism' or 'working too hard'.",
            "Demonstrate self-awareness and a commitment to professional development."
        ]
        weakness_tips.extend(general_weaknesses[:3 - len(weakness_tips)])
        
        return {
            "strengths": strength_tips,
            "weaknesses": weakness_tips
        }


def generate_interview_questions(job_listing: Dict[str, Any], count: int = 10) -> List[Dict[str, Any]]:
    """
    Generate interview questions based on job listing.
    
    Args:
        job_listing: Job listing data
        count: Number of questions to generate
        
    Returns:
        List of generated questions with suggested answers
    """
    module = InterviewPreparationModule()
    return module.generate_interview_questions(job_listing, count)


def generate_preparation_tips(job_listing: Dict[str, Any], user_profile: Optional[Dict[str, Any]] = None) -> Dict[str, Any]:
    """
    Generate interview preparation tips based on job listing and user profile.
    
    Args:
        job_listing: Job listing data
        user_profile: Optional user profile data
        
    Returns:
        Dictionary of preparation tips
    """
    module = InterviewPreparationModule()
    return module.generate_preparation_tips(job_listing, user_profile)


def analyze_job_requirements(job_listing: Dict[str, Any]) -> Dict[str, Any]:
    """
    Analyze job requirements from job listing.
    
    Args:
        job_listing: Job listing data
        
    Returns:
        Dictionary of analyzed requirements
    """
    module = InterviewPreparationModule()
    return module.analyze_job_requirements(job_listing)


if __name__ == "__main__":
    # Example usage
    sample_job = {
        "title": "Senior Full Stack Developer",
        "company": "Tech Innovators",
        "location": "New York, NY",
        "description": """
        We are looking for a Senior Full Stack Developer with 5+ years of experience in Python and JavaScript frameworks.
        
        Responsibilities:
        • Design and implement scalable web applications
        • Write clean, maintainable, and efficient code
        • Collaborate with cross-functional teams
        • Mentor junior developers
        • Participate in code reviews
        
        Requirements:
        • 5+ years of experience in software development
        • Strong proficiency in Python, JavaScript, and React
        • Experience with Django or Flask
        • Knowledge of RESTful APIs and microservices
        • Familiarity with Git and CI/CD pipelines
        • Bachelor's degree in Computer Science or related field
        
        We offer competitive salary, flexible working hours, and opportunities for professional growth.
        """
    }
    
    sample_profile = {
        "personal_info": {
            "name": "John Doe",
            "email": "john.doe@example.com",
            "phone": "(555) 123-4567",
            "location": "New York, NY"
        },
        "skills": [
            {"name": "Python", "proficiency": 5},
            {"name": "JavaScript", "proficiency": 4},
            {"name": "React", "proficiency": 4},
            {"name": "SQL", "proficiency": 3},
            {"name": "Git", "proficiency": 4}
        ]
    }
    
    # Generate interview questions
    questions = generate_interview_questions(sample_job, 5)
    print("INTERVIEW QUESTIONS:")
    print(json.dumps(questions, indent=2))
    
    # Generate preparation tips
    tips = generate_preparation_tips(sample_job, sample_profile)
    print("\nPREPARATION TIPS:")
    print(json.dumps(tips, indent=2))
    
    # Analyze job requirements
    requirements = analyze_job_requirements(sample_job)
    print("\nJOB REQUIREMENTS ANALYSIS:")
    print(json.dumps(requirements, indent=2))
