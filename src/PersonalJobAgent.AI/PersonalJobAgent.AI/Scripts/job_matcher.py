"""
Job Matching Engine for Personal Job Agent

This module provides functionality to match user profiles with job listings
using NLP and machine learning techniques.
"""

import re
import numpy as np
from typing import Dict, List, Any, Optional, Tuple
import json
from sentence_transformers import SentenceTransformer

# Initialize sentence transformer model
# In production, would use a more sophisticated model
try:
    model = SentenceTransformer('all-MiniLM-L6-v2')
except Exception as e:
    print(f"Warning: Could not load sentence transformer model: {e}")
    print("Using mock embeddings for development purposes.")
    model = None


class JobMatcher:
    """
    Class for matching user profiles with job listings using NLP techniques.
    """
    
    def __init__(self):
        """Initialize the job matcher with necessary components."""
        self.model = model
        self.skill_weight = 0.5
        self.experience_weight = 0.3
        self.education_weight = 0.2
        
    def match_jobs(self, user_profile: Dict[str, Any], job_listings: List[Dict[str, Any]]) -> List[Dict[str, Any]]:
        """
        Match user profile with job listings and return ranked results.
        
        Args:
            user_profile: User profile data
            job_listings: List of job listings to match against
            
        Returns:
            List of job listings with match scores
        """
        results = []
        
        for job in job_listings:
            match_score, match_details = self._calculate_match_score(user_profile, job)
            
            # Add match information to job listing
            job_result = job.copy()
            job_result["match_score"] = match_score
            job_result["match_details"] = match_details
            
            results.append(job_result)
        
        # Sort by match score (descending)
        results.sort(key=lambda x: x["match_score"], reverse=True)
        
        return results
    
    def _calculate_match_score(self, user_profile: Dict[str, Any], job: Dict[str, Any]) -> Tuple[float, Dict[str, Any]]:
        """
        Calculate match score between user profile and job listing.
        
        Args:
            user_profile: User profile data
            job: Job listing data
            
        Returns:
            Tuple of (match_score, match_details)
        """
        match_details = {}
        
        # Calculate skill match
        skill_score, skill_matches = self._calculate_skill_match(user_profile, job)
        match_details["skill_score"] = skill_score
        match_details["skill_matches"] = skill_matches
        
        # Calculate experience match
        experience_score, experience_matches = self._calculate_experience_match(user_profile, job)
        match_details["experience_score"] = experience_score
        match_details["experience_matches"] = experience_matches
        
        # Calculate education match
        education_score, education_matches = self._calculate_education_match(user_profile, job)
        match_details["education_score"] = education_score
        match_details["education_matches"] = education_matches
        
        # Calculate semantic similarity between profile and job description
        semantic_score = self._calculate_semantic_similarity(user_profile, job)
        match_details["semantic_score"] = semantic_score
        
        # Calculate overall match score (weighted average)
        match_score = (
            self.skill_weight * skill_score +
            self.experience_weight * experience_score +
            self.education_weight * education_score
        )
        
        # Adjust score based on semantic similarity
        match_score = match_score * 0.7 + semantic_score * 0.3
        
        return match_score, match_details
    
    def _calculate_skill_match(self, user_profile: Dict[str, Any], job: Dict[str, Any]) -> Tuple[float, List[Dict[str, Any]]]:
        """
        Calculate skill match between user profile and job listing.
        
        Args:
            user_profile: User profile data
            job: Job listing data
            
        Returns:
            Tuple of (skill_score, skill_matches)
        """
        user_skills = [skill["name"].lower() for skill in user_profile.get("skills", [])]
        
        # Extract skills from job description
        job_skills = self._extract_skills_from_job(job)
        
        if not job_skills or not user_skills:
            return 0.0, []
        
        # Find matching skills
        skill_matches = []
        for job_skill in job_skills:
            job_skill_lower = job_skill.lower()
            
            # Check for exact match
            if job_skill_lower in user_skills:
                skill_matches.append({
                    "skill": job_skill,
                    "match_type": "exact",
                    "score": 1.0
                })
                continue
            
            # Check for partial match
            for user_skill in user_skills:
                if job_skill_lower in user_skill or user_skill in job_skill_lower:
                    skill_matches.append({
                        "skill": job_skill,
                        "match_type": "partial",
                        "score": 0.7
                    })
                    break
        
        # Calculate skill score
        if not job_skills:
            skill_score = 0.0
        else:
            skill_score = sum(match["score"] for match in skill_matches) / len(job_skills)
        
        return skill_score, skill_matches
    
    def _extract_skills_from_job(self, job: Dict[str, Any]) -> List[str]:
        """
        Extract skills from job description.
        
        Args:
            job: Job listing data
            
        Returns:
            List of skills mentioned in the job
        """
        # If job has explicit skills list, use it
        if "skills" in job and isinstance(job["skills"], list):
            return job["skills"]
        
        # Otherwise, extract skills from description
        description = job.get("description", "")
        
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
    
    def _calculate_experience_match(self, user_profile: Dict[str, Any], job: Dict[str, Any]) -> Tuple[float, List[Dict[str, Any]]]:
        """
        Calculate experience match between user profile and job listing.
        
        Args:
            user_profile: User profile data
            job: Job listing data
            
        Returns:
            Tuple of (experience_score, experience_matches)
        """
        user_experiences = user_profile.get("experience", [])
        job_title = job.get("title", "").lower()
        job_description = job.get("description", "").lower()
        
        if not user_experiences:
            return 0.0, []
        
        experience_matches = []
        
        # Extract years of experience required from job
        years_required = self._extract_years_required(job_description)
        
        # Calculate total years of user experience
        total_years = 0
        for exp in user_experiences:
            # Try to extract years from date range
            years = self._calculate_experience_years(exp)
            total_years += years
        
        # Check if user meets years requirement
        years_match = {
            "type": "years_of_experience",
            "job_requirement": years_required,
            "user_experience": total_years
        }
        
        if years_required > 0:
            if total_years >= years_required:
                years_match["score"] = 1.0
                years_match["match"] = True
            else:
                years_match["score"] = total_years / years_required
                years_match["match"] = False
        else:
            years_match["score"] = 1.0
            years_match["match"] = True
        
        experience_matches.append(years_match)
        
        # Check for title/role match
        title_matches = []
        for exp in user_experiences:
            user_title = exp.get("title", "").lower()
            if not user_title:
                continue
            
            # Calculate similarity between user title and job title
            if self.model:
                title_similarity = self._calculate_text_similarity(user_title, job_title)
            else:
                # Fallback if no model is available
                title_similarity = 0.5 if any(word in job_title for word in user_title.split()) else 0.0
            
            if title_similarity > 0.7:
                title_matches.append({
                    "user_title": exp.get("title"),
                    "similarity": title_similarity
                })
        
        if title_matches:
            experience_matches.append({
                "type": "title_match",
                "matches": title_matches,
                "score": max(match["similarity"] for match in title_matches)
            })
        
        # Calculate overall experience score
        if not experience_matches:
            return 0.0, []
        
        experience_score = sum(match["score"] for match in experience_matches if "score" in match) / len(experience_matches)
        
        return experience_score, experience_matches
    
    def _extract_years_required(self, job_description: str) -> int:
        """
        Extract years of experience required from job description.
        
        Args:
            job_description: Job description text
            
        Returns:
            Number of years required (0 if not specified)
        """
        # Common patterns for years of experience
        patterns = [
            r"(\d+)\+?\s*(?:years|yrs)(?:\s*of)?\s*experience",
            r"experience\s*(?:of)?\s*(\d+)\+?\s*(?:years|yrs)",
            r"(\d+)\+?\s*(?:years|yrs)(?:\s*of)?\s*work\s*experience",
            r"minimum\s*(?:of)?\s*(\d+)\s*(?:years|yrs)"
        ]
        
        for pattern in patterns:
            match = re.search(pattern, job_description, re.IGNORECASE)
            if match:
                return int(match.group(1))
        
        return 0
    
    def _calculate_experience_years(self, experience: Dict[str, Any]) -> float:
        """
        Calculate years of experience from an experience entry.
        
        Args:
            experience: Experience entry
            
        Returns:
            Years of experience (float)
        """
        # If there's a pre-calculated duration, use it
        if "duration_years" in experience:
            return float(experience["duration_years"])
        
        # Try to calculate from start and end dates
        start_date = experience.get("start_date")
        end_date = experience.get("end_date", "Present")
        
        if not start_date:
            return 0.0
        
        # Extract years from dates
        start_year = self._extract_year(start_date)
        end_year = self._extract_year(end_date) if end_date != "Present" else 2025  # Use current year for "Present"
        
        if start_year and end_year:
            return end_year - start_year
        
        return 0.0
    
    def _extract_year(self, date_str: str) -> Optional[int]:
        """
        Extract year from date string.
        
        Args:
            date_str: Date string
            
        Returns:
            Year as integer, or None if not found
        """
        year_match = re.search(r"\b(19|20)\d{2}\b", date_str)
        if year_match:
            return int(year_match.group(0))
        return None
    
    def _calculate_education_match(self, user_profile: Dict[str, Any], job: Dict[str, Any]) -> Tuple[float, List[Dict[str, Any]]]:
        """
        Calculate education match between user profile and job listing.
        
        Args:
            user_profile: User profile data
            job: Job listing data
            
        Returns:
            Tuple of (education_score, education_matches)
        """
        user_education = user_profile.get("education", [])
        job_description = job.get("description", "").lower()
        
        if not user_education:
            return 0.0, []
        
        education_matches = []
        
        # Extract education requirements from job
        degree_required = self._extract_degree_required(job_description)
        field_required = self._extract_field_required(job_description)
        
        # Check if user meets degree requirement
        user_highest_degree = self._get_highest_degree(user_education)
        degree_match = {
            "type": "degree_match",
            "job_requirement": degree_required,
            "user_degree": user_highest_degree
        }
        
        if degree_required:
            if self._is_degree_sufficient(user_highest_degree, degree_required):
                degree_match["score"] = 1.0
                degree_match["match"] = True
            else:
                degree_match["score"] = 0.0
                degree_match["match"] = False
        else:
            degree_match["score"] = 1.0
            degree_match["match"] = True
        
        education_matches.append(degree_match)
        
        # Check if user meets field requirement
        if field_required:
            field_match = {
                "type": "field_match",
                "job_requirement": field_required,
                "user_fields": []
            }
            
            field_score = 0.0
            for edu in user_education:
                user_field = edu.get("field_of_study", "").lower()
                if not user_field:
                    continue
                
                # Calculate similarity between user field and required field
                if self.model:
                    field_similarity = self._calculate_text_similarity(user_field, field_required)
                else:
                    # Fallback if no model is available
                    field_similarity = 0.5 if any(word in field_required for word in user_field.split()) else 0.0
                
                field_match["user_fields"].append({
                    "field": edu.get("field_of_study"),
                    "similarity": field_similarity
                })
                
                field_score = max(field_score, field_similarity)
            
            field_match["score"] = field_score
            field_match["match"] = field_score > 0.7
            
            education_matches.append(field_match)
        
        # Calculate overall education score
        if not education_matches:
            return 0.0, []
        
        education_score = sum(match["score"] for match in education_matches) / len(education_matches)
        
        return education_score, education_matches
    
    def _extract_degree_required(self, job_description: str) -> str:
        """
        Extract degree requirement from job description.
        
        Args:
            job_description: Job description text
            
        Returns:
            Degree requirement (empty string if not specified)
        """
        # Common degree patterns
        degree_patterns = {
            "bachelor": [r"bachelor'?s?", r"ba", r"bs", r"b\.a", r"b\.s", r"undergraduate"],
            "master": [r"master'?s?", r"ma", r"ms", r"m\.a", r"m\.s", r"graduate"],
            "phd": [r"ph\.?d", r"doctorate", r"doctoral"],
            "associate": [r"associate'?s?", r"a\.a", r"a\.s"]
        }
        
        for degree, patterns in degree_patterns.items():
            for pattern in patterns:
                if re.search(r"\b" + pattern + r"\b", job_description, re.IGNORECASE):
                    return degree
        
        return ""
    
    def _extract_field_required(self, job_description: str) -> str:
        """
        Extract field of study requirement from job description.
        
        Args:
            job_description: Job description text
            
        Returns:
            Field requirement (empty string if not specified)
        """
        # Common field patterns
        field_patterns = [
            r"degree in ([\w\s]+)",
            r"([\w\s]+) degree",
            r"background in ([\w\s]+)",
            r"([\w\s]+) background"
        ]
        
        # Common fields of study
        common_fields = [
            "computer science", "information technology", "software engineering",
            "data science", "mathematics", "statistics", "business",
            "engineering", "economics", "finance", "accounting",
            "marketing", "psychology", "biology", "chemistry", "physics"
        ]
        
        # First try to extract field from patterns
        for pattern in field_patterns:
            match = re.search(pattern, job_description, re.IGNORECASE)
            if match:
                field = match.group(1).lower()
                # Check if the extracted field contains a common field
                for common_field in common_fields:
                    if common_field in field:
                        return common_field
        
        # If no match from patterns, check for common fields directly
        for field in common_fields:
            if field in job_description:
                return field
        
        return ""
    
    def _get_highest_degree(self, education: List[Dict[str, Any]]) -> str:
        """
        Get highest degree from education entries.
        
        Args:
            education: List of education entries
            
        Returns:
            Highest degree (empty string if none found)
        """
        # Degree hierarchy
        degree_hierarchy = {
            "phd": 4,
            "master": 3,
            "bachelor": 2,
            "associate": 1
        }
        
        highest_degree = ""
        highest_level = 0
        
        for edu in education:
            degree = edu.get("degree", "").lower()
            
            # Check for degree types
            for degree_type, level in degree_hierarchy.items():
                if degree_type in degree or any(pattern in degree for pattern in self._get_degree_patterns(degree_type)):
                    if level > highest_level:
                        highest_degree = degree_type
                        highest_level = level
                    break
        
        return highest_degree
    
    def _get_degree_patterns(self, degree_type: str) -> List[str]:
        """
        Get patterns for a degree type.
        
        Args:
            degree_type: Type of degree
            
        Returns:
            List of patterns for the degree type
        """
        patterns = {
            "bachelor": ["bachelor", "ba", "bs", "b.a", "b.s", "undergraduate"],
            "master": ["master", "ma", "ms", "m.a", "m.s", "graduate"],
            "phd": ["phd", "ph.d", "doctorate", "doctoral"],
            "associate": ["associate", "a.a", "a.s"]
        }
        
        return patterns.get(degree_type, [])
    
    def _is_degree_sufficient(self, user_degree: str, required_degree: str) -> bool:
        """
        Check if user's degree is sufficient for the required degree.
        
        Args:
            user_degree: User's highest degree
            required_degree: Required degree
            
        Returns:
            True if user's degree is sufficient, False otherwise
        """
        # Degree hierarchy
        degree_hierarchy = {
            "phd": 4,
            "master": 3,
            "bachelor": 2,
            "associate": 1,
            "": 0
        }
        
        user_level = degree_hierarchy.get(user_degree, 0)
        required_level = degree_hierarchy.get(required_degree, 0)
        
        return user_level >= required_level
    
    def _calculate_semantic_similarity(self, user_profile: Dict[str, Any], job: Dict[str, Any]) -> float:
        """
        Calculate semantic similarity between user profile and job listing.
        
        Args:
            user_profile: User profile data
            job: Job listing data
            
        Returns:
            Semantic similarity score
        """
        if not self.model:
            # If model not available, return a default score
            return 0.5
        
        # Create profile text
        profile_text = ""
        
        # Add summary
        if "summary" in user_profile:
            profile_text += user_profile["summary"] + " "
        
        # Add experience descriptions
        for exp in user_profile.get("experience", []):
            if "description" in exp:
                profile_text += exp["description"] + " "
        
        # Add skills
        skill_text = " ".join(skill["name"] for skill in user_profile.get("skills", []))
        profile_text += skill_text
        
        # Create job text
        job_text = ""
        
        # Add title
        if "title" in job:
            job_text += job["title"] + " "
        
        # Add description
        if "description" in job:
            job_text += job["description"]
        
        # Calculate similarity
        try:
            profile_embedding = self.model.encode(profile_text)
            job_embedding = self.model.encode(job_text)
            
            # Cosine similarity
            similarity = np.dot(profile_embedding, job_embedding) / (
                np.linalg.norm(profile_embedding) * np.linalg.norm(job_embedding)
            )
            
            return float(similarity)
        except Exception as e:
            print(f"Error calculating semantic similarity: {e}")
            return 0.5
    
    def _calculate_text_similarity(self, text1: str, text2: str) -> float:
        """
        Calculate semantic similarity between two texts.
        
        Args:
            text1: First text
            text2: Second text
            
        Returns:
            Similarity score
        """
        if not self.model:
            # If model not available, use a simple word overlap measure
            words1 = set(text1.lower().split())
            words2 = set(text2.lower().split())
            
            if not words1 or not words2:
                return 0.0
            
            overlap = len(words1.intersection(words2))
            return overlap / max(len(words1), len(words2))
        
        try:
            embedding1 = self.model.encode(text1)
            embedding2 = self.model.encode(text2)
            
            # Cosine similarity
            similarity = np.dot(embedding1, embedding2) / (
                np.linalg.norm(embedding1) * np.linalg.norm(embedding2)
            )
            
            return float(similarity)
        except Exception as e:
            print(f"Error calculating text similarity: {e}")
            return 0.0


