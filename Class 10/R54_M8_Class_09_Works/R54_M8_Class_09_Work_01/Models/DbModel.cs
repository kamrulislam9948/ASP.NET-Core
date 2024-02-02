﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace R54_M8_Class_09_Work_01.Models
{
    public class AppUser : IdentityUser
    {
    }

    public class JobProvider: AppUser
    {
        [StringLength(100)]
        public string? CompanyName   { get; set; }
        [StringLength(450)]
        public string? CompanyAddress { get; set; }
        public virtual ICollection<JobPost> JobPosts { get; set; } = new List<JobPost>();
        
    }
    public class JobSeeker : AppUser
    {
        [StringLength(50), Display(Name ="Full name")]
        public string? FullName { get; set; }
        [DataType(DataType.Date), Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }
        [StringLength(20)]
        public string? ContactNumber { get; set; }
        public virtual ICollection<JobSeekerJobPost> JobSeekerJobPosts { get; set; } = new List<JobSeekerJobPost>();
    }
    public class JobPost
    {
        public int JobPostId { get; set; }
        [ForeignKey("JobProvider")]
        public string? UserId { get; set; }
        [StringLength(40)]
        public string? JobPostName { get; set; }
        [Column(TypeName ="money")]
        public decimal? Salary { get; set; }
        [StringLength(40)]
        public string? Postion { get; set; }
        [StringLength(40)]
        public string? Location { get; set; }
        [StringLength(250)]
        public string? Description { get; set; }
        [Column(TypeName ="date"), DataType(DataType.Date)]
        public DateTime? LastDayOfSubmission { get; set; }
        public bool IsClosed { get; set; }
        public virtual JobProvider? JobProvider { get; set; }
        public virtual ICollection<JobSeekerJobPost> JobSeekerJobPosts { get; set; } = new List<JobSeekerJobPost>();
    }
    public class JobSeekerJobPost
    {
        public int Id { get; set; }
        [ForeignKey("JobSeeker")]
        public string? UserId { get; set; }
        [ForeignKey("JobPost")]
        public int JobPostId { get; set; }
        [Column(TypeName ="date"), DataType(DataType.Date)]
        public DateTime? DateApplied { get; set; }
        public virtual JobSeeker? JobSeeker { get; set; }
        public virtual JobPost? JobPost { get; set; }
    }
    //public class AppDbContext:IdentityDbContext<IdentityUser>
    //{
    //    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    //    //public DbSet<JobProvider> JobProviders { get; set; } = default!;
    //    //public DbSet<JobSeeker> JobSeekers { get; set; } = default!;
    //    //public DbSet<JobPost> JobPosts { get; set; } = default!;
    //    //public DbSet<JobSeekerJobPost> JobSeekerJobPosts { get; set; } = default!;
    //    protected override void OnModelCreating(ModelBuilder builder)
    //    {
    //        //builder.Entity<JobSeekerJobPost>()
    //        //    .HasKey(p => new { p.UserId, p.JobPostId });
    //    }
    //}
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<JobProvider> JobProviders { get; set; } = default!;
        public DbSet<JobSeeker> JobSeekers { get; set; } = default!;
        public DbSet<JobPost> JobPosts { get; set; } = default!;
        public DbSet<JobSeekerJobPost> JobSeekerJobPosts { get; set; } = default!;
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    //builder.Entity<JobSeekerJobPost>()
        //    //            .HasKey(p => new { p.UserId, p.JobPostId });
        //}
    }
}
