using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalJobAgent.Core.Models;

namespace PersonalJobAgent.Data
{
    /// <summary>
    /// Database context for the Personal Job Agent application
    /// </summary>
    public class PersonalJobAgentDbContext : DbContext
    {
        public PersonalJobAgentDbContext(DbContextOptions<PersonalJobAgentDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<JobListing> JobListings { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<PlatformCredential> PlatformCredentials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // UserProfile configuration
            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.HasIndex(e => e.Email).IsUnique();
                
                // One-to-many relationship with Skills
                entity.HasMany(e => e.Skills)
                    .WithOne()
                    .HasForeignKey(e => e.UserProfileId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                // One-to-many relationship with WorkExperiences
                entity.HasMany(e => e.WorkExperiences)
                    .WithOne()
                    .HasForeignKey(e => e.UserProfileId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                // One-to-many relationship with Education
                entity.HasMany(e => e.Education)
                    .WithOne()
                    .HasForeignKey(e => e.UserProfileId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Skill configuration
            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Level).HasMaxLength(50);
                entity.Property(e => e.Category).HasMaxLength(100);
            });

            // WorkExperience configuration
            modelBuilder.Entity<WorkExperience>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Company).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Location).HasMaxLength(255);
            });

            // Education configuration
            modelBuilder.Entity<Education>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Institution).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Degree).IsRequired().HasMaxLength(255);
                entity.Property(e => e.FieldOfStudy).HasMaxLength(255);
            });

            // JobListing configuration
            modelBuilder.Entity<JobListing>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Company).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Location).HasMaxLength(255);
                entity.Property(e => e.Url).HasMaxLength(2048);
            });

            // Application configuration
            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
                
                // Many-to-one relationship with UserProfile
                entity.HasOne<UserProfile>()
                    .WithMany()
                    .HasForeignKey(e => e.UserProfileId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                // Many-to-one relationship with JobListing
                entity.HasOne(e => e.JobListing)
                    .WithMany()
                    .HasForeignKey(e => e.JobListingId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // PlatformCredential configuration
            modelBuilder.Entity<PlatformCredential>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PlatformName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Password).HasMaxLength(1024);
                entity.Property(e => e.ApiKey).HasMaxLength(1024);
                
                // Many-to-one relationship with UserProfile
                entity.HasOne<UserProfile>()
                    .WithMany()
                    .HasForeignKey(e => e.UserProfileId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