def match_jobs(user_profile: Dict[str, Any], job_listings: List[Dict[str, Any]]) -> List[Dict[str, Any]]:
    """
    Match user profile with job listings and return ranked results.
    
    Args:
        user_profile: User profile data
        job_listings: List of job listings to match against
        
    Returns:
        List of job listings with match scores
    """
    matcher = JobMatcher()
    return matcher.match_jobs(user_profile, job_listings)


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
                "start_date": "Jan 2020",
                "end_date": "Present",
                "description": "Developed and maintained RESTful APIs using Django and Flask. Implemented CI/CD pipelines using Jenkins and GitHub Actions."
            },
            {
                "title": "Software Developer",
                "company": "XYZ Solutions",
                "date_range": "Mar 2017 - Dec 2019",
                "start_date": "Mar 2017",
                "end_date": "Dec 2019",
                "description": "Built responsive web applications using React and Node.js. Optimized database queries resulting in 30% performance improvement."
            }
        ],
        "education": [
            {
                "institution": "University of Technology",
                "degree": "Master of Science in Computer Science",
                "date_range": "2015 - 2017",
                "gpa": "3.8"
            },
            {
                "institution": "State University",
                "degree": "Bachelor of Science in Software Engineering",
                "date_range": "2011 - 2015"
            }
        ],
        "skills": [
            {"name": "Python"},
            {"name": "JavaScript"},
            {"name": "React"},
            {"name": "Django"},
            {"name": "Flask"},
            {"name": "SQL"},
            {"name": "Git"}
        ]
    }
    
    sample_jobs = [
        {
            "title": "Senior Full Stack Developer",
            "company": "Tech Innovators",
            "location": "New York, NY",
            "description": "We are looking for a Senior Full Stack Developer with 5+ years of experience in Python and JavaScript frameworks. The ideal candidate should have experience with React, Django, and RESTful APIs. Bachelor's degree in Computer Science or related field required.",
            "skills": ["Python", "JavaScript", "React", "Django", "RESTful API", "Git"]
        },
        {
            "title": "Data Scientist",
            "company": "Data Analytics Inc.",
            "location": "Boston, MA",
            "description": "Seeking a Data Scientist with strong Python skills and experience in machine learning. The candidate should have a Master's degree in Statistics, Computer Science, or related field and 3+ years of experience with data analysis and visualization.",
            "skills": ["Python", "Machine Learning", "Statistics", "Data Visualization", "SQL"]
        }
    ]
    
    results = match_jobs(sample_profile, sample_jobs)
    print(json.dumps(results, indent=2))
