using System;
using System.Collections.Generic;

namespace PersonalJobAgent.Core.Models
{
    /// <summary>
    /// Represents a skill in the Personal Job Agent application.
    /// </summary>
    public class Skill
    {
        /// <summary>
        /// Gets or sets the unique identifier for the skill.
        /// </summary>
        public int SkillId { get; set; }

        /// <summary>
        /// Gets or sets the profile ID this skill belongs to.
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Gets or sets the name of the skill.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category of the skill.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the proficiency level (1-5 scale).
        /// </summary>
        public int Proficiency { get; set; }

        /// <summary>
        /// Gets or sets the years of experience with this skill.
        /// </summary>
        public decimal YearsExperience { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this skill should be highlighted.
        /// </summary>
        public bool IsHighlighted { get; set; }

        /// <summary>
        /// Gets or sets the user profile this skill belongs to.
        /// </summary>
        public UserProfile UserProfile { get; set; }
    }
}
